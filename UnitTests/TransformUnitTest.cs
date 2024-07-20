using System.Text.Json;
using JsonMapping;

namespace UnitTests
{
    public class TransformUnitTest
    {

        [Fact]
        public void AbsTransformTest()
        {
            var transform = TransformFactory.GetTransform("Abs");

            Assert.NotNull(transform);

            Assert.Equal(8, transform.Transform(8, []));
            Assert.Equal(8, transform.Transform(-8, []));
            Assert.Equal(8.85f, transform.Transform(-8.85f, []));
            Assert.Equal(8.82, transform.Transform(-8.82, []));

            Assert.Null(transform.Transform("", []));

            Assert.Throws<ArgumentException>(() => transform.Transform(-9, new() { { "Param1", "" } }));
        }

        [Fact]
        public void DateTransformTest()
        {
            var transform = TransformFactory.GetTransform("Date");

            Assert.NotNull(transform);

            Assert.Equal(new DateTime(2024, 12, 13), transform.Transform("2024-12-13", new() { { "Format", "yyyy-MM-dd" } }));
            Assert.Equal(new DateTime(2024, 12, 13), transform.Transform("2024-12-13", new() { { "Format", JsonSerializer.SerializeToElement("yyyy-MM-dd") } }));
            Assert.Equal(new DateTime(2024, 12, 13, 5, 15, 36), transform.Transform("2024-12-13T05:15:36", new() { { "Format", "yyyy-MM-dd'T'HH:mm:ss" } }));
            var date = transform.Transform("2024-12-13T05:15:36.652+02:00", new() { { "Format", "yyyy-MM-dd'T'HH:mm:ss.fffK" } });
            Assert.NotNull(date);
            Assert.Equal(new DateTime(2024, 12, 13, 3, 15, 36, 652, DateTimeKind.Utc), ((DateTime)date).ToUniversalTime());
            
            Assert.Null(transform.Transform("2024-12-89", new() { { "Format", "yyyy-MM-dd" } }));
            Assert.Null(transform.Transform("2024-12-13'T'12", new() { { "Format", "yyyy-MM-dd'T'HH" } }));
            Assert.Null(transform.Transform(2093, new() { { "Format", "yyyy" } }));

            Assert.Throws<ArgumentException>(() => transform.Transform("2024-12-13", []));
            Assert.Throws<ArgumentException>(() => transform.Transform("2024-12-13", new() { { "Format", "yyyy-MM-dd" }, { "Param2", "" } }));
            Assert.Throws<ArgumentException>(() => transform.Transform("2024-12-13", new() { { "Form", "yyyy-MM-dd" } }));
        }

