using System;
using UnityEngine.Serialization;

namespace MessageBox
{
    public class MessageBox: IMessageBox
    {
        public event Action<string, string> MessageSent;
        
        public void ShowMessage(string messageText, string agreeText)
        {
            MessageSent?.Invoke(messageText, agreeText);
        }
    }
}