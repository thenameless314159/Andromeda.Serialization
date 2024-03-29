﻿using Xunit;
using Xunit.Abstractions;
using Andromeda.Expressions;
using Andromeda.FastExpressions;
using FastExpressionCompiler;

namespace Andromeda.Serialization.Expressions.Tests
{
    public class TestSerializationExpressionBuilder : SerializationExpressionBuilderBase
    {
        public TestSerializationExpressionBuilder(ITestOutputHelper logger, bool useFastExprCompiler)
            : base(useFastExprCompiler 
                ? new DefaultFastExpressionCompilerFactory(CompilerFlags.EnableDelegateDebugInfo) 
                : new DefaultExpressionCompilerFactory())
        {
            _beforeCompile = e => { var humanReadableExpr = e.ToCSharpString();
                Assert.False(string.IsNullOrWhiteSpace(humanReadableExpr));
                _logger.WriteLine(humanReadableExpr);
            };
            
            _logger = logger;
        }

        private readonly ITestOutputHelper _logger;

        protected override void BuildDeserializeExpressionOf<T>(IExpressionTree expression) {
            Assert.NotNull(expression.GetParameter("deserializer"));
            Assert.NotNull(expression.GetParameter("bytesRead"));
            Assert.NotNull(expression.GetParameter("buffer"));
            Assert.NotNull(expression.GetParameter("value"));
            Assert.Single(expression);
        }

        protected override void BuildSerializeExpressionOf<T>(IExpressionTree expression) {
            Assert.NotNull(expression.GetParameter("bytesWritten"));
            Assert.NotNull(expression.GetParameter("serializer"));
            Assert.NotNull(expression.GetParameter("buffer"));
            Assert.NotNull(expression.GetParameter("value"));
            Assert.Single(expression);
        }
    }
}
