using UnityEngine;

namespace BuildingSystem
{
    public class BuildingData : MonoBehaviour
    {
        [Header("Данные")] public string title; // Название постройки
        public Sprite image; // Изображение постройки
        public Vector2Int size; // Размер постройки

        [Header("Параметры")] public int currentLevel; // Текущий уровень постройки
    }
}