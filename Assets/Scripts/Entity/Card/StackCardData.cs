using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormmingMars.Entity.Card
{
    [CreateAssetMenu(fileName = "Stack Card Data", menuName = "Scriptable Object/Stack Card Data", order = int.MaxValue)]
    public class StackCardData : ActiveCardData
    {
        [SerializeField]
        private string stackType;
        public string StackType { get { return stackType; } }

        [SerializeField]
        private int stackCount;
        public int StackCount { set { stackCount = value; } get { return stackCount; } }
    }
}