        [Fact]
        public void ComparisonGreaterTransformTest()
        {
            var transform = TransformFactory.GetTransform("Comparison");

            Assert.NotNull(transform);

            Assert.Equal(6, transform.Transform(3, new() { { "Type", "Greater" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(9, transform.Transform(10, new() { { "Type", "Greater" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(5, new() { { "Type", "Greater" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(5, new() { { "Type", JsonSerializer.SerializeToElement("Greater") }, { "Value", JsonSerializer.SerializeToElement(5) }, { "True", 9 }, { "False", 6 } }));

            Assert.Equal("No", transform.Transform(3, new() { { "Type", "Greater" }, { "Value", 5 }, { "True", "Yes" }, { "False", "No" } }));
            Assert.Equal(6, transform.Transform(10.5f, new() { { "Type", "Greater" }, { "Value", 58.6f }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(9, transform.Transform(10.5, new() { { "Type", "Greater" }, { "Value", 5.3 }, { "True", 9 }, { "False", 6 } }));

            Assert.Null(transform.Transform("2024-12-89", new() { { "Type", "Greater" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));

            Assert.Throws<ArgumentException>(() => transform.Transform(3, []));
            Assert.Throws<ArgumentException>(() => transform.Transform(3, new() { { "Type", "Greater" }, { "Value", 5 }, { "True", 9 }, { "False", 6 }, { "Param4", "" } }));
            Assert.Throws<ArgumentException>(() => transform.Transform(3, new() { { "Type", "Greater" }, { "Form", "yyyy-MM-dd" }, { "True", 9 }, { "False", 6 } }));
        }

        [Fact]
        public void ComparisonLesserTransformTest()
        {
            var transform = TransformFactory.GetTransform("Comparison");

            Assert.NotNull(transform);

            Assert.Equal(9, transform.Transform(3, new() { { "Type", "Lesser" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(10, new() { { "Type", "Lesser" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(5, new() { { "Type", "Lesser" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(5, new() { { "Type", JsonSerializer.SerializeToElement("Lesser") }, { "Value", JsonSerializer.SerializeToElement(5) }, { "True", 9 }, { "False", 6 } }));

            Assert.Equal("Yes", transform.Transform(3, new() { { "Type", "Lesser" }, { "Value", 5 }, { "True", "Yes" }, { "False", "No" } }));
            Assert.Equal(9, transform.Transform(10.5f, new() { { "Type", "Lesser" }, { "Value", 58.6f }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(10.5, new() { { "Type", "Lesser" }, { "Value", 5.3 }, { "True", 9 }, { "False", 6 } }));

            Assert.Null(transform.Transform("2024-12-89", new() { { "Type", "Lesser" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));

            Assert.Throws<ArgumentException>(() => transform.Transform(3, []));
            Assert.Throws<ArgumentException>(() => transform.Transform(3, new() { { "Type", "Lesser" }, { "Value", 5 }, { "True", 9 }, { "False", 6 }, { "Param4", "" } }));
            Assert.Throws<ArgumentException>(() => transform.Transform(3, new() { { "Type", "Lesser" }, { "Form", "yyyy-MM-dd" }, { "True", 9 }, { "False", 6 } }));
        }

        [Fact]
        public void ComparisonEqualsTransformTest()
        {
            var transform = TransformFactory.GetTransform("Comparison");

            Assert.NotNull(transform);

            Assert.Equal(6, transform.Transform(10, new() { { "Type", "Equal" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(9, transform.Transform(5, new() { { "Type", "Equal" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(9, transform.Transform(5, new() { { "Type", JsonSerializer.SerializeToElement("Equal") }, { "Value", JsonSerializer.SerializeToElement(5) }, { "True", 9 }, { "False", 6 } }));

            Assert.Equal("Yes", transform.Transform(5, new() { { "Type", "Equal" }, { "Value", 5 }, { "True", "Yes" }, { "False", "No" } }));
            Assert.Equal(9, transform.Transform(10.5f, new() { { "Type", "Equal" }, { "Value", 10.5f }, { "True", 9 }, { "False", 6 } }));
            Assert.Equal(6, transform.Transform(10.5, new() { { "Type", "Equal" }, { "Value", 5.3 }, { "True", 9 }, { "False", 6 } }));

            Assert.Null(transform.Transform("2024-12-89", new() { { "Type", "Equal" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));

            Assert.Throws<ArgumentException>(() => transform.Transform(3, []));
            Assert.Throws<ArgumentException>(() => transform.Transform(3, new() { { "Type", "Equal" }, { "Value", 5 }, { "True", 9 }, { "False", 6 }, { "Param4", "" } }));
            Assert.Throws<ArgumentException>(() => transform.Transform(3, new() { { "Type", "Equal" }, { "Form", "yyyy-MM-dd" }, { "True", 9 }, { "False", 6 } }));
        }

        [Fact]
        public void ComparisonErrorTransformTest()
        {
            var transform = TransformFactory.GetTransform("Comparison");

            Assert.NotNull(transform);

            Assert.Throws<ArgumentException>(() => transform.Transform(10, new() { { "Type", 5 }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
            Assert.Throws<ArgumentException>(() => transform.Transform(10, new() { { "Type", "Unknown" }, { "Value", 5 }, { "True", 9 }, { "False", 6 } }));
        }

        [Fact]
        public void MappingTransformTest()
        {
            var transform = TransformFactory.GetTransform("Mapping");

            Assert.NotNull(transform);

            var mapping = new Dictionary<string, object?>
            {
                { "Foo", "Bar" },
                { "Entier", 1 },
                { "Flottant", 2.5f },
                { "Booleen", true }
            };

            Assert.Equal("Bar", transform.Transform("Foo", mapping));
            Assert.Equal(1, transform.Transform("Entier", mapping));
            Assert.Equal(2.5f, transform.Transform("Flottant", mapping));
            Assert.Equal(true, transform.Transform("Booleen", mapping));
            Assert.Null(transform.Transform("Unknown", mapping));

            Assert.Null(transform.Transform("value", []));
        }

        [Fact]
        public void UnknownTransformTest()
        {
            var transform = TransformFactory.GetTransform("INCONNU");
            Assert.Null(transform);
        }
    }
}