using System;
using System.Linq;

namespace Calculator
{
    public class AdditionOnlyCalculator: ICalculator
    {
        private const string ErrorFormat = "{0} = Error";
        private const string ResultFormat = "{0} = {1}";

        public string CurrentInput { get; private set; } = string.Empty;

        public event Action<string> InputChanged;


        public bool TryCalculate(out string result)
        {
            int sum = 0;
            if (IsValidInput())
            {
                var numbers = CurrentInput.Split('+');

                sum += numbers.Sum(Convert.ToInt32);

                result = string.Format(ResultFormat, CurrentInput, sum);
                CurrentInput = result;
                InputChanged?.Invoke(CurrentInput);

                return true;
            }

            result = string.Format(ErrorFormat, CurrentInput);
            CurrentInput = result;
            InputChanged?.Invoke(CurrentInput);

            return false;
        }

        private bool IsValidInput()
        {
            return !string.IsNullOrWhiteSpace(CurrentInput) && CurrentInput.All(c => c == '+' || char.IsDigit(c));
        }

        public void ChangeInput(string input)
        {
            CurrentInput = input;
        }
    }
}