using UnityEngine;
using UnityEngine.EventSystems;

namespace EndGame.Test.UI
{
    public class TouchJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private float knobRange = 1.5f;
        [SerializeField, Range(0, 1.0f)]
        private float maxJoystickValue = 1.0f;

        [SerializeField]
        private RectTransform joystickHolder;

        private Vector2 joystickValue;
        private Vector3 newTargetPosition;
        private Vector3 newJoystickPosition;


        private bool isTapped = false;
        private bool isDragged = false;
        private bool isUp = false;

        public Vector2 GetJoystickValue { get => joystickValue; }
        public bool GetIsTapped { get => isTapped; }
        public bool GetIsDragged { get => isDragged; }
        public bool GetIsUp { get => isUp; }

        private void LateUpdate()
        {
            if (isUp)
            {
                isTapped = false;
                isDragged = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            isDragged = true;

            isTapped = false;

            newTargetPosition = eventData.position;
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition - joystickHolder.position, knobRange);

            // Evaluate the values based on the position of the knob and the knob range.
            joystickValue.x = EvaluateInputValue(newTargetPosition.x / knobRange);
            joystickValue.y = EvaluateInputValue(newTargetPosition.y / knobRange);

            newJoystickPosition = joystickHolder.position + newTargetPosition;
            transform.position = newJoystickPosition;
        }

        /// <summary>
        /// Reset the know position and value.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            newJoystickPosition = joystickHolder.position;
            transform.position = newJoystickPosition;
            joystickValue.x = 0f;
            joystickValue.y = 0f;
        }

        /// <summary>
        /// The first time the player taps the joystick.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            isTapped = true;

            isUp = false;

            Debug.Log("Down joystick");
        }

        /// <summary>
        /// When the player stops tapping the joystick.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            isUp = true;
        }

        private float EvaluateInputValue(float vectorPosition)
        {
            return Mathf.InverseLerp(0, maxJoystickValue, Mathf.Abs(vectorPosition)) * Mathf.Sign(vectorPosition);
        }
    }
}
