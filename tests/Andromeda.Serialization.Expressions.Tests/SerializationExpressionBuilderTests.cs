using Xunit;
using System;
using System.Buffers;
using Xunit.Abstractions;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class SerializationExpressionBuilderTests
    {
        public SerializationExpressionBuilderTests(ITestOutputHelper logger) =>
            _exprBuilder = new TestSerializationExpressionBuilder(logger);

        private readonly SerializationExpressionBuilder _exprBuilder;

        [Fact]
        public void BuildDeserialize_OutputMethodShouldReturnTrueByDefault() => Assert.True(_exprBuilder
            .BuildDeserialize<SerializableModel>()(null!, ReadOnlySequence<byte>.Empty, 
                new SerializableModel(), out _));

        [Fact]
        public void BuildDeserialize_ShouldCreateValidExpressionByDefault()
        {
            var deserialize = _exprBuilder.BuildDeserialize<SerializableModel>();
            deserialize(null!, ReadOnlySequence<byte>.Empty, new SerializableModel(), out var bytesRead);
            Assert.Equal(0, bytesRead);
        }

        [Fact]
        public void BuildSerialize_ShouldCreateValidExpressionByDefault()
        {
            var serialize = _exprBuilder.BuildSerialize<SerializableModel>();
            var emptySpan = Span<byte>.Empty;
            serialize(null!, ref emptySpan, new SerializableModel(), out var bytesWritten);
            Assert.Equal(0, bytesWritten);
        }
    }
}
