using TMPro;
using UnityEngine;

namespace ResourcesSystem
{
    public class UIResources : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] indicators;

        public Resource resource;

        private void Update()
        {
            indicators[0].text = resource.Value.ToString();

            if (Input.GetKeyDown(KeyCode.B))
            {
                resource.AddResource(100);
            }
        }
    }
}