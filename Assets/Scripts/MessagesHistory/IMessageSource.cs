using System;

namespace MessagesHistory
{
    public interface IMessageSource
    {
        public event Action<string> MessageSent;
    }
}