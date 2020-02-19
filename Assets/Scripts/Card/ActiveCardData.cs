using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TerraFormmingMars.Card
{
    [CreateAssetMenu(fileName = "Active Card Data", menuName = "Scriptable Object/Active Card Data", order = int.MaxValue)]
    public class ActiveCardData : CardData
    {
        [SerializeField]
        private List<string> activeFunctionList;
        public List<string> ActiveFucntionList { get { return activeFunctionList; } }

        [SerializeField]
        private List<string> activeFunctionArguments;
        public List<string> ActiveFunctionArguments { get { return activeFunctionArguments; } }

        [SerializeField]
        private string stainableFunction;
        public string StainableFunction { get { return stainableFunction; } }
    }
}