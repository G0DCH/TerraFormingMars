using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormingMars
{
    public class BoardManager : Singleton<BoardManager>
    {
        //타일이 들어있는 보드판 리스트
        public List<List<GameObject>> Board;

        /// <summary>
        /// 기온.
        /// -30도에서 시작
        /// +8도가 끝
        /// </summary>
        public int CelciusDegree = -30;//기온

        /// <summary>
        /// 산소 농도.
        /// 0%에서 시작
        /// 14%가 끝
        /// </summary>
        public int Oxygen = 0;

        /// <summary>
        /// 물.
        /// 0개에서 시작
        /// 9개가 끝
        /// </summary>
        public int Water = 0;


        /// <summary>
        /// 세대
        /// </summary>
        public int Generation = 0;

        const string TILENAME = "Tile";
        const string LINENAME = "Line";

        /// <summary>
        /// 열 화살표
        /// </summary>
        public GameObject HeatArrow;

        /// <summary>
        /// 산소 화살표
        /// </summary>
        public GameObject OxygenArrow;

        /// <summary>
        /// 세대 표기
        /// </summary>
        public GameObject GenerationStone;

        /// <summary>
        /// 차례, 세대, 남은 행동
        /// </summary>
        public UnityEngine.UI.Text[] GameInfoText;

        /// <summary>
        /// 마지막으로 놓인 타일
        /// </summary>
        public TileInfo LastTile;

        private void Start()
        {
            //리스트 초기화
            Board = new List<List<GameObject>>();
            for (int i = 0; i < 9; i++)
            {
                Board.Add(new List<GameObject>());
            }

            GameObject tmp, tmpc;//라인과 타일

            //리스트에 타일을 채워 넣음
            for (int i = 0; i < 9; i++)
            {
                //라인 찾음
                tmp = GameObject.Find(LINENAME + (i + 1).ToString());

                if (tmp != null)
                {
                    int limit;
                    //5번째 라인 이하라면 최대 타일 갯수가 숫자가 증가 할 때 마다 1 증가
                    if (i < 5)
                        limit = 5 + i;
                    else//6번째 라인 부터는 최대 타일 갯수가 1씩 감소
                        limit = 9 + (4 - i);
                    for (int j = 0; j < limit; j++)
                    {
                        //타일 찾아서 리스트에 넣음
                        if ((tmpc = tmp.transform.Find(TILENAME + (j + 1).ToString()).gameObject) != null)
                        {
                            Board[i].Add(tmpc);
                        }
                        else
                            Debug.Log("No Tile");
                    }
                }
                else
                    Debug.Log("No Line");
            }

            StartCoroutine("HeatArrowUpdater");
            StartCoroutine("OxygenArrowUpdater");
            StartCoroutine("GenerationUpdater");
        }

        /// <summary>
        /// 기온 갱신
        /// </summary>
        /// <returns></returns>
        IEnumerator HeatArrowUpdater()
        {
            int heatdiff = 0;//현재온도 - (-28)도
            while (true)
            {
                if (CelciusDegree <= -30)
                {
                    HeatArrow.transform.localPosition
                        = new Vector3(630, -400, 0);
                    CelciusDegree = -30;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    if (CelciusDegree >= 8)
                        CelciusDegree = 8;
                    heatdiff = CelciusDegree - (-28);

                    HeatArrow.transform.localPosition
                        = new Vector3(630, -330 + heatdiff * 23, 0);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        IEnumerator OxygenArrowUpdater()
        {
            Vector3 Center = new Vector3(0f, -28.828125f, 0);
            Vector3 StartPoint = new Vector3(-445f, 510f, 0);
            Vector3 EndPoint = new Vector3(445f, 510f, 0);
            float r = (StartPoint - Center).magnitude;

            float dotresult = Vector3.Dot(StartPoint - Center, EndPoint - Center);

            float theta = Mathf.Acos(dotresult / r / r) / 14;

            //0은 23, 14는 9
            float trueOx = 23 - Oxygen;

            while (true)
            {
                if (Oxygen <= 0)
                    Oxygen = 0;
                else if (Oxygen >= 14)
                    Oxygen = 14;
                trueOx = 23 - Oxygen;

                OxygenArrow.transform.localPosition
                    = new Vector3(r * Mathf.Cos(theta * trueOx),
                    r * Mathf.Sin(theta * trueOx)) + Center;

                yield return new WaitForEndOfFrame();
            }

            //yield break;
        }

        IEnumerator GenerationUpdater()
        {
            float offset_x, offset_y;
            Vector3 basePos = GenerationStone.transform.localPosition;
            float topPos = 9.56f;
            float rightPos = 11.33f;

            offset_y = (topPos - basePos.y) / 24;
            offset_x = rightPos * 2 / 25;

            while(true)
            {
                if (Generation < 1)
                    Generation = 1;
                else if (Generation > 100)
                    Generation = 100;

                if(Generation>0 && Generation<=25)
                {
                    GenerationStone.transform.localPosition = basePos + Vector3.up * offset_y * (Generation-1);
                }
                else if(Generation>25&&Generation<=50)
                {
                    GenerationStone.transform.localPosition =
                        basePos + Vector3.up * offset_y * 24
                        + Vector3.right * offset_x * (Generation - 25);
                }
                else if(Generation>50&&Generation<=75)
                {
                    GenerationStone.transform.localPosition =
                        basePos + Vector3.right * offset_x * 25
                        + Vector3.up * offset_y * (74 - Generation);
                }
                else
                {
                    GenerationStone.transform.localPosition =
                        basePos + Vector3.right * offset_x * (100 - Generation)
                        - Vector3.up * offset_y;
                }

                yield return new WaitForEndOfFrame();
            }

            //yield break;
        }

        /// <summary>
        /// 차례, 세대, 남은 행동을 보여주는 패널 업데이트
        /// </summary>
        public void GameInfoUpdate()
        {
            GameInfoText[0].text = "차례 : " 
                + GameManager.Instance.PlayerList[GameManager.Instance.TurnPlayerIndex];
            GameInfoText[1].text = "세대 : " + Generation.ToString();
            GameInfoText[2].text = "남은 행동 : " + GameManager.Instance.LeftAction.ToString();
        }
    }
}