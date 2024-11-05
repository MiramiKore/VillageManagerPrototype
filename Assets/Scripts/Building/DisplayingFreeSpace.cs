using UnityEngine;

namespace Building
{
    public class DisplayingFreeSpace : MonoBehaviour
    {
        [SerializeField] private Color freeSpaceColor;
        [SerializeField] private Color occupiedSpaceColor;

        [SerializeField] private Material previewObjectMaterial;

        public void IndicateFreeSpace(bool spaceIsFree)
        {
            previewObjectMaterial.color = spaceIsFree ? freeSpaceColor : occupiedSpaceColor;
        }
    }
}