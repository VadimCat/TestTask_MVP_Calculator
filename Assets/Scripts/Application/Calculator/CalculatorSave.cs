using System;
using Calculator;
using Save;

namespace Application.Calculator
{
    public class CalculatorSave: ICalculator, ISave
    {
        private readonly ICalculator _calculator;
        private readonly IAppStateSave _save;
        private const string SaveKey = "CalculatorInput";
        public string CurrentInput => _calculator.CurrentInput;

        public event Action<string> InputChanged
        {
            add => _calculator.InputChanged += value;
            remove => _calculator.InputChanged -= value;
        }

        public CalculatorSave(ICalculator calculator, IAppStateSave save)
        {
            _calculator = calculator;
            _save = save;
        }

        public void ChangeInput(string input)
        {
            _calculator.ChangeInput(input);
            Save();
        }

        public bool TryCalculate(out string result)
        {
            var isCalculated = _calculator.TryCalculate(out result);
            Save();
            return isCalculated;
        }

        public void Save()
        {
            _save.SaveValue(SaveKey, CurrentInput);
        }

        public void Load()
        {
            ChangeInput(_save.GetValue<string>(SaveKey));
        }
    }
    
    public static class CalculatorSaveDecoratorExtension
    {
        public static CalculatorSave WithSave(this ICalculator calculator, IAppStateSave save)
        {
            return new CalculatorSave(calculator, save);
        }
    }
}