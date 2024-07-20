using JsonMapping.Transform;

namespace JsonMapping
{
    public static class TransformFactory
    {
        private static readonly Dictionary<string, ITransform> Implementations = new()
        {
            { "Comparison", new ComparisonTransform() },
            { "Date", new DateTransform() },
            { "Abs", new AbsTransform() },
            { "Mapping", new MappingTransform() }
        };

        public static void RegisterTransform(string transformType, ITransform transform) => Implementations[transformType] = transform;
        public static ITransform? GetTransform(string transformType) => Implementations.GetValueOrDefault(transformType);
    }
}
