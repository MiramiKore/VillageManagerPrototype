using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private GameObject buildingObjectPreview; // Проекция постройки
        [SerializeField] private LayerMask groundLayer; // Слой земли по которому перемещается объект
        [SerializeField] private GameObject gridDisplay; // Объект с шейдером сетки

        [Header("Цвета проекции")] 
        [SerializeField] private Color greenColor;
        [SerializeField] private Color redColor;

        // Ссылки
        private MeshFilter _buildingObjectPreviewMesh;
        private Material _previewObjectMaterial;
        private GridManager _gridManager;
        private Grid _grid;

        private void Awake()
        {
            // Получаем материал и меш с проекции постройки
            _buildingObjectPreviewMesh = buildingObjectPreview.GetComponent<MeshFilter>();
            _previewObjectMaterial = buildingObjectPreview.GetComponent<Renderer>().material;

            _gridManager = FindFirstObjectByType<GridManager>();
            _grid = FindAnyObjectByType<Grid>();
        }

        // Включаем проекцию постройки
        // ReSharper disable Unity.PerformanceAnalysis
        public void ShowBuildingPreview(GameObject building)
        {
            buildingObjectPreview.SetActive(true);
            gridDisplay.SetActive(true);

            // Заменяем модель проекции на модель постройки
            var buildingMesh = building.GetComponent<MeshFilter>();
            _buildingObjectPreviewMesh.mesh = buildingMesh.sharedMesh;

            // Изменям размер проекции на размер объекта
            var buildingScale = building.gameObject;
            _buildingObjectPreviewMesh.transform.localScale = buildingScale.transform.localScale;

            MovePreviewToPointerPosition(building);
        }

        // Отключаем проекцию постройки и возвращаем ее положение
        public Vector3 HideBuildingPreview()
        {
            buildingObjectPreview.SetActive(false);
            gridDisplay.SetActive(false);

            return buildingObjectPreview.transform.position;
        }

        // Перемещаем проекцию по сетке в позицию указателя
        private void MovePreviewToPointerPosition(GameObject building)
        {
            // Создаем луч от камеры к позиции курсора
            var ray = Camera.main.ScreenPointToRay(Pointer.current.position.value);

            // Проверяем попадание луча в землю
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
            {
                // Находим ближайшую точку на сетке
                var cellCenterPosition = _grid.GetCellCenterWorld(_grid.WorldToCell(hit.point));

                // Обновляем позицию объекта
                var projectionTransform = buildingObjectPreview.transform;
                projectionTransform.position =
                    new Vector3(cellCenterPosition.x, building.transform.position.y, cellCenterPosition.z);

                // TODO: Костыль, не дожно находиться здесь
                PlaceIsAvailable(cellCenterPosition, building);
            }
        }

        // Проверяем находиться ли проекция объекта в свободной ячейки
        private void PlaceIsAvailable(Vector3 cellCenterPosition, GameObject building)
        {
            var buildingData = building.GetComponent<BuildingData>();
            
            var canPlaced = _gridManager.CanPlaceObjectOnGrid(cellCenterPosition, buildingData);
            
            // Меняем цвет проекции в зависимости от, возможно ли разместить в данной позиции
            _previewObjectMaterial.color = canPlaced ? greenColor : redColor;
        }
    }
}