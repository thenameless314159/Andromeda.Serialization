using Xunit.Abstractions;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class FastSerializationExpressionBuilderTests : SerializationExpressionBuilderTests
    {
        public FastSerializationExpressionBuilderTests(ITestOutputHelper logger) : base(logger, true)
        {
        }
    }
}
