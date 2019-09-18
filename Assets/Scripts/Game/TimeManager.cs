using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormingMars
{
    public class TimeManager : Singleton<TimeManager>
    {
        [Tooltip("플레이어의 남은 시간")]
        static public float LeftTime = 0f;

        private void Start()
        {
            LeftTime = 0f;
        }
    }
}