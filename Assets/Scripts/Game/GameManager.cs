using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

namespace TerraFormingMars
{
    public class GameManager : Photon.MonoBehaviour
    {
        /// <summary>
        /// 게임 모드
        /// <para>SelectCorpsAndCards : 기업, 카드 선택.</para>
        /// <para>DoGeneration : 세대 진행.</para>
        /// <para>EndAndStartGeneration : 세대 종료 및 자원획득, 카드 분배.</para>
        /// <para>EndTerraFormming : 게임 종료 조건 성립.</para>
        /// <para>GameEnd : 게임 종료. 승자 발표.</para>
        /// </summary>
        public enum GameMode { SelectCorpsAndCards, DoGeneration, EndAndStartGeneration, EndTerraFormming, GameEnd};

        [Tooltip("현재 게임이 어느 상태에 있는가")]
        public static GameMode GAMEMODE = GameMode.SelectCorpsAndCards;

        public static GameManager Instance;

        /// <summary>
        /// 마스터 클라이언트의 플레이어 리스트
        /// </summary>
        public string[] PlayerList;

        /// <summary>
        /// 현재 턴을 가진 플레이어의 인덱스
        /// </summary>
        public int TurnPlayerIndex;

        /// <summary>
        /// 현재 턴을 가진 플레이어의 남은 행동
        /// </summary>
        public int LeftAction = 2;

        public bool[] IsGenerationEnd;

        private void Start()
        {
            //기업, 카드 선택 모드로 설정
            GAMEMODE = GameMode.SelectCorpsAndCards;

            Instance = this;

            PlayerUIManager.Instance.SelectCorpsCards.SetActive(true);
            PlayerUIManager.Instance.ResultPanel.SetActive(false);

            PlayerManager.Instance.MakePlayer();
        }

        #region PunRPC 함수 호출 함수

        /// <summary>
        /// 기업 인덱스 리스트를 받아서
        /// 그 인덱스에 해당하는 기업 이미지를 내 화면에 띄움
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="CorpIndList"></param>
        /// <param name="playerList"></param>
        public void SendCorpIndList(PhotonTargets targets, int[] CorpIndList, string[] playerList)
        {
            photonView.RPC("SendCorpIndexList", targets, CorpIndList, playerList);
        }

        /// <summary>
        /// 카드 인덱스 리스트를 받아서 덱 구성
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="CardIndList"></param>
        public void SendShuffleCardIndList(PhotonTargets targets, int[] CardIndList)
        {
            photonView.RPC("SendShuffleCardIndexList",targets, CardIndList);
        }

        /// <summary>
        /// 선택한 기업과 카드를 받아서 플레이어 초기화
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="CorpName"></param>
        /// <param name="CardName"></param>
        /// <param name="playerName"></param>
        public void SendSCorpsAndCards(PhotonTargets targets, string CorpName, string[] CardName, string playerName)
        {
            photonView.RPC("SendSelectedCorpsAndCards", targets, CorpName, CardName, playerName);
        }

        /// <summary>
        /// 첫 턴인 플레이어의 인덱스를 알리고
        /// 구매한 카드 수를 알려줌
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="FirstIndex"></param>
        public void SendFTurn(PhotonTargets targets, int FirstIndex)
        {
            photonView.RPC("SendFirstTurn", targets, FirstIndex);
        }

        /// <summary>
        /// 턴 엔드 함수
        /// </summary>
        /// <param name="targets"></param>
        public void SendTEnd(PhotonTargets targets)
        {
            photonView.RPC("SendTurnEnd", targets);
        }

        /// <summary>
        /// 세대 종료 함수
        /// </summary>
        /// <param name="targets"></param>
        public void SendGEnd(PhotonTargets targets)
        {
            photonView.RPC("SendGenerationEnd", targets);
        }

        /// <summary>
        /// 선택한 자원 이름 보냄
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="name"></param>
        public void SendSResourceName(PhotonTargets targets, string name)
        {
            photonView.RPC("SendSelectedResourceName", targets, name);
        }

        #endregion


