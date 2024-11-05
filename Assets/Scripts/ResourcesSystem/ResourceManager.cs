using System.Collections;
using UnityEngine;

namespace ResourcesSystem
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private float delaySpeed;
        private int _startValue;
        
        public Resource resourceManagerValue;
        
        private void Start()
        {
            _startValue = 1;
            
            StartCoroutine(ControlResources());
        }
        
        private IEnumerator ControlResources()
        {
            while (true)
            {
                resourceManagerValue.AddResource(_startValue);
                
                yield return new WaitForSeconds(delaySpeed);
            }
        }
    }
}