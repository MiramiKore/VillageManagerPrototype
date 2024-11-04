using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SelectionSystem
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private List<Selectable> _selectables = new();

        [HideInInspector] public UnityEvent<Selectable> objectSelected = new();
        [HideInInspector] public UnityEvent objectDeselected;
        
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