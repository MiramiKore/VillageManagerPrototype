using UnityEngine;

namespace ResourcesSystem
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Scriptable Objects/Resource")]
    public class Resource : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private int value;

        public string ID => id;
        public int Value => value;

        public void AddResource(int resourceValue)
        {
            if (resourceValue > 0)
            {
                value += resourceValue;
            }
        }
    }
}