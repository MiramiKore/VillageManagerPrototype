using UnityEngine;
using UnityEngine.EventSystems;

namespace Selection
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;

        private SelectionSystem _system;

        private void Awake()
        {
            _system = FindAnyObjectByType<SelectionSystem>();
        }

        private void Start()
        {
            _system.Register(this);
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            _system.ClearSelection();

            Select();
        }

        private void Select()
        {
            indicator.SetActive(true);

            _system.OnSelected(this);
        }

        public void Deselect()
        {
            indicator.SetActive(false);

            _system.OnDeselected();
        }
    }
}