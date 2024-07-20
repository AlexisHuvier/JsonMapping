namespace JsonMapping.Transform
{
    internal class MappingTransform: ITransform
    {
        public object? Transform(object source, Dictionary<string, object?> parameters)
        {
            if(source is string sourceStr && parameters.TryGetValue(sourceStr, out var mapping))
                return mapping;
            return null;
        }
    }
}
