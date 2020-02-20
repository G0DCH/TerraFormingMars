using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity.Board
{
    [System.Serializable]
    public class TileData
    {
        [SerializeField]
        private TileType tileType;
        public TileType TileType { get { return tileType; } }

        [SerializeField]
        private List<Source> source;
        public List<Source> Source { get { return source; } }

        [SerializeField, Range(0, 2)]
        private int cardCount;
        public int CardCount { get { return cardCount; } }
    }
}