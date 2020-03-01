using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Utility;
using TerraFormmingMars.Entity;

namespace TerraFormmingMars.Logics.Manager
{
    public class LimitFunctionManager : Singleton<LimitFunctionManager>
    {
        public Entity.Card.CardData cardData;

        private void Start()
        {
            CheckLimit(cardData.LimitFunction);
        }

        /// <summary>
        /// FunctionData와 일치하는 조건 검사 함수 실행
        /// </summary>
        /// <param name="functionData"></param>
        /// <returns></returns>
        public bool CheckLimit(FunctionData functionData)
        {
            Type type = GetType();
            MethodInfo functionInfo =
                type.GetMethod(functionData.FunctionName, BindingFlags.Instance | BindingFlags.NonPublic);

            return (bool)functionInfo.Invoke(this, functionData.FunctionArguments.ToArray());
        }

        private bool ProductLimit(params string[] arguments)
        {
            string sourceType = arguments[0];
            int productLimit = int.Parse(arguments[1]);

            Source source;
            if (PlayerManager.Instance.TurnPlayer.StringSourceMap.TryGetValue(sourceType, out source))
            {
                if (source.Product >= productLimit)
                {
                    return true;
                }
            }
            else
            {
                Debug.LogError(sourceType + "에 해당하는 자원이 없습니다.");
            }

            return false;
        }
    }
}