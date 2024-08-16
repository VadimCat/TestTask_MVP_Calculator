using System;
using Calculator;
using MessagesHistory;

namespace Application.Calculator
{
    public class CalculatorMessageLog : ICalculator
    {
        private readonly ICalculator _calculator;
        private readonly IMessagesHistory _messagesHistory;
        
        public CalculatorMessageLog(ICalculator calculator, IMessagesHistory messagesHistory)
        {
            _calculator = calculator;
            _messagesHistory = messagesHistory;
        }

        public void ChangeInput(string input)
        {
            _calculator.ChangeInput(input);
        }

        public bool TryCalculate(out string result)
        {
            var isCalculated = _calculator.TryCalculate(out result);
            _messagesHistory.AddMessage(result);
            return isCalculated;
        }

        public string CurrentInput => _calculator.CurrentInput;


        public event Action<string> InputChanged
        {
            add => _calculator.InputChanged += value;
            remove => _calculator.InputChanged -= value;
        }
    }

    public static class CalculatorMessageLogDecoratorExtension
    {
        public static CalculatorMessageLog WithMessageHistory(this ICalculator calculator,
            IMessagesHistory messagesHistory)
        {
            return new CalculatorMessageLog(calculator, messagesHistory);
        }
    }
}