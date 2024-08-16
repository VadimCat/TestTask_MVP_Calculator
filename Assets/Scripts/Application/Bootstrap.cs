using System;
using System.Collections.Generic;
using Application.Calculator;
using Application.MessagesHistory;
using Calculator;
using MessageBox;
using MessagesHistory;
using Save;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MessagesHistoryView messagesHistoryView;
    [SerializeField] private CalculatorView calculatorView;
    [SerializeField] private MessageBoxView messageBoxView;
    
    private List<IDisposable> _disposables;

    private void Start()
    {
        var messagesHistory = new MessagesHistory.MessagesHistory();
        var messageBox = new MessageBox.MessageBox();
        var calculator = new AdditionOnlyCalculator().WithMessageHistory(messagesHistory).WithErrorMessage(messageBox);

        IAppStateSave save = new PlayerPrefsAppStateSave();
        save.Load();

        var calculatorSave = calculator.WithSave(save);
        var messagesHistorySave = messagesHistory.WithSave(save);
        
        SaveComposite saveComposite = new SaveComposite(calculatorSave, messagesHistorySave);
        saveComposite.Load();

        _disposables = new List<IDisposable>(2)
        {
            new MessagesHistoryPresenter(messagesHistorySave,
                messagesHistoryView),
            new CalculatorPresenter(calculatorSave, calculatorView),
            new MessageBoxPresenter(messageBox, messageBoxView)
        };
    }

    private void OnDestroy()
    {
        foreach (var item in _disposables)
        {
            item.Dispose();
        }
    }
}