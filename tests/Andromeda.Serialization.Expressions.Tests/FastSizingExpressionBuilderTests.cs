using Xunit.Abstractions;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class FastSizingExpressionBuilderTests : SizingExpressionBuilderTests
    {
        public FastSizingExpressionBuilderTests(ITestOutputHelper logger) : base(logger, true)
        {
        }
    }
}
