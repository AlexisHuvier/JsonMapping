using UnitTests.Models;
using JsonMapping;

namespace UnitTests
{
    public class MapperUnitTest
    {
        [Fact]
        public void EmptyMapperTest()
        {
            var mapper = new Mapper("Mapping/emptyMapping.json");
            var result = mapper.Map<BaseSource, BaseTarget>(new BaseSource());

            Assert.Equal(default, result.Entier);
            Assert.Equal(default, result.Flottant);
            Assert.Equal(default, result.Chaine);
            Assert.False(result.Booleen);

            Assert.Equal(default, result.ComplexEntier);
            Assert.Equal(default, result.ComplexFlottant);
            Assert.Equal(default, result.ComplexChaine);
            Assert.False(result.ComplexBooleen);

            Assert.Equal(default, result.DefaultEntier);
            Assert.Equal(default, result.DefaultFlottant);
            Assert.Equal(default, result.DefaultChaine);
            Assert.False(result.DefaultBooleen);

            Assert.Equal(default, result.NotDefinedEntier);
            Assert.Equal(default, result.NotDefinedFlottant);
            Assert.Equal(default, result.NotDefinedChaine);
            Assert.False(result.NotDefinedBooleen);
        }

        [Fact]
        public void BaseMapperTest()
        {
            var mapper = new Mapper("Mapping/baseMapping.json");
            var result = mapper.Map<BaseSource, BaseTarget>(new BaseSource());

            Assert.Equal(561, result.Entier);
            Assert.Equal(65.2f, result.Flottant);
            Assert.Equal("Super string", result.Chaine);
            Assert.False(result.Booleen);

            Assert.Equal(562, result.ComplexEntier);
            Assert.Equal(6.2f, result.ComplexFlottant);
            Assert.Equal("Super string 2", result.ComplexChaine);
            Assert.True(result.ComplexBooleen);

            Assert.Equal(-3, result.DefaultEntier);
            Assert.Equal(-3.5f, result.DefaultFlottant);
            Assert.Equal("string", result.DefaultChaine);
            Assert.False(result.DefaultBooleen);

            Assert.Equal(-3, result.NotDefinedEntier);
            Assert.Equal(-3.5f, result.NotDefinedFlottant);
            Assert.Equal("string", result.NotDefinedChaine);
            Assert.False(result.NotDefinedBooleen);
        }

        [Fact]
        public void ComplexMapperTest()
        {
            var mapper = new Mapper("Mapping/complexMapping.json");
            var result = mapper.Map<ComplexSource, ComplexTarget>(new ComplexSource());

            Assert.Equal("Salut à tous !", result.Strings.Full);
            Assert.Equal("Salu", result.Strings.Start);
            Assert.Equal("t à ", result.Strings.Middle);
            Assert.Equal("tous !", result.Strings.End);
            Assert.Equal(new DateTime(2024, 05, 06), result.Date);
            Assert.Null(result.DateNullable);
        }

        [Fact]
        public void ErrorMapperTest()
        {
            var mapper = new Mapper("Mapping/erreurMapping.json");
            Assert.Throws<ArgumentException>(() => mapper.Map<BaseSource, BaseTarget>(new BaseSource()));
            Assert.Throws<ArgumentException>(() => new Mapper(""));
        }

        [Fact]
        public void UnknownBaseTypeTest()
        {
            Assert.Throws<ArgumentException>(() => new Mapper("Mapping/unknownSourceMapping.json"));
        }
    }
}
