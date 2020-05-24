using EndGame.Test.Events;
using TMPro;
using UnityEngine;

namespace EndGame.Test.UI
{
    public class InGameStateMenu : Singleton<InGameStateMenu>
    {
        [SerializeField]
        private TextMeshProUGUI stateTitle = null;
        [SerializeField]
        private GameObject uiContainer = null;

        public void OnMainMenuPressed()
        {
            OnUIButtonPressedEventArgs args = new OnUIButtonPressedEventArgs()
            {
                buttonPressed = "mainmenu"
            };

            EventController.QueueEvent(UIEvents.BUTTON_PRESSED, args);

            uiContainer.SetActive(false);
        }

        public void OnReplayPressed()
        {
            OnUIButtonPressedEventArgs args = new OnUIButtonPressedEventArgs()
            {
                buttonPressed = "replay"
            };

            EventController.QueueEvent(UIEvents.BUTTON_PRESSED, args);

            uiContainer.SetActive(false);
        }

        public static void ShowStateScreen(string _stateTitle)
        {
            GetUniqueInstance.stateTitle.text = _stateTitle;
            GetUniqueInstance.uiContainer.SetActive(true);
        }
    }
}
