﻿using Andromeda.Serialization.Expressions.Internal;
using System.Linq.Expressions;
using Andromeda.Expressions;
using Andromeda.Sizing;
using System;

namespace Andromeda.Serialization.Expressions
{
    public abstract class SizingExpressionBuilderBase : SizingMethodBuilder
    {
        protected SizingExpressionBuilderBase(IExpressionTreeBuilderFactory exprBuilderFactory) =>
            _exprBuilderFactory = exprBuilderFactory;

        private readonly IExpressionTreeBuilderFactory _exprBuilderFactory;

        protected abstract void BuildSizeOfExpressionOf<T>(IExpressionTree expression);

        public override SizeOfDlg<T> BuildSizeOf<T>()
        {
            var expression = _exprBuilderFactory.Create<SizeOfDlg<T>>();
            expression.SetupSizeOfExpressionTree();
            BuildSizeOfExpressionOf<T>(expression);

            if (expression.Count < 1) expression.EmitReturnZero();
            _beforeCompile?.Invoke(expression.Build());
            return expression.Compile();
        }

        // for testing/logging
        protected Action<LambdaExpression>? _beforeCompile;
    }
}
