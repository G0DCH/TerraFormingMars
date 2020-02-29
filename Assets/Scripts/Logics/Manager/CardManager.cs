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

        public void UseCard(CardData cardData)
        {
            //제한 검사
            //비용 검사
            //함수 실행
            //비용 지불
        }
    }
}