        #region PunRPC 함수
        /// <summary>
        /// 기업 인덱스 리스트를 받아서
        /// 그 인덱스에 해당하는 기업 이미지를 내 화면에 띄움
        /// </summary>
        /// <param name="CorpIndList">기업 인덱스 리스트</param>
        /// <param name="playerList">마스터 클라이언트의 플레이어 리스트</param>
        [PunRPC]
        void SendCorpIndexList(int[] CorpIndList, string[] playerList)
        {
            Debug.Log("SendCorpIndexList 시작");

            PlayerList = playerList;

            IsGenerationEnd = new bool[playerList.Length];

            for(int i = 0; i<IsGenerationEnd.Length; i++)
            {
                IsGenerationEnd[i] = false;
            }

            int num = 0;

            //내 인덱스 찾음
            for (int i = 0; i < PlayerList.Length; i++)
            {
                if (PlayerList[i] == PhotonNetwork.playerName)
                    num = i;
            }

            int num1 = CorpIndList[num * 2];
            int num2 = CorpIndList[num * 2 + 1];

            Corporation corp1, corp2;

            GameObject check = GameObject.Find("CheckImage");

            corp1 = CorpsManager.Instance.Corps[num1];
            corp2 = CorpsManager.Instance.Corps[num2];

            corp1.CorpImage.transform.SetParent
                    (corp1.CorpImage.transform.parent.parent.Find("SelectCorpsCards"));

            corp1.CorpImage.transform.localPosition = new Vector3(-500, 250, 0);

            corp1.CorpImage.GetComponent<Image>().color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);

            corp2.CorpImage.transform.SetParent
                    (corp2.CorpImage.transform.parent.parent.Find("SelectCorpsCards"));

            corp2.CorpImage.transform.localPosition = new Vector3(500, 250, 0);

            corp2.CorpImage.GetComponent<Image>().color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);



            corp1.CorpImage.GetComponent<Button>().onClick.AddListener
                    (delegate () { PlayerUIManager.Instance.Card_CorpsSelected(corp1.CorpImage, corp2.CorpImage, check); });

            PlayerUIManager.Instance.Card_CorpsSelected(corp1.CorpImage, corp2.CorpImage, check);

