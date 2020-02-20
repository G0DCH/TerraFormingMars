using UnityEngine;

namespace TerraFormmingMars.Entity.Board
{
    [System.Serializable]
    public class Water : PlanetIndicators
    {
        [SerializeField, Range(0, 9)]
        private int currentWater;
        public int CurrentWater { private set { currentWater = value; } get { return currentWater; } }

        public override void IncreaseIndicators()
        {
            if(CurrentWater<9)
            {
                CurrentWater++;
            }
            else
            {
                Debug.LogError("현재 물 갯수가 9개 이상입니다.");
            }
        }
    }
}