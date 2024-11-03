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
        private void Build(GameObject building, Vector3 buildingPreviewPosition)
        {
            var canBuild = _gridManager.PlaceObject(buildingPreviewPosition, building.GetComponent<BuildingData>());

            if (!canBuild) return;
            
            var currentBuilding = Instantiate(building, locationInHierarchy.transform);
            currentBuilding.transform.position = buildingPreviewPosition;

            buildingsQueue.Add(currentBuilding);
        }
    }
}