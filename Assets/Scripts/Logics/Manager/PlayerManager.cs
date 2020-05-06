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
        /// <summary>
        /// 현재 턴을 가진 플레이어
        /// </summary>
        public Player TurnPlayer { private set { turnPlayer = value; } get { return turnPlayer; } }

        private List<Player> playerList = new List<Player>();
        public List<Player> PlayerList { get { return playerList; } }

        private Player targetPlayer = null;
        public Player TargetPlayer { set { targetPlayer = value; } get { return targetPlayer; } }
    }
}