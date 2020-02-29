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
            functionInfo.Invoke(this, functionData.FunctionArguments.ToArray());

            return false;
        }

        private bool ProductLimit(params string[] arguments)
        {
            return false;
        }
    }
}