using System.Diagnostics;
using MicroObjectsCalculator.CalcNodes;

namespace MicroObjectsCalculator.Ops
{
    public sealed class SubtractionCopyRightCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public SubtractionCopyRightCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new SubtractionCalcNode(_left, _right.Copy());
    }
    public sealed class SubtractionInitialCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public SubtractionInitialCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new SubtractionCalcNode(_left.Copy(), new ZeroValueDecoratorCalcNode(_right));
    }

    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class SubtractionCalcNode : CalcNode
    {
        public SubtractionCalcNode(ICalcNode left, ICalcNode right)
            : base(new SubtractionOperation(left, right),
                new SubtractionSequence(left, right),
                new SubtractionCopyRightCreateNode(left, right))
        { }
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class SubtractionOperation : CalculationOperation
    {
        public SubtractionOperation(ICalcNode left, ICalcNode right) : base(left, right) { }
        protected override int Calculation(int left, int right) => left - right;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class SubtractionSequence : CalculationSequence
    {
        public SubtractionSequence(ICalcNode left, ICalcNode right) : base(left, right, "-") { }
    }
}