using System.Collections.Generic;

namespace MessagesHistory
{
    public interface IMessagesHistoryView
    {
        void UpdateMessages(IReadOnlyList<Message> messages);
    }
}