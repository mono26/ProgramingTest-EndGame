using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.UI
{
    public class UIEvents
    {
        public const string BUTTON_PRESSED = "event.ui.button.pressed";
    }

    public struct OnUIButtonPressedEventArgs : IEventArgs
    {
        public string buttonPressed;
    }
}
