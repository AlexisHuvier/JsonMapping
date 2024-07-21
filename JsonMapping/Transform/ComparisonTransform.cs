using System.Text.Json;

namespace JsonMapping.Transform
{
    internal class ComparisonTransform: ITransform
    {
        public object? Transform(object source, Dictionary<string, object?> parameters)
        {
            if (parameters.Count != 4 ||
                !parameters.TryGetValue("Type", out object? type) ||
                !parameters.TryGetValue("Value", out object? value) ||
                !parameters.TryGetValue("True", out object? trueValue) ||
                !parameters.TryGetValue("False", out object? falseValue))
            {
                throw new ArgumentException("Le transform Comparison a besoin de quatre arguments : Type, Value, True et False");
            }

            if (value is JsonElement valueJsonElement)
                value = valueJsonElement.Deserialize(source.GetType());
            if(type is JsonElement typeJsonElement)
                type = typeJsonElement.Deserialize<string>();

            if (type is string strType && (strType == "Greater" || strType == "Equal" || strType == "Lesser"))
            {
                try
                {
                    if(
                        strType == "Greater" && ((IComparable)source).CompareTo(value!) > 0 ||
                        strType == "Equal" && ((IComparable)source).CompareTo(value!) == 0 ||
                        strType == "Lesser" && ((IComparable)source).CompareTo(value!) < 0
                    )
                        return trueValue;
                    return falseValue;
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
            else 
                throw new ArgumentException("Type doit être 'Greater', 'Equal' ou 'Lesser'");
        }
    }
}
