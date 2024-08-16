using System;

namespace Calculator
{
    public class CalculatorPresenter: IDisposable
    {
        private readonly ICalculator _model;
        private readonly ICalculatorView _view;

        public CalculatorPresenter(ICalculator model, ICalculatorView view)
        {
            _model = model;
            _view = view;

            _model.InputChanged += _view.ChangeInput;
            _view.InputChanged += _model.ChangeInput;
            _view.InputSubmitted += SubmitInput;
            
            _view.ChangeInput(_model.CurrentInput);
        }

        private void SubmitInput(string result)
        {
            _model.TryCalculate(out _);
        }

        public void Dispose()
        {
            _model.InputChanged -= _view.ChangeInput;
            _view.InputChanged -= _model.ChangeInput;
            _view.InputSubmitted -= SubmitInput;
        }
    }
}