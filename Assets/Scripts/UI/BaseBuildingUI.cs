using UnityEngine;
using UnityEngine.UI;
using Selectable = Selection.Selectable;

namespace UI
{
    public abstract class BaseBuildingUI : MonoBehaviour
    {
        [SerializeField] protected GameObject uiWindow; // Главное окно UI для отображения информации

        [SerializeField] protected float distanceUIFromBuilding = 200; // Расстояние между интерфейсом и объектом

        [SerializeField] protected Button[] buttons; // Массив для кнопок в окне UI

        protected abstract void SetupUI(Selectable selectable);
        protected abstract void SetupButtonListeners(Selectable selectable);
        protected abstract void UpdateUI(Selectable selectable);

        protected void OpenUI(Selectable selectable)
        {
            uiWindow.SetActive(true);
            MoveUI(selectable.gameObject.transform.position);
            SetupUI(selectable);
            SetupButtonListeners(selectable);
        }

        protected void CloseUI()
        {
            uiWindow.SetActive(false);
            RemoveButtonListeners();
        }

        private void MoveUI(Vector3 objectPosition)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
            uiWindow.transform.position = screenPosition + Vector3.up * distanceUIFromBuilding;
        }

        private void RemoveButtonListeners()
        {
            foreach (var button in buttons) button.onClick.RemoveAllListeners();
        }
    }
}