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

        private bool ProductLimit(string sourceType, string _productLimit)
        {
            int productLimit = int.Parse(_productLimit);

            Source source;
            if (PlayerManager.Instance.TurnPlayer.StringSourceMap.TryGetValue(sourceType, out source) == true)
            {
                if (source.Product >= productLimit)
                {
                    return true;
                }
            }
            else
            {
                Debug.LogError(nameof(ProductLimit) + " : " + sourceType + "에 해당하는 자원이 없습니다.");
            }

            return false;
        }
    }
}