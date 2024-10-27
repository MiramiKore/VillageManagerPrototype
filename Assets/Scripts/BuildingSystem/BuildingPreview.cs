using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private GameObject buildingObjectPreview; // Объект с проекцией постройки

        [SerializeField] private LayerMask groundLayer; // Слой земли, по которому будет перемещаться объект

        [SerializeField] private GameObject gridDisplay;

        private MeshFilter _buildingObjectPreviewMesh;

        private Grid _grid;

        private void Awake()
        {
            _buildingObjectPreviewMesh = buildingObjectPreview.GetComponent<MeshFilter>();
            _grid = FindObjectOfType<Grid>();
        }

        // Включить проекцию постройки
        // ReSharper disable Unity.PerformanceAnalysis
        public void ShowBuildingPreview(GameObject building)
        {
            buildingObjectPreview.SetActive(true);
            
            // Включаем сетку
            gridDisplay.SetActive(true);
            
            // Заменяем модель объекта предпросмотра на модель постройки
            var buildingMesh = building.GetComponent<MeshFilter>();
            _buildingObjectPreviewMesh.mesh = buildingMesh.sharedMesh;

            // Изменям размер проекции на размер объекта
            var buildingScale = building.gameObject;
            _buildingObjectPreviewMesh.transform.localScale = buildingScale.transform.localScale;

            MovePreviewToPointerPosition(building);
        }

        // Спрятать проекцию постройки и вернуть ее местоположение
        public Vector3 HideBuildingPreview()
        {
            buildingObjectPreview.SetActive(false);
            
            // Отключаем сетку
            gridDisplay.SetActive(false);

            return buildingObjectPreview.transform.position;
        }

        // Перемещать проекцию в позицию указателя
        private void MovePreviewToPointerPosition(GameObject building)
        {
            // Создаем луч от камеры к позиции курсора
            // ReSharper disable once PossibleNullReferenceException
            var ray = Camera.main.ScreenPointToRay(Pointer.current.position.value);

            // Проверяем попадание луча в землю
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
            {
                // Получаем точку, на которую указывает луч
                var targetPosition = hit.point;

                // Находим ближайшую точку на сетке
                var cellCenterPosition = _grid.GetCellCenterWorld(_grid.WorldToCell(targetPosition));
                
                // Обновляем позицию объекта
                var projectionTransform = buildingObjectPreview.transform;
                projectionTransform.position =
                    new Vector3(cellCenterPosition.x, building.transform.position.y, cellCenterPosition.z);
            }
        }
    }
}