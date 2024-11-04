using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UIModeSelection : MonoBehaviour
    {
        [HideInInspector] public UnityEvent onDefaultMode;
        [HideInInspector] public UnityEvent onBuildingMode;
        
        [SerializeField] private GameObject defaultMode;
        [SerializeField] private GameObject buildingMode;

        [SerializeField] private Button buildingButton;
        [SerializeField] private Button defaultButton;

        private void Awake()
        {
            buildingButton.onClick.AddListener(SwitchingToBuildingMode);
            defaultButton.onClick.AddListener(SwitchingToDefaultMode);
            
            onDefaultMode.AddListener(SwitchingToDefaultMode);
            onBuildingMode.AddListener(SwitchingToBuildingMode);
        }

        private void SwitchingToDefaultMode()
        {
            buildingMode.SetActive(false);
            defaultMode.SetActive(true);
        }

        private void SwitchingToBuildingMode()
        {
            defaultMode.SetActive(false);
            buildingMode.SetActive(true);
        }
    }
}
