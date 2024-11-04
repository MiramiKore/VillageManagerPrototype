using SelectionSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundClickDetector : MonoBehaviour
{
    private SelectionManager _selectionManager;

    private void Awake()
    {
        _selectionManager = FindAnyObjectByType<SelectionManager>();
    }

    private void OnMouseDown()
    {
        // Проверяем находится ли курсор над интерфейсом
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        _selectionManager.ClearSelection();
    }
}