using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity
{
    [System.Serializable]
    public class Source
    {
        [SerializeField]
        private SourceType sourceType;
        public SourceType SourceType { private set { sourceType = value; } get { return sourceType; } }

        [SerializeField]
        private int amount;
        public int Amount { set { amount = value; } get { return amount; } }

        [SerializeField]
        private int product;
        public int Product { set { product = value; } get { return product; } }
    }
}