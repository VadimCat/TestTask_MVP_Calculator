using TMPro;
using UnityEngine;

namespace MessagesHistory
{
    public class MessageView: MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void SetMessage(string message)
        {
            text.SetText(message);
        }
        
        public class Factory
        {
            private readonly MessageView _prefab;

            public Factory(MessageView prefab)
            {
                _prefab = prefab;
            }

            public MessageView Create(Message text, Transform root)
            {
                var instance = Instantiate(_prefab, root);
                instance.SetMessage(text.Text);
                return instance;
            }
        }
    }
}