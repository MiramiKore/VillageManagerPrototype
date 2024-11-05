using System.Collections.Generic;
using System.Linq;
using BuildingSystem;
using UnityEngine;

namespace Building
{
    public class GridManager : MonoBehaviour
    {
        // Занятые постройками ячейки
        private readonly Dictionary<Vector2Int, GameObject> _occupiedCells = new();

        private Grid _grid;

        private void Awake()
        {
            _grid = FindAnyObjectByType<Grid>();
        }

        // Проверям свободны ли ячейки для объекта нужного размера
        public bool CanPlaceObjectOnGrid(Vector3 buildingPosition, BuildingData buildingData)
        {
            foreach (var cellPosition in GetOccupiedCells(buildingPosition, buildingData))
                if (_occupiedCells.ContainsKey(cellPosition))
                    return false;

            return true;
        }

        // Размещаем объект на сетке
        public void PlaceObjectOnGrid(Vector3 buildingPosition, BuildingData buildingData)
        {
            foreach (var cellPosition in GetOccupiedCells(buildingPosition, buildingData))
                _occupiedCells[cellPosition] = buildingData.gameObject;
        }

        // Удаляем объект с сетки
        public void DemolishObjectOnGrid(GameObject building)
        {
            // Добавляем ключи занятых ячеек в сиписок для удаления
            var keysToRemove = _occupiedCells
                .Where(build => build.Value == building)
                .Select(build => build.Key)
                .ToList();

            // Удаляем ключи занятых ячеек
            foreach (var key in keysToRemove)
                _occupiedCells.Remove(key);
        }

        // Получаем все ячейки занимаемые объектом вокруг базовой ячейки
        private IEnumerable<Vector2Int> GetOccupiedCells(Vector3 buildingPosition, BuildingData buildingData)
        {
            // Положение ячейки в которой находиться постройка
            var baseCellPosition = _grid.WorldToCell(buildingPosition);

            // Вычисляем половину размера постройки для коректного заполения вокруг базовой ячейки
            var halfWidth = Mathf.FloorToInt(buildingData.size.x / 2f);
            var halfHeight = Mathf.FloorToInt(buildingData.size.y / 2f);

            // Вычисляем кол-во ячеек под постройку в зависимости от размера
            for (var x = -halfWidth; x <= halfWidth; x++)
            for (var z = -halfHeight; z <= halfHeight; z++)
                yield return new Vector2Int(baseCellPosition.x + x, baseCellPosition.y + z);
        }
    }
}