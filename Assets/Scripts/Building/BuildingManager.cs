using BuildingSystem;
using Selection;
using UnityEngine;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private GameObject locationInHierarchy; // Позиция объекта в иерархии редактора
        private GridManager _gridManager;

        // Ссылки
        private SelectionSystem _selectionSystem;

        private void Awake()
        {
            _selectionSystem = FindAnyObjectByType<SelectionSystem>();
            _gridManager = FindAnyObjectByType<GridManager>();
        }

        public void BuildConstruction(GameObject prefab, Vector3 buildingPreviewPosition)
        {
            // Создаем постройку
            var building = Instantiate(prefab, locationInHierarchy.transform);

            // Перемещаем в позицию проекции
            building.transform.position = buildingPreviewPosition;

            // Занимаем пространство на сетке
            _gridManager.PlaceObjectOnGrid(building.transform.position, building.GetComponent<BuildingData>());
        }

        public void DemolitionConstruction(GameObject building)
        {
            Destroy(building);

            _selectionSystem.ClearSelection();
            _selectionSystem.Unregister(building.GetComponent<Selectable>());

            _gridManager.DemolishObjectOnGrid(building);
        }
    }
}