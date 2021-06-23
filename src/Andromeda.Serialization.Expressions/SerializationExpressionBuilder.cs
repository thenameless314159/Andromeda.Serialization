using Andromeda.Serialization.Expressions.Internal;
using System.Linq.Expressions;
using Andromeda.Expressions;
using System;

namespace Andromeda.Serialization.Expressions
{
    public abstract class SerializationExpressionBuilder : SerializationMethodBuilder
    {
        protected SerializationExpressionBuilder(IExpressionTreeBuilderFactory exprBuilderFactory) =>
            _exprBuilderFactory = exprBuilderFactory;

        private readonly IExpressionTreeBuilderFactory _exprBuilderFactory;

        protected abstract void BuildDeserializeExpressionOf<T>(IExpressionTree expression);
        protected abstract void BuildSerializeExpressionOf<T>(IExpressionTree expression);

        public override DeserializerDlg<T> BuildDeserialize<T>()
        {
            var expression = _exprBuilderFactory.Create<DeserializerDlg<T>>();
            expression.SetupDeserializeExpressionTree();
            BuildDeserializeExpressionOf<T>(expression);
            expression.EmitReturnTrue();

            _beforeCompile?.Invoke(expression.Build());
            return expression.Compile();
        }

        public override SerializerDlg<T> BuildSerialize<T>()
        {
            var expression = _exprBuilderFactory.Create<SerializerDlg<T>>();
            expression.SetupSerializeExpressionTree();
            BuildSerializeExpressionOf<T>(expression);

            _beforeCompile?.Invoke(expression.Build());
            return expression.Compile();
        }

        // for testing/logging
        protected Action<LambdaExpression>? _beforeCompile;
    }
}
