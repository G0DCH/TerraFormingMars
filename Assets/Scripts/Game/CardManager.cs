using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Reflection;
using System;
using UnityEngine.UI;

namespace TerraFormingMars
{
    public class CardInfo
    {
        /// <summary>
        /// 카드 이미지 이름
        /// </summary>
        public string ImageName;

        /// <summary>
        /// 카드 비용
        /// </summary>
        public int Cost;

        /// <summary>
        /// 카드 종류
        /// Active, Automated, Event
        /// </summary>
        public string Category;

        /// <summary>
        /// 점수
        /// </summary>
        public int Score;

        /// <summary>
        /// 점수 비율
        /// </summary>
        public float ScoreRatio;

        /// <summary>
        /// 태그 종류
        /// </summary>
        public string[] Tags;

        /// <summary>
        /// 태그 갯수.
        /// <para>Tag[n]의 갯수 == TagNum[n]</para>
        /// </summary>
        public int[] TagNum;

        /// <summary>
        /// 카드 사용 효과 함수 이름들
        /// </summary>
        public string[] FuncName;

        /// <summary>
        /// 카드 사용 효과 함수 인자들
        /// </summary>
        public object[] FuncArgs;

        /// <summary>
        /// 각 함수의
        /// 마지막 함수 인자 인덱스
        /// </summary>
        public int[] FuncArgsInd;

        /// <summary>
        /// 카드 사용 제한 조건
        /// <para>첫글자가 m이면 최소, M이면 최대</para>
        /// </summary>
        public string[] Limits;

        /// <summary>
        /// 카드 사용 제한 조건 수치
        /// <para>Limits[n]의 갯수 == LimitsNum[n]</para>
        /// </summary>
        public int[] LimitsNum;

        /// <summary>
        /// 실제 카드 게임 오브젝트
        /// </summary>
        public GameObject Card;

        public CardInfo()
        {

        }

        public CardInfo(string myImageName, int myCost,
            string myCategory, int myScore, float myScoreRatio,
            string[] myTags, int[] myTagNum, string[] myFuncName,
            object[] myFuncArgs, int[] myFuncArgsInd, string[] myLimits,
            int[] myLimitsNum, GameObject myCard)
        {
            ImageName = myImageName;
            Cost = myCost;
            Category = myCategory;
            Score = myScore;
            ScoreRatio = myScoreRatio;
            Tags = myTags;
            TagNum = myTagNum;
            FuncName = myFuncName;
            FuncArgs = myFuncArgs;
            FuncArgsInd = myFuncArgsInd;
            Limits = myLimits;
            LimitsNum = myLimitsNum;
            Card = myCard;
        }
    }

    public class ActiveCardInfo : CardInfo
    {
        /// <summary>
        /// 행동 함수 이름들
        /// </summary>
        public string[] ActiveFuncName;

        /// <summary>
        /// 행동 함수 인자
        /// </summary>
        public object[] ActiveFuncArgs;

        /// <summary>
        /// 각 행동 함수의
        /// 마지막 함수 인자 인덱스
        /// </summary>
        public int[] ActiveFuncInd;

        /// <summary>
        /// 지속 효과 이름
        /// </summary>
        public string SustainableEffectName;

        /// <summary>
        /// 지속 효과 인자
        /// </summary>
        public object[] SustainableEffectArgs;

        /// <summary>
        /// 토큰 타입
        /// </summary>
        public string TokenType;

        /// <summary>
        /// 토큰 갯수
        /// </summary>
        public int TokenNum;

        public ActiveCardInfo()
        {

        }

        public ActiveCardInfo(string myImageName, int myCost,
            string myCategory, int myScore, float myScoreRatio,
            string[] myTags, int[] myTagNum, string[] myFuncName,
               object[] myFuncArgs, int[] myFuncArgsInd, string[] myLimits,
             int[] myLimitsNum, GameObject myCard)
        {
            ImageName = myImageName;
            Cost = myCost;
            Category = myCategory;
            Score = myScore;
            ScoreRatio = myScoreRatio;
            Tags = myTags;
            TagNum = myTagNum;
            FuncName = myFuncName;
            FuncArgs = myFuncArgs;
            FuncArgsInd = myFuncArgsInd;
            Limits = myLimits;
            LimitsNum = myLimitsNum;
            Card = myCard;
        }

