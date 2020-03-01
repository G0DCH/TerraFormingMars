using System.Collections;
using TerraFormmingMars.Entity;
using UnityEngine;

namespace TerraFormmingMars.Logics.Manager
{
    public class EffectManager : Utility.Singleton<EffectManager>
    {
        private void ChangeSourceProduct(Player player, params string[] arguments)
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
    }
}