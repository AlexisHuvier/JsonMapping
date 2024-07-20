using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonMapping.Transform
{
    public interface ITransform
    {
        public object? Transform(object source, Dictionary<string, object?> parameters);
    }
}
