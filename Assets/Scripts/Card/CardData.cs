using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TerraFormmingMars.Card
{
    [CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data", order = int.MaxValue)]
    public class CardData : ScriptableObject
    {
        [SerializeField]
        private int cost;
        public int Cost { get { return cost; } }

        [SerializeField]
        private Image cardImage;
        public Image CardImage { get { return cardImage; } }

        [SerializeField]
        private List<string> tags;
        public List<string> Tags { get { return tags; } }

        [SerializeField]
        private int score;
        public int Score { get { return score; } }

        [SerializeField]
        private List<string> functionList;
        public List<string> FunctionList { get { return functionList; } }

        [SerializeField]
        private List<string> functionArguments;
        public List<string> FunctionArguments { get { return functionArguments; } }

        [SerializeField]
        private string limitFunction;
        public string LimitFunction { get { return limitFunction; } }

        [SerializeField]
        private List<string> limitFunctionArguments;
        public List<string> LimitFunctionArguments { get { return limitFunctionArguments; } }
    }
}