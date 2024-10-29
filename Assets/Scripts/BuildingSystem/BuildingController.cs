using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace BuildingSystem
{
    public class BuildingController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<GameObject, Vector3> onBuild; // Событие постройки здания

        // Ссылки
        private BuildingPreview _buildingPreview;

        private void Awake()
        {
            _buildingPreview = FindFirstObjectByType<BuildingPreview>();

            var buildingModeUI = FindFirstObjectByType<BuildingModeUI>();
            buildingModeUI.buttonOnCardPress.AddListener(StartPreviewBuilding);
        }

        // Запускаем корутину проекции
        private void StartPreviewBuilding(ButtonHoldDetector buttonHold, GameObject construction)
        {
            StartCoroutine(BuildingPreviewUpdate(buttonHold, construction));
        }

        // Управляем отображением проекции
        private IEnumerator BuildingPreviewUpdate(ButtonHoldDetector buttonHold, GameObject construction)
        {
            // Пока кнопка удерживается отображается проекцию
            while (buttonHold.IsButtonHeld)
            {
                _buildingPreview.ShowBuildingPreview(construction);
                yield return null;
            }

            // Когда удержание закончено проекция скрывается
            var buildingPreviewPosition = _buildingPreview.HideBuildingPreview();

            TriggerBuildEvent(construction, buildingPreviewPosition);
        }

        // Пробужаем событие строительства
        private void TriggerBuildEvent(GameObject construction, Vector3 buildingPreviewPosition)
        {
            onBuild.Invoke(construction, buildingPreviewPosition);
        }
    }
}