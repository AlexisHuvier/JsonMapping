namespace JsonMapping.Transform
{
    internal class AbsTransform : ITransform
    {
        public object? Transform(object source, Dictionary<string, object?> parameters)
        {
            if (parameters.Count != 0)
                throw new ArgumentException("Le transform Abs n'a pas besoin d'arguments");

            if (source is int intSource)
                return Math.Abs(intSource);
            if (source is float floatSource)
                return Math.Abs(floatSource);
            if (source is double doubleSource)
                return Math.Abs(doubleSource);
            return null;
        }
    }
}