            corp2.CorpImage.GetComponent<Button>().onClick.AddListener
                    (delegate () { PlayerUIManager.Instance.Card_CorpsSelected(corp2.CorpImage, corp1.CorpImage, check); });

        }

        /// <summary>
        /// 카드 인덱스 리스트를 받아서 덱 구성
        /// </summary>
        /// <param name="CardIndList">카드 인덱스 리스트</param>
        [PunRPC]
        void SendShuffleCardIndexList(int[] CardIndList)
        {
            CardManager.Instance.MakeDeck(CardIndList);
        }

        /// <summary>
        /// 선택한 기업, 카드들을 보내면
        /// 해당 기업, 카드로
        /// PlayerInfo 초기화
        /// </summary>
        /// <param name="CorpName">선택한 기업 이름</param>
        /// <param name="CardName">선택한 카드 이름</param>
        /// <param name="playerName">선택한 플레이어 이름</param>
        [PunRPC]
        void SendSelectedCorpsAndCards(string CorpName, string[] CardName, string playerName)
        {
            for (int i = 0; i < PlayerList.Length; i++)
            {
                if (PlayerList[i] == playerName)
                {
                    CardManager.Instance.BuyCardCount[i] = CardName.Length;

                    for (int j = 0; j < CorpsManager.Instance.Corps.Count; j++)
                    {
                        //플레이어에게 기업과 패를 할당해줌
                        if (CorpsManager.Instance.Corps[j].CorpName == CorpName)
                        {
                            PlayerInfo tmp = new PlayerInfo(playerName, CorpsManager.Instance.Corps[j]);
                            if (playerName == PhotonNetwork.player.NickName)
                            {
                                PlayerManager.Instance.Me = tmp;
                                PlayerManager.Instance.players.Add(PlayerManager.Instance.Me);
                            }
                            else
                                PlayerManager.Instance.players.Add(tmp);

                            //선택한 카드를 플레이어의 패에 넣어줌
                            for (int k = 0; k < CardName.Length; k++)
                            {
                                for (int l = 0; l < CardManager.Instance.PlayerHandedCards[i].Count; l++)
                                {
                                    if (CardManager.Instance.PlayerHandedCards[i][l].ImageName == CardName[k])
                                    {
                                        tmp.Hands.Add(CardManager.Instance.PlayerHandedCards[i][l]);
                                        if (playerName == PhotonNetwork.player.NickName)
                                        {
                                            CardManager.Instance.PlayerHandedCards[i][l].Card.transform.SetParent
                                                (PlayerUIManager.Instance.MyHand, false);
                                        }
                                        tmp.MC.Amount -= 3;
                                        break;
                                    }
                                }
                            }

                            //선택하지 않은 카드는 버린 카드 더미에 넣음
                            for (int k = 0; k < CardManager.Instance.PlayerHandedCards[i].Count; k++)
                            {
                                //카드가 선택한 카드 내에 존재 하는가
                                bool IsCardExist = false;

                                for (int l = 0; l < CardName.Length; l++)
                                {
                                    if (CardManager.Instance.PlayerHandedCards[i][k].ImageName == CardName[l])
                                    {
                                        IsCardExist = true;
                                        break;
                                    }
                                }

                                if(!IsCardExist)
                                {
                                    CardInfo tmpCardInfo = CardManager.Instance.PlayerHandedCards[i][k];
                                    tmpCardInfo.Card.transform.SetParent(CardManager.Instance.DiscardTrans, false);
                                    tmpCardInfo.Card.transform.localPosition = Vector3.zero;
                                    tmpCardInfo.Card.GetComponent<Image>().color = Color.white;
                                    CardManager.Instance.Cards.Add(tmpCardInfo);
                                }
                            }

                            if (playerName == PhotonNetwork.player.NickName)
                            {
                                PlayerManager.Instance.SourceUpdater();
                                CardManager.Instance.ClearCardGrid();
                            }

                            CardManager.Instance.PlayerHandedCards[i].Clear();
                            goto End;
                        }
                    }
                }
            }
        End:
            //모든 플레이어가 기업과 카드를 선택했다면 플레이어 리스트 정렬
            if (PlayerManager.Instance.players.Count == PlayerList.Length)
            {
                //Debug.Log("게임 시작");

                List<PlayerInfo> tmpList = new List<PlayerInfo>();
                int n = PlayerManager.Instance.players.Count;

                for (int i = 0; i < n; i++) 
                {
                    for(int j = 0; j<n; j++)
                    {
                        if(PlayerList[i] == PlayerManager.Instance.players[j].Name)
                        {
                            tmpList.Add(PlayerManager.Instance.players[j]);
                            break;
                        }
                    }
                }

                PlayerManager.Instance.players.Clear();
                PlayerManager.Instance.players = tmpList;


                for (int i = 0; i < n; i++)
                {
                    GameObject obj =
                        Instantiate(PlayerManager.Instance.players[i].Corporation.CorpImage,
                        PlayerUIManager.Instance.SelectPlayerPanel);

                    obj.GetComponent<Button>().onClick.RemoveAllListeners();
                }

                //마스터 클라이언트라면 첫 턴을 가질 플레이어를 선택하고
                //모든 플레이어에게 알림
                if(PhotonNetwork.isMasterClient)
                {
                    int num = Random.Range(0, PlayerList.Length);
                    SendFTurn(PhotonTargets.All, num);
                }
            }
        }

        /// <summary>
        /// 첫 턴을 시작할 플레이어의 인덱스를 받음
        /// </summary>
        /// <param name="FirstIndex">첫 턴을 시작할 플레이어의 인덱스</param>
        [PunRPC]
        void SendFirstTurn(int FirstIndex)
        {
            TurnPlayerIndex = FirstIndex;
            LeftAction = 2;

            PlayerUIManager.Instance.ShowResultPanel(FirstIndex);
        }

        /// <summary>
        /// 턴 엔드를 한 경우
        /// 다음 플레이어에게 턴을 넘김
        /// </summary>
        [PunRPC]
        void SendTurnEnd()
        {
            do
            {
                TurnPlayerIndex = (TurnPlayerIndex + 1) % PlayerList.Length;
            }
            while (IsGenerationEnd[TurnPlayerIndex]);

            LeftAction = 2;
            BoardManager.Instance.GameInfoUpdate();
        }

        /// <summary>
        /// 세대 종료 시킴
        /// </summary>
        [PunRPC]
        void SendGenerationEnd()
        {
            IsGenerationEnd[TurnPlayerIndex] = false;
        }

        /// <summary>
        /// 선택한 자원 이름 보냄
        /// </summary>
        /// <param name="name">선택한 자원 이름</param>
        [PunRPC]
        void SendSelectedResourceName(string name)
        {
            CardManager.Instance.SelectedResourceName = name;
        }
        #endregion
    }
}