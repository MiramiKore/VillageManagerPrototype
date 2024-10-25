using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ConstructionManager : MonoBehaviour
    {
        [SerializeField] private GameObject constructionCard;

        public List<Constructions> listConstructions;

        private void GenerateConstructionsCards()
        {
            listConstructions = new List<Constructions>()
            {
                
            };
        }
    }
}