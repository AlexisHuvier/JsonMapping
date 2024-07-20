using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JsonMapping
{
    public class MappingConfig
    {
        public required string SourceType { get;set; }
        public Dictionary<string, FieldConfig> Mapping { get; set; } = [];
    }

    public class FieldConfig
    {
        public string? Source { get; set; }
        public List<PropertyInfo?>? SourceProperties { get; set; }
        public int? SourceStart { get; set; }
        public int? SourceEnd { get; set; }
        public string? Transformer { get; set; }
        public Dictionary<string, object?>? TransformerParams { get; set; }
        public required object Default { get; set; }
        public required string Type { get; set; }
    }    
}
