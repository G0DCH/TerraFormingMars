using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TerraFormmingMars.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace TerraFormmingMars.Logics.Manager
{
    public class EffectManager : Utility.Singleton<EffectManager>
    {
        [SerializeField]
        private GameObject selectWindow;

        [SerializeField]
        private GameObject selectTarget;


        public void ExecuteEffect(List<FunctionData> functionDatas)
        {
            Player targetPlayer;
            targetPlayer = PlayerManager.Instance.TurnPlayer;

            List<object> arguments = new List<object>();
            foreach(FunctionData functionData in functionDatas)
            {
                arguments.Clear();
                arguments.Add(targetPlayer);
                arguments.Add(functionData.FunctionArguments);

                Type type = GetType();
                MethodInfo functionInfo =
                    type.GetMethod(functionData.FunctionName, BindingFlags.Instance | BindingFlags.NonPublic);
                functionInfo.Invoke(this, arguments.ToArray());
            }
        }

        private void ChangeSourceProduct(Player player, List<string> arguments)
        {
            SourceType sourceType = EnumManager.Instance.StringToSourceType(arguments[0]);
            int sourceDifference = int.Parse(arguments[1]);

            Source source;
            //자원을 알맞게 획득했다면 생산량 변경
            if(player.TypeSourceMap.TryGetValue(sourceType, out source) == true)
            {
                source.Product += sourceDifference;
            }
            else
            {
                Debug.LogError(nameof(ChangeSourceProduct) + " : " + sourceType + "에 해당하는 자원이 존재하지 않습니다.");
            }
        }

        //SelectWindow에 들어갈 유저를 조건에 맞게 필터링 후 추가
        private void FilteringTargetUser(string sourceType, string which)
        {
            foreach (Player player in PlayerManager.Instance.PlayerList)
            {
                if (player.StringSourceMap.TryGetValue(sourceType, out Source source) == true)
                {

                    if (which == "Product")
                    {
                        if (source.Product > 0)
                        {
                            AddTarget(selectTarget, player, source);
                        }
                    }
                    else if (which == "Amount")
                    {
                        if (source.Amount > 0)
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

            if (addedTarget == null)
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

                label.text = string.Format("{0} [{1} {2}(+{3})]", player.playerName, source.SourceType, source.Amount, source.Product);
            }
        }
    }
}