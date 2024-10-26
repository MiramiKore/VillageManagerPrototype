using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Image = UnityEngine.UI.Image;

namespace Construction
{
    public class BuildingSystem : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<GameObject, GameObject> onBuild; // Событие постройки здания

        [SerializeField] private GameObject buildingCardPrefab; // Кнопка отвечающая за здание
        [SerializeField] private GameObject parentLocation; // Кнопка отвечающая за здание

        // Ссылки
        private BuildingProjection _buildingProjection;
        private ButtonHoldDetector _buttonHold;
        private BuildingManager _buildingManager;

        private void Awake()
        {
            _buildingProjection = FindObjectOfType<BuildingProjection>();
            _buildingManager = FindObjectOfType<BuildingManager>();

            GenerateBuildingCard();
        }

        private void GenerateBuildingCard()
        {
            foreach (var construction in _buildingManager.buildingsList)
            {
                var buildingInfo = construction.GetComponent<Building>();
                
                // Создаем карточку на сцене
                var card = Instantiate(buildingCardPrefab, parentLocation.transform);
                
                // Название постройки
                card.transform.Find("BuildingName").GetComponent<TextMeshProUGUI>().text = buildingInfo.title;

                // Установка изображения карточки
                card.GetComponent<Image>().sprite = buildingInfo.image;

                // Кнопка отвечающая за постройку конкретного объекта
                var buttonHold = card.GetComponent<ButtonHoldDetector>();
                buttonHold.onButtonHoldStart.AddListener(() => StartProjection(buttonHold, construction));
            }
        }

        // Запускаем корутину проекции
        private void StartProjection(ButtonHoldDetector buttonHold, GameObject construction)
        {
            StartCoroutine(UpdateProjection(buttonHold, construction));
        }

        // Управляем отображением проекции
        private IEnumerator UpdateProjection(ButtonHoldDetector buttonHold, GameObject construction)
        {
            // Пока кнопка удерживается отображается проекцию
            while (buttonHold.IsButtonHeld)
            {
                _buildingProjection.ShowProjectionBuilding(construction);
                yield return null;
            }

            // Когда удержание закончено проекция скрывается
            var projectionObject = _buildingProjection.HideProjectionBuilding();
            
            OnBuild(construction, projectionObject);
        }

        // Пробужаем событие строительства
        private void OnBuild(GameObject construction, GameObject projectionObject)
        {
            onBuild.Invoke(construction, projectionObject);
        }
    }
}