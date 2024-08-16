using System;

namespace Calculator
{
    public interface ICalculator
    {
        public void ChangeInput(string input);
        public bool TryCalculate(out string result);
        public string CurrentInput { get; }
        public event Action<string> InputChanged;
    }
}