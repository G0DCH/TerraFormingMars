using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity.Board
{
    public abstract class PlanetIndicators
    {
        [UnityEngine.SerializeField]
        protected PLANETINDICATORS planetIndicators;

        public abstract void IncreaseIndicators();
    }
}