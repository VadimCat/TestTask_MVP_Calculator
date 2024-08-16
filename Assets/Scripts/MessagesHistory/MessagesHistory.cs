using System;
using System.Collections.Generic;

namespace MessagesHistory
{
    public class MessagesHistory: IMessagesHistory
    {
        private readonly IMessageSource _source;
        private readonly List<Message> _messages = new();
        
        public IReadOnlyList<Message> Messages => _messages;
        public event Action<IReadOnlyList<Message>> MessagesUpdated;
        
        public void AddMessage(string message)
        {
            _messages.Add(new Message(message));
            MessagesUpdated?.Invoke(Messages);
        }
    }
}