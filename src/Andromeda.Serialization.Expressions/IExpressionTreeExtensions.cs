using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using Andromeda.Expressions;

namespace Andromeda.Serialization.Expressions
{
    public static class IExpressionTreeExtensions
    {
        public static IExpressionTree EmitReturnTrue(this IExpressionTree expr) => expr.EmitReturn(_true);
        private static readonly ConstantExpression _true = Constant(true, typeof(bool));

        public static IExpressionTree EmitReturn(this IExpressionTree expr, Expression expression) {
            var target = Label(expression.Type); var returnLabel = Label(target, Default(expression.Type));
            return expr.Emit(Return(target, expression, expression.Type)).Emit(returnLabel);
        }
    }
}
