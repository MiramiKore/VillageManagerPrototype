using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BuildingSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private List<Vector2Int> occupiedCells = new();
        
        // Материал проекции постройки и его цвета
        [SerializeField] private Material previewObjectMaterial;
        [SerializeField] private Color greenColor;
        [SerializeField] private Color redColor;
        
        private Grid _grid;

        private void Awake()
        {
            _grid = FindFirstObjectByType<Grid>();
        }

        // Метод проверки, свободны ли все ячейки для объекта нужного размера
        public bool CanPlaceObject(Vector3 buildingPosition, BuildingData buildingData)
        {
            var buildingPos = _grid.WorldToCell(buildingPosition);
            
            var halfWidth = Mathf.FloorToInt(buildingData.size.x / 2f);
            var halfHeight = Mathf.FloorToInt(buildingData.size.y / 2f);
            
            for (int x = -halfWidth; x <= halfWidth; x++)
            {
                for (int z = -halfHeight; z <= halfHeight; z++)
                {
                    var cellPosition = new Vector2Int(buildingPos.x + x, buildingPos.y + z);

                    if (occupiedCells.Contains(cellPosition))
                    {
                        previewObjectMaterial.color = redColor;
                        return false;
                    }
                }
            }

            previewObjectMaterial.color = greenColor;
            return true;
        }

        // Метод для размещения объекта в свободных ячейках
        public bool PlaceObject(Vector3 buildingPosition, BuildingData buildingData)
        {
            var baseCell = _grid.WorldToCell(buildingPosition);

            if (!CanPlaceObject(buildingPosition, buildingData))
            {
                Debug.Log("Невозможно разместить здание");
                return false;
            }
                
            var halfWidth = Mathf.FloorToInt(buildingData.size.x / 2f);
            var halfHeight = Mathf.FloorToInt(buildingData.size.y / 2f);

            // Заполняем ячейки вокруг центральной позиции объекта
            for (int x = -halfWidth; x <= halfWidth; x++)
            {
                for (int z = -halfHeight; z <= halfHeight; z++)
                {
                    var cellPosition = new Vector2Int(baseCell.x + x, baseCell.y + z);
                    occupiedCells.Add(cellPosition);
                }
            }

            return true;
        }
    }
}