using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Entity.Card;

namespace TerraFormmingMars.Logics.Manager
{
    public class CardManager : Utility.Singleton<CardManager>
    {
        private List<Card> cardList = new List<Card>();
        public List<Card> CardList { get { return cardList; } }

        [SerializeField]
        private List<CardData> cardDataList;
    }
}