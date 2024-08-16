using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    public class CalculatorView: MonoBehaviour, ICalculatorView
    { 
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submitButton;
        
        public event Action<string> InputChanged;
        public event Action<string> InputSubmitted;

        private void Awake()
        {
            submitButton.onClick.AddListener(OnSubmitClick);
            inputField.onSubmit.AddListener(OnInputSubmit);
            inputField.onValueChanged.AddListener(OnInputChanged);
        }

        private void OnSubmitClick()
        {
            InputSubmitted?.Invoke(inputField.text);
        }

        public void ChangeInput(string input)
        {
            inputField.text = input;
        }

        private void OnInputChanged(string input)
        {
            InputChanged?.Invoke(input);
        }

        private void OnInputSubmit(string input)
        {
            InputSubmitted?.Invoke(input);
        }

        private void OnDestroy()
        {
            inputField.onSubmit.RemoveAllListeners();
            inputField.onValueChanged.RemoveAllListeners();
            submitButton.onClick.RemoveAllListeners();
        }
    }

    public interface ICalculatorView
    {
        public event Action<string> InputChanged;
        public event Action<string> InputSubmitted;
        public void ChangeInput(string input);
    }
}