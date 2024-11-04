using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> constructedBuildings; // Очередь построек
        [SerializeField] private GameObject locationInHierarchy; // Позиция объекта в иерархии редактора

        private BuildingsController _buildingsController;

        private void Awake()
        {
            _buildingsController = FindFirstObjectByType<BuildingsController>();
        }
        
        // Строим здание
        public void Build(GameObject prefab, Vector3 buildingPreviewPosition)
        {
            // Создаем и перемещаем постройку в позицию префаба
            var building = Instantiate(prefab, locationInHierarchy.transform);
            building.transform.position = buildingPreviewPosition;
            
            // Добавляем здание в список построенных
            constructedBuildings.Add(building);
            
            // Вызываем ивент завершения постройки
            var buildingData = building.GetComponent<BuildingData>();
            _buildingsController.constructionIsCompleted.Invoke(building.transform.position, buildingData);
        }

        // Сносим здание
        public void Demolition(GameObject building)
        {
            Destroy(building);
            
            // Вызываем ивент завершения сноса постройки
            _buildingsController.demolitionIsCompleted.Invoke(building);
        }
    }
}