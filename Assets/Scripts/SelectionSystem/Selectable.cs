using UnityEngine;
using UnityEngine.EventSystems;

namespace SelectionSystem
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;

        private SelectionManager _manager;

        private void Awake()
        {
            _manager = FindAnyObjectByType<SelectionManager>();
        }
        
        private void Start()
        {
            _manager.Register(this);
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            _manager.ClearSelection();
            
            Select();
        }

        private void Select()
        {
            indicator.SetActive(true);
            
            _manager.OnSelected(this);
        }

        public void Deselect()
        {
            indicator.SetActive(false);
            
            _manager.OnDeselected();
        }
    }
}