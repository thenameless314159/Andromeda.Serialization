using Xunit;
using System;
using System.Buffers;
using Xunit.Abstractions;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class SerializationExpressionBuilderTests
    {
        protected SerializationExpressionBuilderTests(ITestOutputHelper logger, bool useFastExprCompiler) =>
            _exprBuilder = new TestSerializationExpressionBuilder(logger, useFastExprCompiler);

        public SerializationExpressionBuilderTests(ITestOutputHelper logger) : this(logger, false)
        {
        }

        private readonly SerializationExpressionBuilder _exprBuilder;

        [Fact]
        public void BuildDeserialize_OutputMethodShouldReturnFalseByDefault() => Assert.False(_exprBuilder
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
            var serialize = _exprBuilder.BuildSerialize<SerializableModel>(); var emptySpan = Span<byte>.Empty;
            serialize(null!, ref emptySpan, new SerializableModel(), out var bytesWritten);
            Assert.Equal(0, bytesWritten);
        }
    }
}
