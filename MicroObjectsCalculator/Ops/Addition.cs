using System.Diagnostics;
using MicroObjectsCalculator.CalcNodes;

namespace MicroObjectsCalculator.Ops
{
    public sealed class AdditionCopyRightCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public AdditionCopyRightCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new AdditionCalcNode(_left, _right.Copy());
    }
    public sealed class AdditionInitialCreateNode : ICreateNode
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        public AdditionInitialCreateNode(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        public ICalcNode Create() => new AdditionCalcNode(_left.Copy(), new ZeroValueDecoratorCalcNode(_right));
    }

    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class AdditionCalcNode : CalcNode
    {
        public AdditionCalcNode(ICalcNode left, ICalcNode right)
            : base(new AdditionOperation(left, right), 
                new AdditionSequence(left, right),
                new AdditionCopyRightCreateNode(left, right))
        {}
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class AdditionOperation : CalculationOperation
    {
        public AdditionOperation(ICalcNode left, ICalcNode right) : base(left, right) { }
        protected override int Calculation(int left, int right) => left + right;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class AdditionSequence : CalculationSequence
    {
        public AdditionSequence(ICalcNode left, ICalcNode right) : base(left, right, "+") { }
    }
}