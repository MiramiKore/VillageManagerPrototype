using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Selection
{
    public class SelectionSystem : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Selectable> objectSelected = new();
        [HideInInspector] public UnityEvent objectDeselected;

        private readonly List<Selectable> _selectables = new();

        // Регистрируем объект как "Selectable", добавляя его в список
        public void Register(Selectable selectable)
        {
            _selectables.Add(selectable);
        }

        public void Unregister(Selectable selectable)
        {
            _selectables.Remove(selectable);
        }

        public void ClearSelection()
        {
            _selectables.ForEach(s => s.Deselect());
        }

        public void OnSelected(Selectable selectable)
        {
            objectSelected.Invoke(selectable);
        }

        public void OnDeselected()
        {
            objectDeselected.Invoke();
        }
    }
}