        public ActiveCardInfo(string myImageName, int myCost,
            string myCategory, int myScore, float myScoreRatio,
            string[] myTags, int[] myTagNum, string[] myFuncName, object[] myFuncArgs, 
            int[] myFuncArgsInd, string[] myLimits,
            int[] myLimitsNum, GameObject myCard,
            string[] myActiveFuncName, object[] myActiveFuncArgs, int[] myActiveFuncInd,
            string mySustainableEffectName, object[] mySustainableEffectArgs,
            string myTokenType)
        {
            ImageName = myImageName;
            Cost = myCost;
            Category = myCategory;
            Score = myScore;
            ScoreRatio = myScoreRatio;
            Tags = myTags;
            TagNum = myTagNum;
            FuncName = myFuncName;
            FuncArgs = myFuncArgs;
            FuncArgsInd = myFuncArgsInd;
            Limits = myLimits;
            LimitsNum = myLimitsNum;
            Card = myCard;
            ActiveFuncName = myActiveFuncName;
            ActiveFuncArgs = myActiveFuncArgs;
            ActiveFuncInd = myActiveFuncInd;
            SustainableEffectName = mySustainableEffectName;
            SustainableEffectArgs = mySustainableEffectArgs;
            TokenType = myTokenType;
            TokenNum = 0;
        }
    }

    public class CardManager : Singleton<CardManager>
    {

        #region public 변수
        /// <summary>
        /// 카드
        /// </summary>
        public GameObject Card;

        /// <summary>
        /// 덱의 Transform
        /// </summary>
        public Transform DeckTrans;

        /// <summary>
        /// 버린 카드의 Transform
        /// </summary>
        public Transform DiscardTrans;

        /// <summary>
        /// 카드 모음
        /// </summary>
        public List<CardInfo> Cards = new List<CardInfo>();

        /// <summary>
        /// 덱
        /// </summary>
        public Queue<CardInfo> Deck = new Queue<CardInfo>();

        public Transform CardGrid;

        /// <summary>
        /// 내가 선택한 카드들
        /// </summary>
        public List<GameObject> SelectedCards = new List<GameObject>();

        /// <summary>
        /// 플레이어들이 받은 카드들
        /// </summary>
        public List<List<CardInfo>> PlayerHandedCards = new List<List<CardInfo>>();

        /// <summary>
        /// 덱이 셔플 되었는가?
        /// </summary>
        public bool isDeckShuffled = false;

        /// <summary>
        /// 산 카드 갯수
        /// </summary>
        public int[] BuyCardCount;

        /// <summary>
        /// 카드에서 선택한 자원 이름
        /// </summary>
        public string SelectedResourceName = string.Empty;

        #endregion

        public void LoadCards()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("InfoJSONFiles/CardInfo");
            if (textAsset != null)
            {
                string jsonStr = textAsset.ToString();

                JsonData playerData = JsonMapper.ToObject(jsonStr);

                for (int i = 0; i < playerData.Count; i++)
                {
                    string name = (string)playerData[i]["name"];
                    Image card = Instantiate(Card.gameObject, DeckTrans).GetComponent<Image>();
                    card.sprite = Resources.Load("Cards/" + (string)playerData[i]["category"] + "/"
                        + name, typeof(Sprite)) as Sprite;

                    card.name = name;

                    CardInfo cardInfo = new CardInfo();

                    cardInfo.Card = card.gameObject;

                    cardInfo.ImageName = name;

                    Cards.Add(cardInfo);

                    card.transform.localPosition = Vector3.zero;
                }
            }

            else
                Debug.Log("파일이 없음");

            BuyCardCount = new int[PhotonNetwork.playerList.Length];

            for(int i = 0; i<PhotonNetwork.playerList.Length; i++)
            {
                BuyCardCount[i] = -1;
            }

            CardShuffle();
        }

        public void CardShuffle()
        {
            if (PhotonNetwork.isMasterClient)
            {
                //카드 인덱스 길이
                int len = Cards.Count;

                //카드 리스트에서 뽑아서 쓸 인덱스 리스트
                int[] cardIndexes = new int[len];

                for (int i = 0; i < len; i++)
                    cardIndexes[i] = -1;

                int front = 0;

                //인덱스 리스트에 중복되지 않는 난수를 채움
                while (front != cardIndexes.Length)
                {
                    int num;
                    bool isInList = true;


                    while (isInList)
                    {
                        num = UnityEngine.Random.Range(0, Cards.Count);
                        int i;

                        for (i = 0; i <= front; i++)
                        {
                            if (num == cardIndexes[i])
                            {
                                isInList = true;
                                break;
                            }
                            else
                            {
                                isInList = false;
                            }
                        }

                        if (i > front && !isInList)
                        {
                            cardIndexes[front] = num;
                            front++;
                        }
                    }
                }

                GameManager.Instance.SendShuffleCardIndList(PhotonTargets.All, cardIndexes);
            }

            //StartCoroutine(ReceiveCard(10));
        }

