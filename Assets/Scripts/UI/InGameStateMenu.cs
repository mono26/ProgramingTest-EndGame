using EndGame.Test.Events;
using TMPro;
using UnityEngine;

namespace EndGame.Test.UI
{
    public class InGameStateMenu : Singleton<InGameStateMenu>
    {
        [SerializeField]
        private TextMeshProUGUI stateTitle;

        public void OnMainMenuPressed()
        {
            OnUIButtonPressedEventArgs args = new OnUIButtonPressedEventArgs()
            {
                buttonPressed = "mainmenu"
            };

            EventController.PushEvent(UIEvents.BUTTON_PRESSED, args);

            gameObject.SetActive(false);
        }

        public void OnReplayPressed()
        {
            OnUIButtonPressedEventArgs args = new OnUIButtonPressedEventArgs()
            {
                buttonPressed = "replay"
            };

            EventController.PushEvent(UIEvents.BUTTON_PRESSED, args);

            gameObject.SetActive(false);
        }

        public static void ShowStateScreen(string _stateTitle)
        {
            GetUniqueInstance.stateTitle.text = _stateTitle;
            GetUniqueInstance.gameObject.SetActive(true);
        }
    }
}
