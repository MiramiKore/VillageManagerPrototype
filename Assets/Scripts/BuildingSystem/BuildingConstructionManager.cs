using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingConstructionManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> buildingsQueue; // Очередь построек

        [SerializeField] private GameObject locationInHierarchy; // Позиция объекта в иерархии редактора

        private void Awake()
        {
            var buildingSystem = FindObjectOfType<BuildingController>();
            buildingSystem.onBuild.AddListener(Build);
        }

        // Возводим постройку
        private void Build(GameObject building, Vector3 buildingPreviewPosition)
        {
            var currentBuilding = Instantiate(building, locationInHierarchy.transform);
            currentBuilding.transform.position = buildingPreviewPosition;

            buildingsQueue.Add(currentBuilding);
        }
    }
}