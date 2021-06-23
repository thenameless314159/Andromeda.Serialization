using Andromeda.Expressions;
using Andromeda.FastExpressions;
using FastExpressionCompiler;
using Xunit.Abstractions;
using Xunit;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class TestSizingExpressionBuilder : SizingExpressionBuilder
    {
        public TestSizingExpressionBuilder(ITestOutputHelper logger, bool useFastExprCompiler)
            : base(useFastExprCompiler
                ? new DefaultFastExpressionCompilerFactory(CompilerFlags.EnableDelegateDebugInfo)
                : new DefaultExpressionCompilerFactory())
        {
            _beforeCompile = e => {
                var humanReadableExpr = e.ToCSharpString();
                Assert.False(string.IsNullOrWhiteSpace(humanReadableExpr));
                _logger.WriteLine(humanReadableExpr);
            };

            _logger = logger;
        }

        private readonly ITestOutputHelper _logger;

        protected override void BuildSizeOfExpressionOf<T>(IExpressionTree expression)
        {
            Assert.NotNull(expression.GetParameter("sizing"));
            Assert.NotNull(expression.GetParameter("value"));
            Assert.Empty(expression);
        }
    }
}
