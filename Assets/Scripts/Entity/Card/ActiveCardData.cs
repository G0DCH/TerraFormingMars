using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TerraFormmingMars.Entity.Card
{
    /// <summary>
    /// 카드의 행동을 수행 했는가 여부
    /// </summary>
    public enum DIDACTION
    {
        NO,
        YES,
        NULL
    }

    [CreateAssetMenu(fileName = "Active Card Data", menuName = "Scriptable Object/Active Card Data", order = int.MaxValue)]
    public class ActiveCardData : CardData
    {
        [SerializeField]
        private List<FunctionData> activeFunctionList;
        public List<FunctionData> ActiveFucntionList { get { return activeFunctionList; } }

        [SerializeField]
        private FunctionData stainableFunction;
        public FunctionData StainableFunction { get { return stainableFunction; } }

        [Space]
        [SerializeField]
        private DIDACTION didAction;
        public DIDACTION DidAction { private set { didAction = value; } get { return didAction; } }

        public void ChangeDidAction()
        {
            if(DidAction == DIDACTION.NULL)
            {
                Debug.LogError("행동이 있는 카드가 아닙니다.");
                return;
            }
            else if(DidAction == DIDACTION.YES)
            {
                Debug.LogError("이미 행동 했습니다.");
                return;
            }
            else
            {
                DidAction = DIDACTION.NO;
            }
        }
    }
}