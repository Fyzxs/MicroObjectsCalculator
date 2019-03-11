using System.Diagnostics;
using MicroObjectsCalculator.CalcNodes;

namespace MicroObjectsCalculator.Ops
{
    public sealed class DivisionCopyRightCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public DivisionCopyRightCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new DivisionCalcNode(_left, _right.Copy());
    }

    public sealed class DivisionInitialCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public DivisionInitialCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new DivisionCalcNode(_left.Copy(), new OneValueDecoratorCalcNode(_right));
    }

    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class DivisionCalcNode : CalcNode
    {
        public DivisionCalcNode(ICalcNode left, ICalcNode right)
            : base(new DivisionOperation(left, right),
                new DivisionSequence(left, right),
                new DivisionCopyRightCreateNode(left, right)) { }
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class DivisionOperation : CalculationOperation
    {
        public DivisionOperation(ICalcNode left, ICalcNode right) : base(left, right) { }
        protected override int Calculation(int left, int right) => left / right;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class DivisionSequence : CalculationSequence
    {
        public DivisionSequence(ICalcNode left, ICalcNode right) : base(left, right, "/") { }
    }
}
