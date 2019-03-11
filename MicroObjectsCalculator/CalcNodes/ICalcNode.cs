using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Controls;
using MicroObjectsCalculator.Ops;

namespace MicroObjectsCalculator.CalcNodes
{



    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class ZeroValueDecoratorCalcNode : ICalcNode
    {
        private readonly ICalcNode _origin;
        public ZeroValueDecoratorCalcNode(ICalcNode origin) => _origin = origin;

        public Operation Value() => new ZeroOperation();

        public Sequence Sequence() => _origin.Sequence();

        public ICalcNode Copy() => _origin.Copy();
    }

    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class OneValueDecoratorCalcNode : ICalcNode
    {
        private readonly ICalcNode _origin;
        public OneValueDecoratorCalcNode(ICalcNode origin) => _origin = origin;

        public Operation Value() => new OneOperation();

        public Sequence Sequence() => _origin.Sequence();

        public ICalcNode Copy() => _origin.Copy();
    }

    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class ContentControlValueCalcNode : ICalcNode
    {
        private readonly ContentControl _origin;

        public ContentControlValueCalcNode(ContentControl origin) => _origin = origin;

        public Operation Value() => throw new Exception("Decorate and define the value to be returned");

        public Sequence Sequence() => new EmptySequence();

        public ICalcNode Copy() => new StringValueCalcNode(_origin.Content.ToString());
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class EmptySequence : Sequence
    {
        protected override string RawValue() => string.Empty;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class ZeroOperation : Operation
    {
        protected override int RawValue() => 0;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class OneOperation : Operation
    {
        protected override int RawValue() => 1;
    }



    [DebuggerDisplay("[{GetType().Name}][Value={Value()}][Display={Sequence()}]")]
    public sealed class StringValueCalcNode : ICalcNode
    {
        private readonly string _value;
        public StringValueCalcNode(string value) => _value = value;

        public Operation Value() => new StringToIntOperation(_value);
        public Sequence Sequence() => new StringSequence(_value);
        public ICalcNode Copy() => new StringValueCalcNode(_value);
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class StringSequence : Sequence
    {
        private readonly string _origin;

        public StringSequence(string origin) => _origin = origin;

        protected override string RawValue() => _origin;
    }

    [DebuggerDisplay("[{GetType().Name}][RawValue={RawValue()}]")]
    public sealed class StringToIntOperation : Operation
    {
        private readonly string _origin;

        public StringToIntOperation(string origin) => _origin = origin;
        protected override int RawValue() => int.TryParse(_origin, out int val) ? val : 0;
    }
}