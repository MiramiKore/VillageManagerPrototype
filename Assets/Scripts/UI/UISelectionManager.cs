using SelectionSystem;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UISelectionManager : MonoBehaviour
    {
        // Событие вызываемое при выборе объектов с тегом "Construction"
        [HideInInspector] public UnityEvent<Selectable> onConstructionSelected = new();

        private SelectionManager _manager;

        private void Awake()
        {
            _manager = FindAnyObjectByType<SelectionManager>();
        }

        private void OnEnable()
        {
            _manager.objectSelected.AddListener(UIObjectSelector);
        }

        private void OnDisable()
        {
            _manager.objectSelected.RemoveAllListeners();
        }

        // Управляем интрфейсом для выбранного объекта
        private void UIObjectSelector(Selectable selectable)
        {
            if (selectable.CompareTag("Construction"))
            {
                onConstructionSelected.Invoke(selectable);
            }
        }
    }
}