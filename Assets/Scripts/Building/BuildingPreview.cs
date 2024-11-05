using UnityEngine;
using UnityEngine.InputSystem;

namespace Building
{
    public class BuildingPreview : MonoBehaviour
    {
        [SerializeField] private GameObject buildingObjectPreview; // Проекция постройки

        [SerializeField] private LayerMask groundLayer; // Слой земли по которому перемещается объект

        [SerializeField] private GameObject gridDisplay; // Объект с шейдером сетки

        // Ссылки
        private MeshFilter _buildingObjectPreviewMesh;
        private Grid _grid;

        private void Awake()
        {
            _buildingObjectPreviewMesh = buildingObjectPreview.GetComponent<MeshFilter>();

            _grid = FindAnyObjectByType<Grid>();

            if (_grid == null)
                Debug.LogError("Component Grid not found");
        }

        // Включаем проекцию постройки
        public void ShowBuildingPreview(GameObject building)
        {
            buildingObjectPreview.SetActive(true);
            gridDisplay.SetActive(true);

            // Заменяем модель проекции на модель постройки
            var buildingMesh = building.GetComponent<MeshFilter>();
            _buildingObjectPreviewMesh.mesh = buildingMesh.sharedMesh;

            // Изменям размер проекции на размер объекта
            buildingObjectPreview.transform.localScale = building.transform.localScale;
        }

        // Отключаем проекцию постройки
        public Vector3 HideBuildingPreview()
        {
            buildingObjectPreview.SetActive(false);
            gridDisplay.SetActive(false);

            // Возвращаем ее положение проекции
            return buildingObjectPreview.transform.position;
        }

        // Обновляем позицию проекции в текущем положении указателя
        public Vector3 UpdateBuildingPreviewPosition()
        {
            var ray = Camera.main.ScreenPointToRay(Pointer.current.position.value);

            // Проверяем попадание луча в землю
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
            {
                var cellCenterPosition = _grid.GetCellCenterWorld(_grid.WorldToCell(hit.point));

                // Обновляем позицию объекта
                var transformPreviewObject = buildingObjectPreview.transform;

                transformPreviewObject.position =
                    new Vector3(cellCenterPosition.x, transformPreviewObject.position.y, cellCenterPosition.z);
            }

            return buildingObjectPreview.transform.position;
        }
    }
}