        /// <summary>
        /// 덱에서 카드를 뽑아서 CardGrid에 넣음
        /// </summary>
        public void GoCardGrid(int n)
        {
            List<CardInfo> tmpCards = new List<CardInfo>();

            for(int i = 0; i<n; i++)
            {
                GameObject obj;
                CardInfo tmpCard = Deck.Dequeue();
                tmpCards.Add(tmpCard);
                obj = tmpCard.Card;
                obj.transform.SetParent(CardGrid);

                obj.GetComponent<Button>().onClick.AddListener
                    (delegate () { PlayerUIManager.Instance.Card_CorpsSelected(obj); });

                SelectedCards.Add(obj);
            }

            PlayerHandedCards.Add(tmpCards);
        }

        /// <summary>
        /// 마스터 클라이언트가 준 인덱스 리스트를 이용해서
        /// 덱을 구성
        /// </summary>
        /// <param name="CardIndList">인덱스 리스트</param>
        public void MakeDeck(int[] CardIndList)
        {
            for(int i = 0; i<CardIndList.Length; i++)
            {
                Deck.Enqueue(Cards[CardIndList[i]]);
            }

            Cards.Clear();

            isDeckShuffled = true;
        }

        /// <summary>
        /// 세대 시작 시 카드를 n 장씩 받음
        /// </summary>
        /// <param name="n">받을 카드 갯수</param>
        public IEnumerator ReceiveCard(int n)
        {
            while(!isDeckShuffled)
            {
                yield return new WaitForEndOfFrame();
            }

            int num = GameManager.Instance.PlayerList.Length;

            for(int i = 0; i<num; i++)
            {
                List<CardInfo> tmpCards = new List<CardInfo>();
                if (GameManager.Instance.PlayerList[i] != PhotonNetwork.playerName)
                {
                    for (int j = 0; j < n; j++)
                    {
                        tmpCards.Add(Deck.Dequeue());
                    }
                    PlayerHandedCards.Add(tmpCards);
                }
                else
                    GoCardGrid(n);
            }

           // Debug.Log("ReceiveCard 함수 끝");

            yield break;
        }

        /// <summary>
        /// CardGrid에 남은 카드들을
        /// DisCard로 옮김
        /// </summary>
        public void ClearCardGrid()
        {
            for(int i = 0; i<CardGrid.childCount; i++)
            {
                Transform tmp = CardGrid.GetChild(i);
                tmp.SetParent(DiscardTrans);
                tmp.localPosition = Vector3.zero;
            }
        }

        #region 카드 효과 함수들

        /// <summary>
        /// 태그 검사 후 코스트 감소
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator TagChangeCost(params object[] args)
        {
            string tag = (string)args[0];
            int offset = (int)args[1];

            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            int n = player.Hands.Count;

            //태그 검사 후 코스트 변화
            for(int i = 0; i<n; i++)
            {
                int c = player.Hands[i].Tags.Length;

                for(int j = 0; j<c; j++)
                {
                    if(player.Hands[i].Tags[j] == tag)
                    {
                        player.Hands[i].Cost += offset;
                        break;
                    }
                }
            }

            ActionManager.Instance.IsActionSuccess = true;

            yield break;
        }


        /// <summary>
        /// 자원 양 변화
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator DiffSource(params object[] args)
        {
            string sourceName = (string)args[0];
            int sourceDiff = (int)args[1];

            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            Type tp = typeof(PlayerInfo);
            FieldInfo fld = tp.GetField(sourceName);
            Source src = (Source)fld.GetValue(player);

            src.Amount += sourceDiff;

            ActionManager.Instance.IsActionSuccess = true;

            yield break;
        }

        /// <summary>
        /// 자원 생산량 변화
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator DiffSourceProduct(params object[] args)
        {
            string sourceName = (string)args[0];
            int sourceProductDiff = (int)args[1];

            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            Type tp = typeof(PlayerInfo);
            FieldInfo fld = tp.GetField(sourceName);
            Source src = (Source)fld.GetValue(player);

            src.Production += sourceProductDiff;

            ActionManager.Instance.IsActionSuccess = true;

            yield break;
        }

        /// <summary>
        /// 토큰 증가
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator IncreaseToken(params object[] args)
        {
            string cardName = (string)args[0];
            int diff = (int)args[1];

            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            int n = player.Hands.Count;

            for(int i = 0; i<n; i++)
            {
                if(player.Hands[i].ImageName == cardName)
                {
                    ((ActiveCardInfo)player.Hands[i]).TokenNum += diff;
                    ActionManager.Instance.IsActionSuccess = true;
                    yield break;
                }
            }

            yield break;
        }

