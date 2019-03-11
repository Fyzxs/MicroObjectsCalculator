using System.Diagnostics;
using MicroObjectsCalculator.CalcNodes;

namespace MicroObjectsCalculator.Ops
{
    public sealed class MultiplicationCopyRightCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public MultiplicationCopyRightCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new MultiplicationCalcNode(_left, _right.Copy());
    }

    public sealed class MultiplicationInitialCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public MultiplicationInitialCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new MultiplicationCalcNode(_left.Copy(), new OneValueDecoratorCalcNode(_right));
    }

    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class MultiplicationCalcNode : CalcNode
    {
        public MultiplicationCalcNode(ICalcNode left, ICalcNode right)
            : base(new MultiplicationOperation(left, right),
                new MultiplicationSequence(left, right),
                new MultiplicationCopyRightCreateNode(left, right))
        { }
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class MultiplicationOperation : CalculationOperation
    {
        public MultiplicationOperation(ICalcNode left, ICalcNode right) : base(left, right) { }
        protected override int Calculation(int left, int right) => left * right;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class MultiplicationSequence : CalculationSequence
    {
        public MultiplicationSequence(ICalcNode left, ICalcNode right) : base(left, right, "*"){ }
    }
}