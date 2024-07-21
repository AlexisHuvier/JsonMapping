using JsonMapping.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class CustomTransform : ITransform
    {
        public object? Transform(object source, Dictionary<string, object?> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
