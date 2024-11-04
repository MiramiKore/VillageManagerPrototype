using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingConstructionManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> buildingsQueue; // Очередь построек

        [SerializeField] private GameObject locationInHierarchy; // Позиция объекта в иерархии редактора

        private GridManager _gridManager;

        private void Awake()
        {
            var buildingSystem = FindFirstObjectByType<BuildingController>();
            buildingSystem.onBuild.AddListener(Build);

            _gridManager = FindFirstObjectByType<GridManager>();
        }

        // Возводим постройку
        private void Build(GameObject prefab, Vector3 prefabPreviewPosition)
        {
            if (_gridManager.CanPlaceObject(prefabPreviewPosition, prefab.GetComponent<BuildingData>()))
            {
                var building = Instantiate(prefab, locationInHierarchy.transform);
                building.transform.position = prefabPreviewPosition;

                var buildingData = building.GetComponent<BuildingData>();
                buildingData.destroy.AddListener(DestroyBuild);
            
                _gridManager.PlaceObject(building.transform.position, buildingData);

                buildingsQueue.Add(building);
            }
            else
                Debug.Log("Невозможно разместить здание");
        }

        private void DestroyBuild(BuildingData buildingData)
        {
            _gridManager.DestroyObject(buildingData.gameObject);
            Destroy(buildingData.gameObject);
        }
    }
}