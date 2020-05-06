using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Entity.Card;
using TerraFormmingMars.Entity;
using UnityEngine.UI;

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
            if(LimitFunctionManager.Instance.CheckLimit(cardData.LimitFunctions) == false)
            {
                return;
            }            
            //비용 검사            
            if(CheckCost(cardData.Cost) == false)
            {
                return;
            }
            //함수 실행
            EffectManager.Instance.ExecuteCard(cardData.FunctionList, cardData.TargetFunctionList);
            //비용 지불
        }

        private bool CheckCost(int cost)
        {
            Source source;
            if(PlayerManager.Instance.TurnPlayer.TypeSourceMap.TryGetValue(SourceType.MC, out source) == true)
            {
                if(source.Amount >= cost)
                {
                    return true;
                }
            }
            else
            {
                Debug.LogError(nameof(CheckCost) + " : " + SourceType.MC + "에 해당하는 자원이 없습니다.");
            }

            return false;
        }
    }
}