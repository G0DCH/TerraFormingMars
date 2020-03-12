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
        public bool CheckLimit(List<FunctionData> functionDatas)
        {
            //최종 검사 결과
            bool checkResults = false;
            foreach(FunctionData functionData in functionDatas)
            {
                Type type = GetType();
                MethodInfo functionInfo =
                    type.GetMethod(functionData.FunctionName, BindingFlags.Instance | BindingFlags.NonPublic);
                //조건 1개 검사 결과
                bool checkResult = (bool)functionInfo.Invoke(this, functionData.FunctionArguments.ToArray());

                checkResults = checkResults || checkResult;
            }

            return checkResults;
        }

        private bool ProductLimit(string sourceType, string _limitCount)
        {
            int productLimit = int.Parse(_limitCount);

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

        private bool TagLimit(string tagType, string _limitCount)
        {
            Tag tag = (Tag)EnumManager.Instance.StringToEnumType(tagType);
            int limitCount = int.Parse(_limitCount);

            TagContainer result;
            if(PlayerManager.Instance.TurnPlayer.TagTagMap.TryGetValue(tag, out result) == true)
            {
                if(result.TagCount > limitCount)
                {
                    return true;
                }
            }
            else
            {
                Debug.LogError(nameof(TagLimit) + " : " + tag + "에 해당하는 태그가 없습니다.");
            }

            return false;
        }
    }
}