using System;

namespace MessageBox
{
    public class MessageBoxPresenter: IDisposable
    {
        private readonly IMessageBox _model;
        private readonly IMessageBoxView _view;

        public MessageBoxPresenter(IMessageBox model, IMessageBoxView view)
        {
            _model = model;
            _view = view;
            
            _model.MessageSent += _view.ShowMessage;
        }
        
        public void Dispose()
        {
            _model.MessageSent -= _view.ShowMessage;
        }
    }
}