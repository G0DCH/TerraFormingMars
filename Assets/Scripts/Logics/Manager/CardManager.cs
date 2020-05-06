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
        [SerializeField]
        private GameObject selectWindow;

        [SerializeField]
        private GameObject selectTarget;

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
            //비용 지불
            //함수 실행
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

        //SelectWindow에 들어갈 유저를 조건에 맞게 필터링
        private void FilteringTargetUser(SourceType sourceType, string which)
        {
            foreach(Player player in PlayerManager.Instance.PlayerList)
            {
                if (player.TypeSourceMap.TryGetValue(sourceType, out Source source) == true)
                {

                    if (which == "product")
                    {
                        if (source.Product > 0)
                        {
                            AddTarget(selectTarget, player, source);
                        }
                    }
                }
                else
                {
                    Debug.LogError(nameof(FilteringTargetUser) + " : " + sourceType + "에 해당하는 자원이 없습니다.");
                }
            }
        }

        private void AddTarget(GameObject target, Player player, Source source)
        {
            GameObject addedTarget = Instantiate(selectTarget, selectWindow.transform);

            if(addedTarget == null)
            {
                Debug.LogError(nameof(AddTarget) + " : 타겟 추가에 실패했습니다.");
                return;
            }
            else
            {
                Toggle toggle = addedTarget.GetComponent<Toggle>();
                toggle.group = selectWindow.GetComponent<ToggleGroup>();

                string labelName = "Label";
                Text label = addedTarget.transform.Find(labelName).GetComponent<Text>();

                label.text = string.Format("{0}[{1} {2}(+{3})", player.playerName, source.SourceType, source.Amount, source.Product);
            }
        }
    }
}