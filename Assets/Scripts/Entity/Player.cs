using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Logics.Manager;

namespace TerraFormmingMars.Entity
{
    public class Player : MonoBehaviour
    {
        #region 자원
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
        #endregion

        private Corporation.Corporation corporation;

        private List<Card.Card> hands = new List<Card.Card>();
        private List<Card.Card> selectHands = new List<Card.Card>();

        //사용한 카드들
        private List<Card.Card> activeCards = new List<Card.Card>();
        private List<Card.Card> automatedCards = new List<Card.Card>();
        private List<Card.Card> eventCards = new List<Card.Card>();

        public string playerName { private set; get; } = string.Empty;

        #region 태그
        private TagContainer tag_Building;
        private TagContainer tag_Space;
        private TagContainer tag_Energy;
        private TagContainer tag_Science;
        private TagContainer tag_Jupiter;
        private TagContainer tag_Earth;
        private TagContainer tag_Plant;
        private TagContainer tag_Animal;
        private TagContainer tag_Microbe;
        private TagContainer tag_City;
        private TagContainer tag_Event;

        public Dictionary<string, TagContainer> StringTagMap = new Dictionary<string, TagContainer>();
        public Dictionary<Logics.Tag, TagContainer> TagTagMap = new Dictionary<Logics.Tag, TagContainer>();
        #endregion

        private int terraformmingLevel = 20;

        //public Card.CardData cardData;

        private void Start()
        {
            InitSource();
            InitStringSourceMap();
            InitTypeSourceMap();

            InitTagContainer();
            InitStringTagMap();
            InitTagTagMap();

            // 테스트용 코드
            //MC.Amount += 1000;
            //MC.Product += 1000;
            //Energy.Product += 1000;

            //playerName = "1234";

            //CardManager.Instance.UseCard(cardData);
        }

        //자원 초기화
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

        //태그 초기화
        private void InitTagContainer()
        {
            tag_Building = new TagContainer(Logics.Tag.Building);
            tag_Animal = new TagContainer(Logics.Tag.Animal);
            tag_City = new TagContainer(Logics.Tag.City);
            tag_Earth = new TagContainer(Logics.Tag.Earth);
            tag_Energy = new TagContainer(Logics.Tag.Energy);
            tag_Event = new TagContainer(Logics.Tag.Event);
            tag_Jupiter = new TagContainer(Logics.Tag.Jupitor);
            tag_Microbe = new TagContainer(Logics.Tag.Microbe);
            tag_Plant = new TagContainer(Logics.Tag.Plant);
            tag_Science = new TagContainer(Logics.Tag.Science);
            tag_Space = new TagContainer(Logics.Tag.Space);
        }

        private void InitStringTagMap()
        {
            StringTagMap.Add("Animal", tag_Animal);
            StringTagMap.Add("Building", tag_Building);
            StringTagMap.Add("City", tag_City);
            StringTagMap.Add("Earth", tag_Earth);
            StringTagMap.Add("Energy", tag_Energy);
            StringTagMap.Add("Event", tag_Event);
            StringTagMap.Add("Jupiter", tag_Jupiter);
            StringTagMap.Add("Microbe", tag_Microbe);
            StringTagMap.Add("Plant", tag_Plant);
            StringTagMap.Add("Science", tag_Science);
            StringTagMap.Add("Space", tag_Space);
        }

        private void InitTagTagMap()
        {
            TagTagMap.Add(Logics.Tag.Animal, tag_Animal);
            TagTagMap.Add(Logics.Tag.Building, tag_Building);
            TagTagMap.Add(Logics.Tag.City, tag_City);
            TagTagMap.Add(Logics.Tag.Earth, tag_Earth);
            TagTagMap.Add(Logics.Tag.Energy, tag_Energy);
            TagTagMap.Add(Logics.Tag.Event, tag_Event);
            TagTagMap.Add(Logics.Tag.Jupitor, tag_Jupiter);
            TagTagMap.Add(Logics.Tag.Microbe, tag_Microbe);
            TagTagMap.Add(Logics.Tag.Plant, tag_Plant);
            TagTagMap.Add(Logics.Tag.Science, tag_Science);
            TagTagMap.Add(Logics.Tag.Space, tag_Space);
        }
    }
}