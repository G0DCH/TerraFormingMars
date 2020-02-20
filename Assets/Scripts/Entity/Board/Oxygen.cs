using UnityEngine;

namespace TerraFormmingMars.Entity.Board
{
    [System.Serializable]
    public class Oxygen : PlanetIndicators
    {
        [SerializeField, Range(0, 14)]
        private int currentOxygen;
        public int CurrentOxygen { private set { currentOxygen = value; } get { return currentOxygen; } }

        public override void IncreaseIndicators()
        {
            if(CurrentOxygen < 14)
            {
                CurrentOxygen++;
            }
            else
            {
                Debug.LogError("현재 산소 농도가 14퍼센트 이상입니다.");
            }
        }
    }
}