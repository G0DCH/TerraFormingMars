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
        public delegate void functionList(params object[] arguments);
    }
}