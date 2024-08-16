using System;

namespace MessageBox
{
    public interface IMessageBox
    {
        public event Action<string, string> MessageSent;
        public void ShowMessage(string messageText, string agreeText);
    }
}