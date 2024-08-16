using System;
using System.Collections.Generic;

namespace MessagesHistory
{
    public interface IMessagesHistory
    {
        public void AddMessage(string message);
        public IReadOnlyList<Message> Messages { get; }
        public event Action<IReadOnlyList<Message>> MessagesUpdated;
    }
}