        /// <summary>
        /// 대상을 선택하고,
        /// 그 대상의 자원 생산량을 변동시킴
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator TargetDiffSourceProduct(params object[] args)
        {
            PlayerUIManager.Instance.SelectPlayerPanel.gameObject.SetActive(true);

            Transform trans = PlayerUIManager.Instance.SelectPlayerPanel;

            int n = trans.childCount;

            string sourceName = (string)args[0];
            int diff = (int)args[1];

            for(int i = 0; i<n; i++)
            {
                Button button = trans.GetChild(i).GetComponent<Button>();

                button.onClick.RemoveAllListeners();

                button.onClick.AddListener(
                    delegate () { PlayerUIManager.Instance.DiffSourceProduct(i, sourceName, diff); });
            }

            yield break;
        }

        /// <summary>
        /// 타일을 놓을 때
        /// 특정한 타일이 놓였다면
        /// 카드의 토큰을 증가시킴
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator TileToToken(params object[] args)
        {
            string tileType = (string)args[0];

            //내가 놓은 타일 만 해당인가?
            bool isOnlyMe = (bool)args[1];

            //카드 사용한 플레이어 이름
            string playerName = (string)args[2];

            //사용된 카드 이름
            string cardName = (string)args[3];

            if(isOnlyMe)
            {
                if(BoardManager.Instance.LastTile.Owner == playerName)
                {
                    List<CardInfo> tmpList = 
                        PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex].Hands;

                    CardInfo card = null;
                    for(int i = 0; i<tmpList.Count; i++)
                    {
                        if(tmpList[i].ImageName == cardName)
                        {
                            card = tmpList[i];
                            break;
                        }
                    }

                    switch (tileType)
                    {
                        case "Water":
                            if(BoardManager.Instance.LastTile.TileType == 4)
                            {
                                ((ActiveCardInfo)card).TokenNum++;
                            }
                            break;
                        case "Forest":
                            if (BoardManager.Instance.LastTile.TileType == 2)
                            {
                                ((ActiveCardInfo)card).TokenNum++;
                            }
                            break;
                        case "City":
                            if (BoardManager.Instance.LastTile.TileType == 1)
                            {
                                ((ActiveCardInfo)card).TokenNum++;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            yield break;
        }

        /// <summary>
        /// 카드를 뽑음
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator DrawCard(params object[] args)
        {
            int n = (int)args[0];

            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            for(int i = 0; i<n; i++)
            {
                CardInfo card = Deck.Dequeue();

                if(card==null)
                {
                    CardShuffle();

                    while (!isDeckShuffled)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    card = Deck.Dequeue();
                }

                player.Hands.Add(card);
                if(player.Name == PhotonNetwork.player.NickName)
                {
                    card.Card.transform.SetParent(PlayerUIManager.Instance.MyHand);
                }
            }

            ActionManager.Instance.IsActionSuccess = true;

            yield break;
        }

        /// <summary>
        /// 타일 선택 함수
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator SelectTile(params object[] args)
        {
            ActionManager.Instance.StartCoroutine(ActionManager.Instance.SelectTile(args));

            yield break;
        }

        /// <summary>
        /// 자원을 선택하고 그 자원을 획득
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerator SelectAndGetResource(params object[] args)
        {
            string srcName1 = (string)args[0];
            string srcName2 = (string)args[1];

            int srcAmount1 = (int)args[2];
            int srcAmount2 = (int)args[3];

            PlayerInfo player 
                = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            if (player.Name
                == PhotonNetwork.playerName)
            {
                PlayerUIManager.Instance.SetSelectResourceImage
                    (srcName1, srcName2, srcAmount1, srcAmount2);
            }
            else
            {
                if(srcName1 == SelectedResourceName)
                {
                    Type tp = typeof(PlayerInfo);
                    FieldInfo fld = tp.GetField(srcName1);
                    Source src = (Source)fld.GetValue(player);

                    src.Amount += srcAmount1;
                }
                else
                {
                    Type tp = typeof(PlayerInfo);
                    FieldInfo fld = tp.GetField(srcName2);
                    Source src = (Source)fld.GetValue(player);

                    src.Amount += srcAmount2;
                }

                SelectedResourceName = string.Empty;

                ActionManager.Instance.IsActionSuccess = true;
            }

            yield break;
        }

        #endregion
    }
}