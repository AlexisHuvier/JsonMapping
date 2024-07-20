using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    public class ComplexTarget
    {
        public string FullString { get; set; } = default!;
        public string StartString { get; set; } = default!;
        public string EndString { get; set; } = default!;
        public string MiddleString { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
