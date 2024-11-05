using Selection;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UISelectionManager : MonoBehaviour
    {
        // Событие вызываемое при выборе объектов с тегом "Construction"
        [HideInInspector] public UnityEvent<Selectable> onConstructionSelected = new();

        private SelectionSystem _system;

        private void Awake()
        {
            _system = FindAnyObjectByType<SelectionSystem>();
        }

        private void OnEnable()
        {
            _system.objectSelected.AddListener(UIObjectSelector);
        }

        private void OnDisable()
        {
            _system.objectSelected.RemoveAllListeners();
        }

        // Управляем интрфейсом для выбранного объекта
        private void UIObjectSelector(Selectable selectable)
        {
            if (selectable.CompareTag("Construction")) onConstructionSelected.Invoke(selectable);
        }
    }
}