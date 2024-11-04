using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace BuildingSystem
{
    public class BuildingsController : MonoBehaviour
    {
        // Управление потсройкой
        [HideInInspector] public UnityEvent<Vector3, BuildingData> constructionIsCompleted = new();
        [HideInInspector] public UnityEvent<GameObject> demolitionIsCompleted = new();
        
        // Ссылки
        [SerializeField] private GridManager gridManager;
        [SerializeField] private BuildingPreview buildingPreview;
        [SerializeField] private BuildingManager buildingManager;

        private void Awake()
        {
            var buildingModeUI = FindFirstObjectByType<BuildingModeUI>();
            buildingModeUI.buttonOnCardPress.AddListener(StartPreviewBuilding);
        }

        // Запускаем корутину проекции
        private void StartPreviewBuilding(ButtonHoldDetector buttonHold, GameObject construction)
        {
            StartCoroutine(BuildingPreviewUpdate(buttonHold, construction));
        }

        // Управление 
        private IEnumerator BuildingPreviewUpdate(ButtonHoldDetector buttonHold, GameObject prefab)
        {
            // Пока кнопка удерживается отображаем проекцию
            while (buttonHold.IsButtonHeld)
            {
                buildingPreview.ShowBuildingPreview(prefab);
                yield return null;
            }
            
            // Когда удержание закончено проекция скрывается
            var buildingPreviewPosition = buildingPreview.HideBuildingPreview();
            
            var prefabBuildingData = prefab.GetComponent<BuildingData>();

            // Строим здание, если его можно размесить в данной позиции
            if (gridManager.CanPlaceObjectOnGrid(buildingPreviewPosition, prefabBuildingData))
                buildingManager.Build(prefab, buildingPreviewPosition);
            else
                Debug.Log("Невозможно разместить постройку");
        }
    }
}