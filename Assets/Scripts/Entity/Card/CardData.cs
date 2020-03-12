﻿using UnityEngine;
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
    }
}