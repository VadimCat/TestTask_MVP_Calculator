using System;
using System.Collections.Generic;
using MessagesHistory;
using Save;

namespace Application.MessagesHistory
{
    public class MessagesHistorySave: IMessagesHistory, ISave
    {
        private const string SaveKey = "MessagesHistory";

        private readonly IAppStateSave _save;
        private readonly IMessagesHistory _messagesHistory;

        public event Action<IReadOnlyList<Message>> MessagesUpdated
        {
            add => _messagesHistory.MessagesUpdated += value;
            remove => _messagesHistory.MessagesUpdated -= value;
        }

        public MessagesHistorySave(IMessagesHistory messagesHistory, IAppStateSave save)
        {
            _messagesHistory = messagesHistory;
            _save = save;
            
            MessagesUpdated += OnMessagesUpdated;
        }

        private void OnMessagesUpdated(IReadOnlyList<Message> messages)
        {
            Save();
        }

        public void Save()
        {
            _save.SaveValue(SaveKey, Messages);
        }

        public void Load()
        {
            var messages = _save.GetValue(SaveKey, new List<Message>());
            foreach (var message in messages)
            {
                _messagesHistory.AddMessage(message.Text);
            }
        }

        public void AddMessage(string message)
        {
            _messagesHistory.AddMessage(message);
        }

        public IReadOnlyList<Message> Messages => _messagesHistory.Messages;

        public void Dispose()
        {
            MessagesUpdated -= OnMessagesUpdated;
        }
    }

    public static class MessageHistorySaveDecoratorExtension
    {
        public static MessagesHistorySave WithSave(this IMessagesHistory messagesHistory, IAppStateSave save)
        {
            return new MessagesHistorySave(messagesHistory, save);
        }
    }
}