using System.Windows;
using System.Windows.Controls;
using MicroObjectsCalculator.CalcNodes;
using MicroObjectsCalculator.Ops;

namespace MicroObjectsCalculator
{
    public interface IMainWindow { }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindow
    {
        private ICalcNode _lastOperation;
        private bool _lastDisplayResult;
        private static ICalcNode _inputNode;

        public MainWindow()
        {
            InitializeComponent();
            _inputNode = new ContentControlValueCalcNode(LblCurrentResult);
            _lastOperation = _inputNode;
        }

        private void Digit_OnClick(object sender, RoutedEventArgs e)
        {
            string content = ((ContentControl)sender).Content.ToString();
            if (_lastDisplayResult)
            {
                LblCurrentResult.Content = content;
                _lastDisplayResult = false;

                return;
            }

            LblCurrentResult.Content += content;
        }

        private void BtnAddition_OnClick(object sender, RoutedEventArgs e) 
            => Operation(new AdditionInitialCreateNode(_lastOperation, _inputNode));

        private void BtnSubtraction_OnClick(object sender, RoutedEventArgs e) 
            => Operation(new SubtractionInitialCreateNode(_lastOperation, _inputNode));

        private void BtnMultiplication_OnClick(object sender, RoutedEventArgs e)
            => Operation(new MultiplicationInitialCreateNode(_lastOperation, _inputNode));

        private void BtnDivision_OnClick(object sender, RoutedEventArgs e) 
            => Operation(new DivisionInitialCreateNode(_lastOperation, _inputNode));
        private void Operation(ICreateNode createNode)
        {
            if (_lastDisplayResult) return;

            _lastOperation = createNode.Create();
            LblCurrentResult.Content = (int)_lastOperation.Value();

            LblButtonSequence.Content = (string)_lastOperation.Sequence();

            _lastDisplayResult = true;
        }


        private void BtnEquals_OnClick(object sender, RoutedEventArgs e)
        {
            _lastOperation = _lastOperation.Copy();

            LblCurrentResult.Content = (int)_lastOperation.Value();

            LblButtonSequence.Content = "";
            
            _lastDisplayResult = false;

            _lastOperation = new ContentControlValueCalcNode(LblCurrentResult);
        }

        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            _lastOperation = new ContentControlValueCalcNode(LblCurrentResult);
            LblButtonSequence.Content = "";
            LblCurrentResult.Content = "";
            _lastDisplayResult = false;
        }

    }
}
