using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity.Card
{
    [CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data", order = int.MaxValue)]
    public class CardData : ScriptableObject
    {
        [SerializeField]
        private int cost;
        public int Cost { get { return cost; } }

        private Image cardImage;
        public Image CardImage { set { cardImage = value; } get { return cardImage; } }

        [SerializeField]
        private string cardImageName;
        public string CardImageName { get { return cardImageName; } }

        [SerializeField]
        private List<Tag> tags;
        public List<Tag> Tags { get { return tags; } }

        [SerializeField]
        private int score;
        public int Score { get { return score; } }

        [SerializeField]
        private CardType cardType;
        public CardType CardType { get { return cardType; } }

        [Space]
        [SerializeField]
        private List<FunctionData> functionList;
        public List<FunctionData> FunctionList { get { return functionList; } }

        [SerializeField]
        private List<FunctionData> limitFunctions;
        public List<FunctionData> LimitFunctions { get { return limitFunctions; } }

        [SerializeField]
        private List<FunctionData> targetFunctionList;
        /// <summary>
        /// 첫번째는 타겟을 지정하는 함수
        /// <para>다음부터는 지정한 타겟에게 실행할 함수</para>
        /// </summary>
        public List<FunctionData> TargetFunctionList { get { return targetFunctionList; } }
    }
}