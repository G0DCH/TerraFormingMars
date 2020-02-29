using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Utility;
using TerraFormmingMars.Entity;

namespace TerraFormmingMars.Logics.Manager
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        private Player turnPlayer;
        public Player TurnPlayer { private set { turnPlayer = value; } get { return turnPlayer; } }

        private Player targetPlayer;
        public Player TargetPlayer { set { targetPlayer = value; } get { return targetPlayer; } }
    }
}