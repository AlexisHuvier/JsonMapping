using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace JsonMapping
{
    public class Mapper
    {
        public Mapper(string jsonFile)
        {
            if(string.IsNullOrWhiteSpace(jsonFile) || !File.Exists(jsonFile))
                throw new ArgumentException("Le fichier de configuration ne peut pas être vide ou absent", nameof(jsonFile));

            var config = JsonSerializer.Deserialize<MappingConfig>(File.ReadAllText(jsonFile)) ?? throw new ArgumentException("Impossible de désérialiser le fichier de configuration", nameof(jsonFile));

            Config = config;
            SetupFields();
        }

        public MappingConfig Config { get; }

        public TTarget Map<TSource, TTarget>(TSource source) where TTarget : class, new()
        {
            var targetFields = new Dictionary<string, object?>();

            foreach (var field in Config.Mapping)
                targetFields.Add(field.Key, MapField(source, field.Value));

            var target = new TTarget();
            var targetType = typeof(TTarget);
            foreach (var field in targetFields)
            {
                var property = targetType.GetProperty(field.Key) ?? throw new ArgumentException($"Impossible de trouver la propriété dans la cible : {field.Key}");
                property.SetValue(target, field.Value);
            }

            return target;
        }

        private static object? MapField<T>(T source, FieldConfig fieldConfig)
        {
            var baseValue = GetFieldBaseValue(source, fieldConfig);
            if (baseValue is string baseValueStr && (fieldConfig.SourceStart != null || fieldConfig.SourceEnd != null))
            {
                if (fieldConfig.SourceStart != null && fieldConfig.SourceEnd != null)
                    baseValue = baseValueStr[fieldConfig.SourceStart.Value..fieldConfig.SourceEnd.Value];
                else if (fieldConfig.SourceStart != null)
                    baseValue = baseValueStr[fieldConfig.SourceStart.Value..];
                else
                    baseValue = baseValueStr[..fieldConfig.SourceEnd!.Value];
            }

            if (baseValue != null && fieldConfig.Transformer != null)
            {
                var transformer = TransformFactory.GetTransform(fieldConfig.Transformer) ?? throw new ArgumentException($"Impossible de trouver le transformer: {fieldConfig.Transformer}");
                baseValue = transformer.Transform(baseValue, fieldConfig.TransformerParams ?? []) ?? fieldConfig.Default;
            }

            baseValue ??= fieldConfig.Default;

            if (baseValue is JsonElement value)
                baseValue = value.Deserialize(Type.GetType(fieldConfig.Type)!);

            var finalType = Type.GetType(fieldConfig.Type) ?? throw new ArgumentException($"Impossible de trouver le type du field : {fieldConfig.Type}");
            return Convert.ChangeType(baseValue, finalType, CultureInfo.InvariantCulture);
        }

        private static object? GetFieldBaseValue<T>(T source, FieldConfig fieldConfig)
        {
            if (fieldConfig.Source == null)
                return null;

            object? value = source;
            foreach(var property in fieldConfig.SourceProperties ?? [])
                value = property?.GetValue(value);

            return value;
        }

        private void SetupFields()
        {
            Type? baseType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var typeTemp = assembly.GetType(Config.SourceType);
                if (typeTemp != null)
                {
                    baseType = typeTemp;
                    break;
                }
            }

            if (baseType == null)
                throw new ArgumentException($"Impossible de trouver le type source : {Config.SourceType}");

            foreach (var field in Config.Mapping.Select(x => x.Value))
            {
                if (field.Source != null)
                {
                    var type = baseType;
                    field.SourceProperties = [];
                    foreach (var sourcePart in field.Source.Split("."))
                    {
                        var property = type?.GetProperty(sourcePart);
                        field.SourceProperties.Add(property);
                        type = property?.PropertyType;
                        if (type != null)
                            type = Nullable.GetUnderlyingType(type) ?? type;
                    }
                }
            }
        }
    }
}
