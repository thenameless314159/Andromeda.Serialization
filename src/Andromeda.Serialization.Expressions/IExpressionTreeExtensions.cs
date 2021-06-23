using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using Andromeda.Expressions;

namespace Andromeda.Serialization.Expressions
{
    public static class IExpressionTreeExtensions
    {
        public static IExpressionTree EmitReturnFalse(this IExpressionTree expr) => expr.EmitReturn(_false);
        public static IExpressionTree EmitReturnTrue(this IExpressionTree expr) => expr.EmitReturn(_true);
        private static readonly ConstantExpression _false = Constant(false, typeof(bool));
        private static readonly ConstantExpression _true = Constant(true, typeof(bool));

        internal static IExpressionTree EmitReturnZero(this IExpressionTree expr) => expr.EmitReturn(_zero);
        private static readonly ConstantExpression _zero = Constant(0, typeof(int));

        public static IExpressionTree EmitReturn(this IExpressionTree expr, Expression expression) {
            var target = Label(expression.Type); var returnLabel = Label(target, Default(expression.Type));
            return expr.Emit(Return(target, expression, expression.Type)).Emit(returnLabel);
        }
    }
}
