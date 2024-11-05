using Selection;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundClickDetector : MonoBehaviour
{
    private SelectionSystem _selectionSystem;

    private void Awake()
    {
        _selectionSystem = FindAnyObjectByType<SelectionSystem>();
    }

    private void OnMouseDown()
    {
        // Проверяем находится ли курсор над интерфейсом
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _selectionSystem.ClearSelection();
    }
}