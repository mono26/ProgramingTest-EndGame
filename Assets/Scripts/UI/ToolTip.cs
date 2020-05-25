using System.Collections;
using TMPro;
using UnityEngine;

namespace EndGame.Test.UI
{
    public class ToolTip : Singleton<ToolTip>
    {
        [SerializeField]
        private float displayDuration = 3.0f;
        [SerializeField]
        private TextMeshProUGUI textComponent = null;
        [SerializeField]
        private GameObject uiContainer = null;

        private WaitForSeconds activationWait = null;

        private void Start()
        {
            activationWait = new WaitForSeconds(displayDuration);
        }

        private void DisplayTextObject(string _textToDisplay)
        {
            textComponent.text = _textToDisplay;

            uiContainer.SetActive(true);

            StartCoroutine(TimedActivation());
        }

        private IEnumerator TimedActivation()
        {
            yield return activationWait;

            uiContainer.SetActive(false);
        }

        /// <summary>
        /// Displays a tool message for the player.
        /// </summary>
        /// <param name="_textToDisplay"></param>
        public static void DisplayToolTip(string _textToDisplay)
        {
            GetUniqueInstance.DisplayTextObject(_textToDisplay);
        }
    }
}
