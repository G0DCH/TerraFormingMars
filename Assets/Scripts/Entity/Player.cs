using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormmingMars.Entity
{
    public class Player : MonoBehaviour
    {
        private Source MC;
        private Source Iron;
        private Source Titanium;
        private Source Plant;
        private Source Energy;
        private Source Heat;

        /// <summary>
        /// 자원 이름(string), 자원이 매핑
        /// </summary>
        public Dictionary<string, Source> StringSourceMap = new Dictionary<string, Source>();
        /// <summary>
        /// 자원 타입(SourceType), 자원이 매핑
        /// </summary>
        public Dictionary<Logics.SourceType, Source> TypeSourceMap = new Dictionary<Logics.SourceType, Source>();

        private Corporation.Corporation corporation;

        private List<Card.Card> hands = new List<Card.Card>();
        private List<Card.Card> selectHands = new List<Card.Card>();

        //사용한 카드들
        private List<Card.Card> activeCards = new List<Card.Card>();
        private List<Card.Card> automatedCards = new List<Card.Card>();
        private List<Card.Card> eventCards = new List<Card.Card>();

        private int terraformmingLevel = 20;

        private void Start()
        {
            InitSource();
            InitStringSourceMap();
            InitTypeSourceMap();
        }

        private void InitSource()
        {
            MC = new Source(Logics.SourceType.MC);
            Iron = new Source(Logics.SourceType.Iron);
            Titanium = new Source(Logics.SourceType.Titanium);
            Plant = new Source(Logics.SourceType.Plant);
            Energy = new Source(Logics.SourceType.Energy);
            Heat = new Source(Logics.SourceType.Heat);
        }

        private void InitStringSourceMap()
        {
            StringSourceMap.Add("MC", MC);
            StringSourceMap.Add("Iron", Iron);
            StringSourceMap.Add("Titanium", Titanium);
            StringSourceMap.Add("Plant", Plant);
            StringSourceMap.Add("Energy", Energy);
            StringSourceMap.Add("Heat", Heat);
        }

        private void InitTypeSourceMap()
        {
            TypeSourceMap.Add(Logics.SourceType.MC, MC);
            TypeSourceMap.Add(Logics.SourceType.Iron, Iron);
            TypeSourceMap.Add(Logics.SourceType.Titanium, Titanium);
            TypeSourceMap.Add(Logics.SourceType.Plant, Plant);
            TypeSourceMap.Add(Logics.SourceType.Energy, Energy);
            TypeSourceMap.Add(Logics.SourceType.Heat, Heat);
        }
    }
}