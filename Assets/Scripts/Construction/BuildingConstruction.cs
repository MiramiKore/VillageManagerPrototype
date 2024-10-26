using System.Collections.Generic;
using UnityEngine;

namespace Construction
{
    public class BuildingConstruction : MonoBehaviour
    {
        [SerializeField] private List<GameObject> buildQueue;
    
        [SerializeField] private GameObject parentLocation;
    
        private void Awake()
        {
            var buildingSystem = FindObjectOfType<BuildingSystem>();
            buildingSystem.onBuild.AddListener(Build);
        }

        private void Build(GameObject building, GameObject projection)
        {
            var currentBuilding = Instantiate(building, parentLocation.transform);
            currentBuilding.transform.position = projection.transform.position;
        
            buildQueue.Add(currentBuilding);
        }
    }
}