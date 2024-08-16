using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MessageBox
{
    public class MessageBoxView : MonoBehaviour, IMessageBoxView
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_Text agreeText;
        [SerializeField] private Button agreeButton;

        private void Awake()
        {
            agreeButton.onClick.AddListener(OnAgreeClick);
        }

        public void ShowMessage(string message, string agree)
        {
            gameObject.SetActive(true);
            messageText.text = message;
            agreeText.text = agree;
            agreeButton.Select();
        }

        private void OnAgreeClick()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            agreeButton.onClick.RemoveAllListeners();
        }
    }
}