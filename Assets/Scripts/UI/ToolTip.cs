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

        private WaitForSeconds activationWait = null;

        private void Start()
        {
            activationWait = new WaitForSeconds(3.0f);
        }

        private void DisplayTextObject(string _textToDisplay)
        {
            textComponent.text = _textToDisplay;

            gameObject.SetActive(true);

            StartCoroutine(TimedActivation());
        }

        private IEnumerator TimedActivation()
        {
            yield return activationWait;

            gameObject.SetActive(false);
        }

        public static void DisplayToolTip(string _textToDisplay)
        {
            GetUniqueInstance.DisplayTextObject(_textToDisplay);
        }
    }
}
