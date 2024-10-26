using UnityEngine;
using UnityEngine.InputSystem;

namespace Construction
{
    public class BuildingProjection : MonoBehaviour
    {
        [SerializeField] private GameObject projectionObject; // Объект с проекцией постройки

        [SerializeField] private LayerMask groundLayer; // Слой земли, по которому будет перемещаться объект

        // Отобразить проекцию
        public void ShowProjectionBuilding(GameObject construction)
        {
            projectionObject.SetActive(true);

            projectionObject.GetComponent<MeshFilter>().mesh = construction.GetComponent<MeshFilter>().sharedMesh;

            MoveProjectionToMousePosition(construction);
        }

        // Спрятать и вернуть объект проекции
        public GameObject HideProjectionBuilding()
        {
            projectionObject.SetActive(false);

            return projectionObject;
        }

        // Перемещать проекцию в позицию указателя
        private void MoveProjectionToMousePosition(GameObject construction)
        {
            // Создаем луч от камеры к позиции курсора
            var ray = Camera.main.ScreenPointToRay(Pointer.current.position.value);

            // Проверяем попадание луча в землю
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
            {
                // Получаем точку, на которую указывает луч
                var targetPosition = hit.point;

                // Обновляем позицию объекта
                var projectionTransform = projectionObject.transform;
                projectionTransform.position =
                    new Vector3(targetPosition.x, construction.transform.position.y, targetPosition.z);
            }
        }
    }
}