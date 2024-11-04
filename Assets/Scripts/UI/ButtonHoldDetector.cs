using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonHoldDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsButtonHeld { get; private set; }
        [HideInInspector] public UnityEvent onButtonHoldStart;
        [HideInInspector] public UnityEvent onButtonHoldEnd;

        public void OnPointerDown(PointerEventData eventData)
        {
            IsButtonHeld = true;
            onButtonHoldStart?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsButtonHeld = false;
            onButtonHoldEnd?.Invoke();
        }
    }
}