using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Entity;

namespace TerraFormmingMars.Logics.Manager
{
    public class UIManager : Utility.Singleton<UIManager>
    {
        private Player tmpSelectedPlayer = null;
        public Player TmpSelectedPlayer { private set { tmpSelectedPlayer = value; } get { return tmpSelectedPlayer; } }

        public void SetTmpSelectedPlayer(bool isSet, Player selectedPlayer)
        {
            if (isSet == true)
            {
                TmpSelectedPlayer = selectedPlayer;
            }
        }

        public void SetTargetPlayer()
        {
            PlayerManager.Instance.TargetPlayer = TmpSelectedPlayer;
        }
    }
}