using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingSystem
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private GameObject buildingObjectPreview; // Объект с проекцией постройки

        [SerializeField] private LayerMask groundLayer; // Слой земли, по которому будет перемещаться объект

        private MeshFilter _buildingObjectPreviewMesh;

        private void Awake()
        {
            _buildingObjectPreviewMesh = buildingObjectPreview.GetComponent<MeshFilter>();
        }

        // Включить проекцию постройки
        // ReSharper disable Unity.PerformanceAnalysis
        public void ShowBuildingPreview(GameObject building)
        {
            buildingObjectPreview.SetActive(true);

            // Заменяем модель объекта предпросмотра на модель постройки
            var buildingMesh = building.GetComponent<MeshFilter>();
            _buildingObjectPreviewMesh.mesh = buildingMesh.sharedMesh;

            MovePreviewToPointerPosition(building);
        }

        // Спрятать проекцию постройки и вернуть ее местоположение
        public Vector3 HideBuildingPreview()
        {
            buildingObjectPreview.SetActive(false);

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

                // Обновляем позицию объекта
                var projectionTransform = buildingObjectPreview.transform;
                projectionTransform.position =
                    new Vector3(targetPosition.x, building.transform.position.y, targetPosition.z);
            }
        }
    }
}