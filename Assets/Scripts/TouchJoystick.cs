using UnityEngine;
using UnityEngine.EventSystems;

namespace EndGame.Test.UI
{
    public class TouchJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        protected float knobRange = 1.5f;
        [SerializeField, Range(0, 1.0f)]
        protected float maxJoystickValue = 1.0f;

        [SerializeField]
        protected RectTransform joystickHolder;

        [SerializeField]
        protected Vector2 joystickValue;
        protected Vector3 newTargetPosition;
        protected Vector3 newJoystickPosition;

        public Vector2 GetJoystickValue { get => joystickValue; }

        public void OnDrag(PointerEventData eventData)
        {
            newTargetPosition = eventData.position;
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition - joystickHolder.position, knobRange);

            joystickValue.x = EvaluateInputValue(newTargetPosition.x / knobRange);
            joystickValue.y = EvaluateInputValue(newTargetPosition.y / knobRange);

            newJoystickPosition = joystickHolder.position + newTargetPosition;
            transform.position = newJoystickPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            newJoystickPosition = joystickHolder.position;
            transform.position = newJoystickPosition;
            joystickValue.x = 0f;
            joystickValue.y = 0f;
        }

        private float EvaluateInputValue(float vectorPosition)
        {
            Debug.Log("Evaluating value: " + vectorPosition);

            return Mathf.InverseLerp(0, maxJoystickValue, Mathf.Abs(vectorPosition)) * Mathf.Sign(vectorPosition);
        }
    }
}
