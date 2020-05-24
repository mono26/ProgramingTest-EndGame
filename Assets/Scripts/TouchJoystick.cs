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
        private float minTimeForDrag = 0.3f;

        [SerializeField]
        private RectTransform joystickHolder;

        [SerializeField]
        private Vector2 joystickValue;
        private Vector3 newTargetPosition;
        private Vector3 newJoystickPosition;

        private float clickedTime = 0.0f;
        private bool isTapped = false;
        private bool isDragged = false;
        private bool isUp = false;

        public Vector2 GetJoystickValue { get => joystickValue; }
        public bool GetIsTapped { get => isTapped; }
        public bool GetIsDragged { get => isDragged; }
        public bool GetIsUp { get => isUp; }

        private void Start()
        {
            clickedTime = 0.0f;
        }

        private void LateUpdate()
        {
            isTapped = false;
            isDragged = false;
            isUp = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (clickedTime >= minTimeForDrag)
            {
                isDragged = true;
            }

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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (clickedTime < minTimeForDrag)
            {
                isTapped = true;

                clickedTime += Time.deltaTime;
            }
            else
            {
                isTapped = false;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isTapped = false;

            isUp = true;

            clickedTime = 0.0f;
        }

        private float EvaluateInputValue(float vectorPosition)
        {
            Debug.Log("Evaluating value: " + vectorPosition);

            return Mathf.InverseLerp(0, maxJoystickValue, Mathf.Abs(vectorPosition)) * Mathf.Sign(vectorPosition);
        }
    }
}
