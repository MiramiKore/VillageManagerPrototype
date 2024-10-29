using BuildingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class BuildingModeUI : MonoBehaviour
    {
        // Событие отвечающее за нажатие по карточке постройки
        [HideInInspector] public UnityEvent<ButtonHoldDetector, GameObject> buttonOnCardPress;
        
        [SerializeField] private GameObject buildingPrefabCard; // Кнопка отвечающая за здание
        
        [SerializeField] private GameObject locationInHierarchy; // Положение постройки в иерархии редактора
        
        // Ссылки
        private BuildingRegistry _buildingRegistry;
        
        private void Awake()
        {
            _buildingRegistry = FindAnyObjectByType<BuildingRegistry>();
            
            GenerateBuildingCards();
        }
        
        // Создаем карточки зданий
        private void GenerateBuildingCards()
        {
            foreach (var construction in _buildingRegistry.buildingsList)
            {
                var buildingInfo = construction.GetComponent<BaseBuilding>();

                // Создаем карточку на сцене
                var card = Instantiate(buildingPrefabCard, locationInHierarchy.transform);

                // Название постройки
                card.transform.Find("BuildingName").GetComponent<TextMeshProUGUI>().text = buildingInfo.title;

                // Установка изображения карточки
                card.GetComponent<Image>().sprite = buildingInfo.image;

                // Кнопка отвечающая за постройку конкретного объекта
                var buttonHold = card.GetComponent<ButtonHoldDetector>();
                buttonHold.onButtonHoldStart.AddListener(() => buttonOnCardPress.Invoke(buttonHold, construction));
            }
        }
    }
}