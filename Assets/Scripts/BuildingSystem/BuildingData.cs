using UnityEngine;
using UnityEngine.Events;

namespace BuildingSystem
{
    public class BuildingData : MonoBehaviour
    {
        public UnityEvent<BuildingData> destroy = new();
        
        [Header("Данные")] public string title; // Название постройки
        public Sprite image; // Изображение постройки
        public Vector2Int size; // Размер постройки

        [Header("Параметры")] public int currentLevel; // Текущий уровень постройки
        
        private void OnMouseDown()
        {
            destroy.Invoke(this);
        }
    }
}