using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessagesHistory
{
    public class MessagesHistoryView : MonoBehaviour, IMessagesHistoryView
    {
        [SerializeField] private MessageView messageViewPrefab;

        [SerializeField] private RectTransform root;
        [SerializeField] private RectTransform[] parents;

        [SerializeField] private float maxHeight = 700;

        private Dictionary<Message, MessageView> _messagesCache;

        private MessageView.Factory _messagesFactory;

        private void Awake()
        {
            _messagesCache = new Dictionary<Message, MessageView>();
            _messagesFactory = new MessageView.Factory(messageViewPrefab);
        }

        private void Start()
        {
            StartCoroutine(UpdateCanvasHeightNextFrame());
        }

        public void UpdateMessages(IReadOnlyList<Message> messages)
        {
            foreach (var message in messages)
            {
                if (!_messagesCache.ContainsKey(message))
                {
                    _messagesCache.Add(message, _messagesFactory.Create(message, root));

                    StartCoroutine(UpdateCanvasHeightNextFrame());
                }
            }
        }

        private IEnumerator UpdateCanvasHeightNextFrame()
        {
            yield return null;

            float y = Mathf.Clamp(root.sizeDelta.y, 0, maxHeight);
            foreach (var parent in parents)
            {
                var sizeDelta = parent.sizeDelta;
                sizeDelta.y = y;
                parent.sizeDelta = sizeDelta;
            }
        }
    }
}