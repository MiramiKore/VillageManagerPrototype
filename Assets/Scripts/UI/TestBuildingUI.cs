using BuildingSystem;
using SelectionSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Selectable = SelectionSystem.Selectable;

namespace UI
{
    public class TestBuildingUI : BaseBuildingUI
    {
        [SerializeField] private TextMeshProUGUI buildingTitle;
        [SerializeField] private TextMeshProUGUI level;
        
        [HideInInspector] public UnityEvent<GameObject> destroyButtonActive = new();
        
        private void Awake()
        {
            var uiSelectionManager = FindAnyObjectByType<UISelectionManager>();
            uiSelectionManager.onConstructionSelected.AddListener(OpenUI);

            var selectionManager = FindAnyObjectByType<SelectionManager>();
            selectionManager.objectDeselected.AddListener(CloseUI);
        }

        protected override void SetupUI(Selectable selectable)
        {
            UpdateUI(selectable);
        }

        protected override void SetupButtonListeners(Selectable selectable)
        {
            buttons[0].onClick.AddListener(UpgradeConstruction);
            buttons[1].onClick.AddListener(RebuildConstruction);
            buttons[2].onClick.AddListener(() => DestroyConstruction(selectable));
        }

        protected override void UpdateUI(Selectable selectable)
        {
            var buildingData = selectable.gameObject.GetComponent<BuildingData>();

            buildingTitle.text = buildingData.title;
            level.text = buildingData.currentLevel.ToString();
        }

        private void UpgradeConstruction()
        {
            Debug.Log("Upgrade");
        }

        private void RebuildConstruction()
        {
            Debug.Log("Rebuild");
        }

        private void DestroyConstruction(Selectable selectable)
        {
            destroyButtonActive.Invoke(selectable.gameObject);
        }
    }
}