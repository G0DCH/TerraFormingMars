using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;
using TerraFormingMars;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    public GameObject WaitingText;

    public GameObject SelectCorpsCards;

    public GameObject ResultPanel;

    public GameObject PlayerResultPrefab;

    public GameObject FirstTurnText;

    public GameObject CorpNameImage;

    public Transform SelectPlayerPanel;

    public Transform SelectResourcePanel;

    public Transform SelectTokenCardPanel;

    /// <summary>
    /// 내 패를 넣어둘 게임 오브젝트
    /// </summary>
    public Transform MyHand;

    private void Start()
    {
        StartCoroutine(EscAction());
    }

    public void ShowButton(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void CloseButton(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// 카드나 기업이 클릭되면
    /// 밝아지거나 어두워짐
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="obj_2"></param>
    /// <param name="obj_3"></param>
    public void Card_CorpsSelected(GameObject obj, GameObject obj_2 = null, GameObject obj_3 = null)
    {
        Image image = obj.GetComponent<Image>();

        if(image.color == Color.white)
        {
            if(obj_2 == null)
            {
                image.color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);
                CardManager.Instance.SelectedCards.Remove(obj);
                obj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            image.color = Color.white;

            if (obj_2 != null)
            {
                obj_2.GetComponent<Image>().color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);

                for (int i = 0; i < CorpsManager.Instance.Corps.Count; i++)
                {
                    //내 기업 저장
                    if (CorpsManager.Instance.Corps[i].CorpName == obj.name)
                    {
                        CorpsManager.Instance.myCorporation
                            = CorpsManager.Instance.Corps[i];
                    }
                    //내가 선택 하지 않은 기업 저장
                    if (CorpsManager.Instance.Corps[i].CorpName == obj_2.name)
                        CorpsManager.Instance.otherCorporation
                            = CorpsManager.Instance.Corps[i];
                }

                if (obj_3 != null)
                {
                    obj_3.transform.SetParent(obj.transform);
                    obj_3.transform.localPosition = new Vector3(-250, 200, 0);
                }
            }
                
            else
            {
                CardManager.Instance.SelectedCards.Add(obj);
                obj.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 선택한 카드, 기업을 전송함
    /// </summary>
    public void SelectEndButton(Button button = null)
    {
        string[] CardName = new string[CardManager.Instance.SelectedCards.Count];

        for(int i = 0; i<CardName.Length; i++)
        {
            CardName[i] = CardManager.Instance.SelectedCards[i].name;
        }

        WaitingText.SetActive(true);

        SelectCorpsCards.SetActive(false);

        if (CorpsManager.Instance.otherCorporation.CorpImage.transform.parent.gameObject.name != "Corps")
        {
            CorpsManager.Instance.otherCorporation.CorpImage.transform.SetParent
                (SelectCorpsCards.transform.parent.Find("Corps"), false);
            CorpsManager.Instance.otherCorporation.CorpImage.transform.localPosition = Vector3.zero;

            Transform trans = GameObject.Find("MyPanel").transform;


            Corporation myCorps = CorpsManager.Instance.myCorporation;
            myCorps.CorpImage.transform.SetParent(trans, false);
            myCorps.CorpImage.transform.localPosition = new Vector3(0, -310, 0);
            myCorps.CorpImage.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(myCorps.CorpImage.transform.GetChild(0).gameObject);
            //trans.gameObject.SetActive(false);
        }

        if (button != null)
            GameManager.Instance.SendSCorpsAndCards(PhotonTargets.All,
                CorpsManager.Instance.myCorporation.CorpName, CardName, PhotonNetwork.player.NickName);
    }

    /// <summary>
    /// 카드 구매 결과와
    /// 첫 턴이 누구인가를 보여줌
    /// </summary>
    /// <param name="n">첫 플레이어의 인덱스</param>
    public void ShowResultPanel(int n)
    {
        ResultPanel.SetActive(true);
        WaitingText.SetActive(false);

        Transform PlayerResultGrid = ResultPanel.transform.Find("PlayerResultGrid");

        if(PlayerResultGrid.childCount == 0)
        {
            Transform trans = GameObject.Find("Others").transform.Find("Viewport").Find("Content");
                
            for (int i = 0; i<GameManager.Instance.PlayerList.Length; i++)
            {
                GameObject tmp = Instantiate(PlayerResultPrefab, PlayerResultGrid);
                //이미지 복사
                tmp.transform.Find("CorpImage").GetComponent<Image>().sprite =
                    PlayerManager.Instance.players[i].Corporation.CorpImage.GetComponent<Image>().sprite;
                tmp.transform.Find("PlayerName").GetComponent<Text>().text
                    = PlayerManager.Instance.players[i].Name;
                tmp.transform.Find("BuyCardCount").GetComponent<Text>().text
                    = "구매한 카드 수 : " + CardManager.Instance.BuyCardCount[i];

                CardManager.Instance.BuyCardCount[i] = -1;

                if (i == n)
                {
                    FirstTurnText.transform.SetParent(PlayerResultGrid.GetChild(i));
                    FirstTurnText.transform.localPosition = new Vector3(0, 175, 0);
                }

                if (GameManager.Instance.PlayerList[i] != PlayerManager.Instance.Me.Name)
                {
                    //기업 이름 이미지
                    GameObject obj = Instantiate(CorpNameImage, trans);
                    obj.GetComponent<Image>().sprite = Resources.Load("Corps/Corps_Name/"
                            + PlayerManager.Instance.players[i].Corporation.ImageName
                            + "_Name", typeof(Sprite)) as Sprite;
                }

            }
        }
        else
        {
            for(int i = 0; i<GameManager.Instance.PlayerList.Length;i++)
            {
                PlayerResultGrid.GetChild(i).Find("BuyCardCount").GetComponent<Text>().text
                    = "구매한 카드 수 : " + CardManager.Instance.BuyCardCount[i];
                CardManager.Instance.BuyCardCount[i] = -1;

                if (i == n)
                {
                    FirstTurnText.transform.SetParent(PlayerResultGrid.GetChild(i));
                    FirstTurnText.transform.localPosition = new Vector3(0, 175, 0);
                }
            }

            BoardManager.Instance.Generation++;
        }

        BoardManager.Instance.GameInfoUpdate();
    }

    /// <summary>
    /// 턴 엔드 버튼
    /// </summary>
    public void TurnEndButton()
    {
        if (PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex].Name
            == PlayerManager.Instance.Me.Name
            && GameManager.Instance.LeftAction < 2)
        {
            GameManager.Instance.SendTEnd(PhotonTargets.All);
        }
    }

    /// <summary>
    /// 세대 종료 버튼
    /// </summary>
    public void GenerationEndButton()
    {
        if (PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex].Name
            == PlayerManager.Instance.Me.Name)
        {
            GameManager.Instance.SendGEnd(PhotonTargets.All);
            GameManager.Instance.SendTEnd(PhotonTargets.All);
            return;
        }
          
    }

    /// <summary>
    /// 자원 생산량 변동 시킴
    /// </summary>
    /// <param name="i">플레이어 인덱스</param>
    /// <param name="sourcename">자원 이름</param>
    /// <param name="diff">변동량</param>
    public void DiffSourceProduct(int i, string sourcename, int diff)
    {
        PlayerInfo player = PlayerManager.Instance.players[i];

        Type tp = typeof(PlayerInfo);
        FieldInfo fld = tp.GetField(sourcename);
        Source src = (Source)fld.GetValue(player);

        src.Production += diff;

        ActionManager.Instance.IsActionSuccess = true;
    }

    /// <summary>
    /// esc키를 누르면 다른 창이 모두 꺼짐
    /// </summary>
    /// <returns></returns>
    IEnumerator EscAction()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.Escape))
                ResultPanel.SetActive(false);
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 자원 이름에 해당하는
    /// 자원 이미지 설정
    /// 아직 미완성
    /// </summary>
    /// <param name="Name1"></param>
    /// <param name="Name2"></param>
    /// <param name="Offset1"></param>
    /// <param name="Offset2"></param>
    public void SetSelectResourceImage(string Name1, string Name2, int Offset1, int Offset2)
    {
        Transform tmp = SelectResourcePanel.GetChild(0);

        tmp.GetComponent<Image>().sprite
            = Resources.Load("ResourceIcons/"
                        + Name1, typeof(Sprite)) as Sprite;

        tmp.GetChild(0).GetComponent<Text>().text = Offset1.ToString();

        tmp.GetComponent<Button>().onClick.RemoveAllListeners();

        if (Name1 != "Animal" && Name1 != "Microbe")
            tmp.GetComponent<Button>().onClick.AddListener(
                delegate () { ActionManager.Instance.CardFuncAction("DiffSource", Name1, Offset1); });
        else
        {
            tmp.GetComponent<Button>().onClick.AddListener(
                delegate () { SetSelectTokenCards(Name1, Offset1); });
        }

        tmp = SelectResourcePanel.GetChild(1);

        tmp.GetComponent<Image>().sprite
            = Resources.Load("ResourceIcons/"
                        + Name2, typeof(Sprite)) as Sprite;

        tmp.GetChild(0).GetComponent<Text>().text = Offset2.ToString();

        tmp.GetComponent<Button>().onClick.RemoveAllListeners();

        if (Name2 != "Animal" && Name2 != "Microbe")
            tmp.GetComponent<Button>().onClick.AddListener(
                delegate () { ActionManager.Instance.CardFuncAction("DiffSource", Name2, Offset2); });
        else
        {
            tmp.GetComponent<Button>().onClick.AddListener(
                delegate () { SetSelectTokenCards(Name2, Offset2); });
        }
    }

    /// <summary>
    /// 인자로 들어온 토큰을 지닌 카드를
    /// 그리드에 넣음
    /// </summary>
    /// <param name="TokenName"></param>
    public void SetSelectTokenCards(string TokenName, int TokenNum)
    {
        List<CardInfo> cards =  PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex].Hands;

        Transform grid = SelectTokenCardPanel.GetChild(0).GetChild(0).GetChild(0);

        Transform tmp;
        while ((tmp = grid.GetChild(0)) != null)
            Destroy(tmp.gameObject);

        GameObject tmpObj;
        Button tmpButton;

        for(int i = 0; i<cards.Count; i++)
        {
            if(cards[i].Category == "Active")
            {
                if(((ActiveCardInfo)cards[i]).TokenType == TokenName)
                {
                    tmpObj = Instantiate(cards[i].Card, grid, false);
                    tmpButton = tmpObj.GetComponent<Button>();
                    tmpButton.onClick.RemoveAllListeners();
                    tmpButton.onClick.AddListener
                        (delegate () { ActionManager.Instance.CardFuncAction("IncreaseToken",tmpObj.name,TokenNum); });
                }
            }
        }

    }
}
