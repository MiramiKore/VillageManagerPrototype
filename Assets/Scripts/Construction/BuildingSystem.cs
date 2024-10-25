using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Construction
{
    public class BuildingSystem : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Building, GameObject> onBuild; // Событие постройки здания
    
        [SerializeField] private GameObject buildingPrefab; // Префаб здания
        [SerializeField] private GameObject buildingButton; // Кнопка отвечающая за здание

        // Ссылки
        private BuildingProjection _buildingProjection;
        private ButtonHoldDetector _buttonHold;
        private Building _building;

        private void Awake()
        {
            _building = buildingPrefab.GetComponent<Building>();
            _buttonHold = buildingButton.GetComponent<ButtonHoldDetector>();
            _buildingProjection = FindObjectOfType<BuildingProjection>();

            _buttonHold.onButtonHoldStart.AddListener(StartProjection);
        }

        // Запускаем корутину проекции
        private void StartProjection()
        {
            StartCoroutine(UpdateProjection());
        }

        // Управляем отображением проекции
        private IEnumerator UpdateProjection()
        {
            // Пока кнопка удерживается, показываем проекцию
            while (_buttonHold.IsButtonHeld)
            {
                _buildingProjection.ShowProjectionBuilding();
                yield return null;
            }

            // Когда удержание закончено, скрываем проекцию
            var projectionObject = _buildingProjection.HideProjectionBuilding();

            OnBuild(projectionObject);
        }

        private void OnBuild(GameObject projectionObject)
        {
            onBuild.Invoke(_building, projectionObject);
        }
    }
}