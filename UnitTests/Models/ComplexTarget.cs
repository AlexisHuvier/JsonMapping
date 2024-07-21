using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    public class ComplexTarget
    {
        public Strings Strings { get; set; } = default!;
        public DateTime Date { get; set; }
    }

    public class Strings
    {
        public string Full { get; set; } = default!;
        public string Start { get; set; } = default!;
        public string End { get; set; } = default!;
        public string Middle { get; set; } = default!;
    }
}
