using System;
using Calculator;
using MessageBox;

namespace Application.Calculator
{
    public class CalculatorErrorMessage : ICalculator
    {
        private readonly ICalculator _calculator;
        private readonly IMessageBox _message;

        private const string ErrorMessage = "You have entered an unsupported expression.";
        private const string AgreeMessage = "Okay";

        public event Action<string> InputChanged
        {
            add => _calculator.InputChanged += value;
            remove => _calculator.InputChanged -= value;
        }

        public CalculatorErrorMessage(ICalculator calculator, IMessageBox message)
        {
            _calculator = calculator;
            _message = message;
        }

        public void ChangeInput(string input)
        {
            _calculator.ChangeInput(input);
        }

        public bool TryCalculate(out string result)
        {
            var isCalculated = _calculator.TryCalculate(out result);
            if (!isCalculated)
            {
                _message.ShowMessage(ErrorMessage, AgreeMessage);
            }
            return isCalculated;
        }

        public string CurrentInput => _calculator.CurrentInput;
    }

    public static class CalculatorErrorMessageDecoratorExtension
    {
        public static CalculatorErrorMessage WithErrorMessage(this ICalculator calculator,
            IMessageBox messageBox)
        {
            return new CalculatorErrorMessage(calculator, messageBox);
        }
    }
}