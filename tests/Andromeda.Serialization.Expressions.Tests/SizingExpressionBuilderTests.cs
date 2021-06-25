using Xunit;
using Xunit.Abstractions;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class SizingExpressionBuilderTests
    {
        protected SizingExpressionBuilderTests(ITestOutputHelper logger, bool useFastExprCompiler) =>
            _exprBuilder = new TestSizingExpressionBuilder(logger, useFastExprCompiler);

        public SizingExpressionBuilderTests(ITestOutputHelper logger) : this(logger, false)
        {
        }

        private readonly SizingExpressionBuilderBase _exprBuilder;

        [Fact]
        public void BuildSizeOf_OutputMethodShouldReturnZeroByDefault()
        {
            var sizeOf = _exprBuilder.BuildSizeOf<string>();
            Assert.Equal(0, sizeOf(null!, string.Empty));
        }

        [Fact]
        public void BuildDeserialize_ShouldCreateValidExpressionByDefault() =>
            Assert.NotNull(_exprBuilder.BuildSizeOf<string>());
    }
}
