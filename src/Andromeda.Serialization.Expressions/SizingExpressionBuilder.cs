using Andromeda.Serialization.Expressions.Internal;
using System.Linq.Expressions;
using Andromeda.Expressions;
using Andromeda.Sizing;
using System;
using System.Linq;

namespace Andromeda.Serialization.Expressions
{
    public abstract class SizingExpressionBuilder : SizingMethodBuilder
    {
        protected SizingExpressionBuilder(IExpressionTreeBuilderFactory exprBuilderFactory) =>
            _exprBuilderFactory = exprBuilderFactory;

        private readonly IExpressionTreeBuilderFactory _exprBuilderFactory;

        protected abstract void BuildSizeOfExpressionOf<T>(IExpressionTree expression);

        public override SizeOfDlg<T> BuildSizeOf<T>()
        {
            var expression = _exprBuilderFactory.Create<SizeOfDlg<T>>();
            expression.SetupSizeOfExpressionTree();
            BuildSizeOfExpressionOf<T>(expression);

            if (!expression.Any()) expression.EmitReturnZero();
            _beforeCompile?.Invoke(expression.Build());
            return expression.Compile();
        }

        // for testing/logging
        protected Action<LambdaExpression>? _beforeCompile;
    }
}
