using System.Collections;
using BuildingSystem;
using UI;
using UnityEngine;

namespace Building
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private BuildingPreview buildingPreview;
        [SerializeField] private DisplayingFreeSpace displayingFreeSpace;
        [SerializeField] private GridManager gridManager;
        [SerializeField] private BuildingManager manager;

        private bool _canPlace;

        private void Awake()
        {
            var buildingModeUI = FindAnyObjectByType<BuildingModeUI>();
            buildingModeUI.buttonOnCardPress.AddListener(StartPreviewBuilding);

            var testBuildingUI = FindAnyObjectByType<TestBuildingUI>();
            testBuildingUI.destroyButtonActive.AddListener(manager.DemolitionConstruction);
        }

        // Запускаем корутину проекции
        private void StartPreviewBuilding(ButtonHoldDetector buttonHold, GameObject construction)
        {
            StartCoroutine(BuildingPreviewUpdate(buttonHold, construction));
        }

        // Управление 
        private IEnumerator BuildingPreviewUpdate(ButtonHoldDetector buttonHold, GameObject prefab)
        {
            var prefabData = IsButtonStart(prefab);

            // Пока кнопка удерживается перемещаем проекцию
            while (buttonHold.IsButtonHeld)
            {
                MoveConstruction(prefabData);
                yield return null;
            }

            IsButtonUp(prefab);
        }

        private BuildingData IsButtonStart(GameObject prefab)
        {
            // Меняем вид проекции на префаб
            buildingPreview.ShowBuildingPreview(prefab);

            var prefabData = prefab.GetComponent<BuildingData>();

            return prefabData;
        }

        private void IsButtonUp(GameObject prefab)
        {
            // Когда удержание закончено проекция скрывается
            var buildingPreviewPosition = buildingPreview.HideBuildingPreview();

            // Строим здание, если его можно размесить в данной позиции
            if (_canPlace)
                manager.BuildConstruction(prefab, buildingPreviewPosition);
            else
                Debug.Log("Невозможно разместить постройку");
        }

        private void MoveConstruction(BuildingData prefabData)
        {
            var previewPosition = buildingPreview.UpdateBuildingPreviewPosition();

            _canPlace = gridManager.CanPlaceObjectOnGrid(previewPosition, prefabData);
            displayingFreeSpace.IndicateFreeSpace(_canPlace);
        }
    }
}