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

        private Player SelectTargetPlayer()
        {
            //플레이어 리스트를 보여주고
            //그 중에서 하나를 선택하게 함

            return null;
        }
    }
}