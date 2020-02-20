using UnityEngine;

namespace TerraFormmingMars.Entity.Board
{
    [System.Serializable]
    public class Temperature : PlanetIndicators
    {
        [SerializeField, Range(-30, 8)]
        private int currentTemperature;
        public int CurrentTemperature { private set { currentTemperature = value; } get { return currentTemperature; } }

        public override void IncreaseIndicators()
        {
            if (CurrentTemperature < 8)
            {
                CurrentTemperature += 2;
            }
            else
            {
                Debug.LogError("현재 기온이 8도 이상입니다.");
            }
        }
    }
}