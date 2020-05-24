using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.UI
{
    public class MainStateMenu : Singleton<MainStateMenu>
    {
        public void OnPlayGamePressed()
        {
            OnUIButtonPressedEventArgs args = new OnUIButtonPressedEventArgs()
            {
                buttonPressed = "playgame"
            };

            EventController.QueueEvent(UIEvents.BUTTON_PRESSED, args);
        }

        public void OnQuitPressed()
        {
            OnUIButtonPressedEventArgs args = new OnUIButtonPressedEventArgs()
            {
                buttonPressed = "quit"
            };

            EventController.QueueEvent(UIEvents.BUTTON_PRESSED, args);
        }
    }
}
