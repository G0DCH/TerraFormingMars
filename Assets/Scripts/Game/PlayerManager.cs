using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormingMars
{
    /// <summary>
    /// 자원
    /// </summary>
    public class Source
    {
        /// <summary>
        /// 자원 이름
        /// </summary>
        public string Name;
        /// <summary>
        /// 자원 소지 량
        /// </summary>
        public int Amount;
        /// <summary>
        /// 자원 생산 량
        /// </summary>
        public int Production;

        public Source()
        {
            Name = null;
            Amount = 0;
            Production = 0;
        }

        public Source(string myName, int myAmount, int myProduction)
        {
            Name = myName;
            Amount = myAmount;
            Production = myProduction;
        }

        /// <summary>
        /// 세대 종료 시 자원 생산 량 만큼
        /// 자원 소지량을 증가 시킨다.
        /// </summary>
        public void UpdateSource()
        {
            Amount += Production;
        }
    }

    /// <summary>
    /// 플레이어 정보
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// 돈, 메가크레딧
        /// </summary>
        public Source MC;
        /// <summary>
        /// 강철
        /// </summary>
        public Source Iron;
        /// <summary>
        /// 티타늄
        /// </summary>
        public Source Titanium;
        /// <summary>
        /// 식물
        /// </summary>
        public Source Plant;
        /// <summary>
        /// 에너지
        /// </summary>
        public Source Energy;
        /// <summary>
        /// 열
        /// </summary>
        public Source Heat;

        /// <summary>
        /// 플레이어 이름
        /// </summary>
        public string Name;
        /// <summary>
        /// 기업
        /// </summary>
        public Corporation Corporation;

        /// <summary>
        /// 테라포밍 등급
        /// </summary>
        public int TerraFormingLevel;

        /// <summary>
        /// 현재 갖고 있는 패
        /// </summary>
        public List<CardInfo> Hands;

        /// <summary>
        /// 카드 드래프트 중인 패
        /// </summary>
       // public List<CardInfo> DraftHands;

        /// <summary>
        /// DraftHands에서 내가 선택한 카드들
        /// 여기서 구매 할 패를 선택해서
        /// 1장당 3MC를 지불하고 Hands에 추가
        /// </summary>
       // public List<CardInfo> SelectDraftHands;

        /// <summary>
        /// 태그 이름
        /// </summary>
        public string[] TagName;

        /// <summary>
        /// 태그 갯수
        /// </summary>
        public int[] TagValue;

        /// <summary>
        /// 티타늄 가치
        /// 일반적으로 MC 3
        /// </summary>
        public int TitaniumValue = 3;

        /// <summary>
        /// 강철 가치
        /// 일반적으로 MC 2
        /// </summary>
        public int IronValue = 2;

        /// <summary>
        /// 테라포밍 레벨이 올랐는가?
        /// UNMI만 사용
        /// 매 세대가 지날 때 마다
        /// false로 초기화
        /// </summary>
        public bool IsTFLevelUp = false;

        public PlayerInfo()
        {
            MC = new Source("MC", 0, 0);
            Iron = new Source("Iron", 0, 0);
            Titanium = new Source("Titanium", 0, 0);
            Plant = new Source("Plant", 0, 0);
            Energy = new Source("Energy", 0, 0);
            Heat = new Source("Heat", 0, 0);
            TerraFormingLevel = 20;

            Name = null;
            Corporation = null;
            Hands = null;
           // DraftHands = null;
           // SelectDraftHands = null;

            initTag();
        }

        public PlayerInfo(string myName, Corporation myCoporation)
        {
            Name = myName;
            Corporation = myCoporation;

            MC = new Source("MC", 0, 0);
            Iron = new Source("Iron", 0, 0);
            Titanium = new Source("Titanium", 0, 0);
            Plant = new Source("Plant", 0, 0);
            Energy = new Source("Energy", 0, 0);
            Heat = new Source("Heat", 0, 0);
            TerraFormingLevel = 20;

            int tmplength = myCoporation.StartResource.Length;

            //시작 기업 자원으로 소지량 초기화
            for (int i = 0; i<tmplength;i++)
            {
                //자원 이름
                string tmpsourcename = myCoporation.StartResource[i];
                int tmpamount = myCoporation.StartResourceNum[i];
                switch(tmpsourcename)
                {
                    case "MC":
                        MC.Amount = tmpamount;
                        break;
                    case "Iron":
                        Iron.Amount = tmpamount;
                        break;
                    case "Titanium":
                        Titanium.Amount = tmpamount;
                        break;
                    case "Plant":
                        Plant.Amount = tmpamount;
                        break;
                    case "Energy":
                        Energy.Amount = tmpamount;
                        break;
                    case "Heat":
                        Heat.Amount = tmpamount;
                        break;
                }
            }

            tmplength = myCoporation.StartResourceProduct.Length;

            //시작 기업 자원으로 생산량 초기화
            for (int i = 0; i < tmplength; i++)
            {
                //자원 이름
                string tmpsourcename = myCoporation.StartResourceProduct[i];
                int tmpproduct = myCoporation.StartResourceProductNum[i];
                switch (tmpsourcename)
                {
                    case "MC":
                        MC.Production = tmpproduct;
                        break;
                    case "Iron":
                        Iron.Production = tmpproduct;
                        break;
                    case "Titanium":
                        Titanium.Production = tmpproduct;
                        break;
                    case "Plant":
                        Plant.Production = tmpproduct;
                        break;
                    case "Energy":
                        Energy.Production = tmpproduct;
                        break;
                    case "Heat":
                        Heat.Production = tmpproduct;
                        break;
                }
            }

            Hands = new List<CardInfo>();
          //  DraftHands = new List<CardInfo>();
           // SelectDraftHands = new List<CardInfo>();

            initTag();

            //기업이 포보로그 인 경우 티타늄 가치 1 증가
            if(Corporation.CorpName == "PhoboLog")
                TitaniumValue++;

            if (Corporation.CorpName == "SaturnSystems")
                MC.Production++;

            Debug.Log(Name + " : " + Corporation.CorpName);
        }

        /// <summary>
        /// 태그 배열 초기화 함수
        /// </summary>
        void initTag()
        {
            TagValue = new int[11];
            TagName = new string[11];
            for(int i = 0; i<11; i++)
            {
                TagValue[i] = 0;
            }

            TagName[0] = "Building";
            TagName[1] = "Space";
            TagName[2] = "Energy";
            TagName[3] = "Science";
            TagName[4] = "Jupiter";
            TagName[5] = "Earth";
            TagName[6] = "Plant";
            TagName[7] = "Animal";
            TagName[8] = "Microbe";
            TagName[9] = "City";
            TagName[10] = "Event";
        }
    }

    public class PlayerManager : Singleton<PlayerManager>
    {
        public List<PlayerInfo> players = new List<PlayerInfo>();

        /// <summary>
        /// 나
        /// </summary>
        public PlayerInfo Me;

        /// <summary>
        /// 내 자원을 보여주는 텍스트
        /// </summary>
        public UnityEngine.UI.Text[] SourceText;

        public void MakePlayer()
        {
            CorpsManager.Instance.LoadCorps();

            CardManager.Instance.LoadCards();
            CardManager.Instance.StartCoroutine(CardManager.Instance.ReceiveCard(10));
        }

        public void SourceUpdater()
        {
            SourceText[0].text = Me.MC.Amount.ToString() + "(+" + Me.MC.Production.ToString() + ")";

            SourceText[1].text = Me.Iron.Amount.ToString() + "(+" + Me.Iron.Production.ToString() + ")";

            SourceText[2].text = Me.Titanium.Amount.ToString() + "(+" + Me.Titanium.Production.ToString() + ")";

            SourceText[3].text = Me.Plant.Amount.ToString() + "(+" + Me.Plant.Production.ToString() + ")";

            SourceText[4].text = Me.Energy.Amount.ToString() + "(+" + Me.Energy.Production.ToString() + ")";

            SourceText[5].text = Me.Heat.Amount.ToString() + "(+" + Me.Heat.Production.ToString() + ")";
        }
    }
}