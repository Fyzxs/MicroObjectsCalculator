namespace MicroObjectsCalculator.Ops
{
    public interface ICreateNode
    {
        ICalcNode Create();
    }

    public interface ICalcNode
    {
        Operation Value();
        Sequence Sequence();
        ICalcNode Copy();
    }

    public abstract class CalcNode : ICalcNode
    {
        private readonly Operation _operation;
        private readonly Sequence _sequence;
        private readonly ICreateNode _createNode;

        protected CalcNode(Operation operation, Sequence sequence, ICreateNode createNode)
        {
            _operation = operation;
            _sequence = sequence;
            _createNode = createNode;
        }

        public Operation Value() => _operation;

        public Sequence Sequence() => _sequence;

        public ICalcNode Copy() => _createNode.Create();
    }

    public abstract class Operation
    {
        public static implicit operator int(Operation origin) => origin.RawValue();

        protected abstract int RawValue();
    }

    public abstract class CalculationOperation : Operation
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;

        protected CalculationOperation(ICalcNode left, ICalcNode right)
        {
            _left = left;
            _right = right;
        }

        protected override int RawValue() => Calculation(_left.Value(), _right.Value());
        protected abstract int Calculation(int left, int right);
    }

    public abstract class Sequence
    {
        public static implicit operator string(Sequence origin) => origin.RawValue();
        protected abstract string RawValue();
    }

    public abstract class CalculationSequence : Sequence
    {
        private readonly ICalcNode _left;
        private readonly ICalcNode _right;
        private readonly string _symbol;

        protected CalculationSequence(ICalcNode left, ICalcNode right, string symbol)
        {
            _left = left;
            _right = right;
            _symbol = symbol;
        }

        protected override string RawValue() => $"{(string) _left.Sequence()} {_symbol} {(string) _right.Sequence()}";
    }
}