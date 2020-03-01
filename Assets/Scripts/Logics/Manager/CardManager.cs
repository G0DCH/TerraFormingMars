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
            if(LimitFunctionManager.Instance.CheckLimit(cardData.LimitFunction) == false)
            {
                return;
            }
            //비용 검사
            if(CheckCost(cardData) == false)
            {
                return;
            }
            //함수 실행
            //비용 지불
        }

        private bool CheckCost(CardData cardData)
        {
            int cost = cardData.Cost;

            Entity.Source source;
            if(PlayerManager.Instance.TurnPlayer.TypeSourceMap.TryGetValue(SourceType.MC, out source))
            {
                if(source.Amount >= cost)
                {
                    return true;
                }
            }
            else
            {
                Debug.LogError(source.SourceType + "에 해당하는 자원이 없습니다.");
            }

            return false;
        }
    }
}