using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    public class BaseSource
    {
        public int Integer { get; set; } = 561;
        public float Float { get; set; } = 65.2f;
        public string String { get; set; } = "Super string";
        public bool Bool { get; set; } = false;
        public ComplexObject Object { get; set; } = new();
    }

    public class ComplexObject
    {
        public int Integer { get; set; } = 562;
        public float Float { get; set; } = 6.2f;
        public string String { get; set; } = "Super string 2";
        public bool Bool { get; set; } = true;

    }
}
