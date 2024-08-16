using System;
using System.Collections.Generic;

namespace MessagesHistory
{
    public class MessagesHistoryPresenter: IDisposable
    {
        private readonly IMessagesHistory _model;
        private readonly IMessagesHistoryView _view;

        public MessagesHistoryPresenter(IMessagesHistory model, IMessagesHistoryView view)
        {
            _model = model;
            _view = view;

            _model.MessagesUpdated += UpdateMessages;
            
            _view.UpdateMessages(_model.Messages);
        }

        private void UpdateMessages(IReadOnlyList<Message> messages)
        {
            _view.UpdateMessages(messages);
        }

        public void Dispose()
        {
            _model.MessagesUpdated -= UpdateMessages;
        }
    }
}