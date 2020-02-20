using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormmingMars.Entity
{
    /// <summary>
    /// 함수의 이름, 함수의 인자를 담고 있음
    /// </summary>
    [System.Serializable]
    public class FunctionData
    {
        [SerializeField]
        private string functionName;
        public string FunctionName { get { return functionName; } }

        [SerializeField]
        private List<string> functionArguments;
        public List<string> FunctionArguments { get { return functionArguments; } }
    }
}