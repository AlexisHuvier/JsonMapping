using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    internal class BaseTarget
    {
        public int Entier { get; set; }
        public float Flottant { get; set; }
        public string Chaine { get; set; } = default!;
        public bool Booleen { get; set; }
        public int ComplexEntier { get; set; }
        public float ComplexFlottant { get; set; }
        public string ComplexChaine { get; set; } = default!;
        public bool ComplexBooleen { get; set; }
        public int DefaultEntier { get; set; }
        public float DefaultFlottant { get; set; }
        public string DefaultChaine { get; set; } = default!;
        public bool DefaultBooleen { get; set; }
        public int NotDefinedEntier { get; set; }
        public float NotDefinedFlottant { get; set; }
        public string NotDefinedChaine { get; set; } = default!;
        public bool NotDefinedBooleen { get; set; }
    }
}
