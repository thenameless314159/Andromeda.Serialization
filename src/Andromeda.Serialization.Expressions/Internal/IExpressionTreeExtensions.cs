using static Andromeda.Serialization.Expressions.Internal.CommonTypes;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Andromeda.Expressions;
using Andromeda.Sizing;

namespace Andromeda.Serialization.Expressions.Internal
{
    internal static class IExpressionTreeExtensions
    {
        private static readonly ConstantExpression _zero = Constant((long)0, typeof(long));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpressionTreeBuilder<DeserializerDlg<T>> SetupDeserializeExpressionTree<T>(this IExpressionTreeBuilder<DeserializerDlg<T>> expr) =>
            expr.SetupDeserializeExpressionTree(out _, out _, out _, out _);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpressionTreeBuilder<SerializerDlg<T>> SetupSerializeExpressionTree<T>(this IExpressionTreeBuilder<SerializerDlg<T>> expr) =>
            expr.SetupSerializeExpressionTree(out _, out _, out _, out _);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpressionTreeBuilder<SizeOfDlg<T>> SetupSizeOfExpressionTree<T>(this IExpressionTreeBuilder<SizeOfDlg<T>> expr) =>
            expr.SetupSizeOfExpressionTree(out _, out _);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpressionTreeBuilder<DeserializerDlg<T>> SetupDeserializeExpressionTree<T>(this IExpressionTreeBuilder<DeserializerDlg<T>> expr,
            out ParameterExpression deserializer, out ParameterExpression buffer, out ParameterExpression value,
            out ParameterExpression bytesRead)
        {
            expr.Parameter(Deserializer, nameof(deserializer), out deserializer)
                .Parameter(ReadOnlySeqByRef, nameof(buffer), out buffer)
                .Parameter(typeof(T), nameof(value), out value)
                .Parameter(LongByRef, nameof(bytesRead), out bytesRead);

            expr.Emit(Assign(bytesRead, _zero));
            return expr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpressionTreeBuilder<SerializerDlg<T>> SetupSerializeExpressionTree<T>(this IExpressionTreeBuilder<SerializerDlg<T>> expr,
            out ParameterExpression serializer, out ParameterExpression buffer, out ParameterExpression value,
            out ParameterExpression bytesWritten)
        {
            expr.Parameter(Serializer, nameof(serializer), out serializer)
                .Parameter(SpanByRef, nameof(buffer), out buffer)
                .Parameter(typeof(T), nameof(value), out value)
                .Parameter(LongByRef, nameof(bytesWritten), out bytesWritten);

            expr.Emit(Assign(bytesWritten, _zero));
            return expr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpressionTreeBuilder<SizeOfDlg<T>> SetupSizeOfExpressionTree<T>(this IExpressionTreeBuilder<SizeOfDlg<T>> expr,
            out ParameterExpression sizing, out ParameterExpression value)
        {
            expr.Parameter(SizingInterface, nameof(sizing), out sizing).Parameter(typeof(T), nameof(value), out value);
            return expr;
        }
    }
}
