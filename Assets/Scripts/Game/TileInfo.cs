using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormingMars
{
    public class TileInfo : MonoBehaviour
    {
        #region public 변수
        //물 타일인가
        public bool IsWater = false;

        //라인, 타일 번호
        public int LineNum = 1;
        public int TileNum = 1;

        /// <summary>
        ///없음, 도시, 숲, 특수 타일인가
        ///없으면 0
        ///도시면 1
        ///숲이면 2
        ///특수 타일이면 3
        ///물이면 4
        /// </summary>
        public int TileType = 0;

        //타일 소유자 이름
        public string Owner = string.Empty;

        /// <summary>
        /// 타일에 있는 자원
        /// 0번째는 자원 종류
        /// 1번째는 자원 갯수
        /// </summary>
        public string[] Resource;
        #endregion

        #region private 변수
        //점수
        int point = 0;

        //타일 색깔
        Material MyMeterial;
        #endregion

        private void Start()
        {
            MyMeterial = GetComponent<Renderer>().material;

            //MyMeterial.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            //물 타일이라면 색을 얼음색으로
            if (IsWater)
                MyMeterial.color = Color.cyan;
            //아니면 모래 색으로
            else
                MyMeterial.color = new Color(197f / 255f, 174f / 255f, 145f / 255f);

            MyMeterial.color = new Color(MyMeterial.color.r, MyMeterial.color.g, MyMeterial.color.b, 0.2f);
        }

        //타일 설정
        //설정 하고 싶은 타일 타입과
        //소유자의 이름을 받아서 타일 설정
        public void SetTile(int tiletype, string ownername = null)
        {
            TileType = tiletype;
            Owner = ownername;

            switch(TileType)
            {
                //없는 경우
                case 0:
                    break;
                //도시인 경우
                case 1:
                    //주변 숲 타일의 개수 만큼 이 타일의 점수 증가
                    point = FindTileCount(2);
                    MyMeterial.color = Color.gray;
                    break;
                //숲인 경우
                case 2:
                    //타일의 점수를 1 증가
                    point = 1;
                    MyMeterial.color = Color.green;
                    break;
                //특수 타일 인 경우
                case 3:
                    break;
                //물인 경우
                case 4:
                    MyMeterial.color = Color.cyan;
                    break;
            }

            MyMeterial.color = new Color(MyMeterial.color.r, MyMeterial.color.g, MyMeterial.color.b, 1f);
        }

        //타일 타입을 인자로 받아서
        //이 타일 주변의 타일 중
        //해당 타입을 가진 타일의 개수 return
        public int FindTileCount(int tiletype)
        {
            //해당 타입의 타일 개수
            int count = 0;

            //리스트의 인덱스로 변환
            int tmptilenum = TileNum - 1;
            int tmplinenum = LineNum - 1;


            //특수 지형에 놓인 타일이라면 0을 return
            if (tmplinenum < 0)
                return 0;

            //5번째 라인 이하라면
            if (tmplinenum < 5)
            {
                //첫번째 라인이 아니라면 위에도 검사
                if (tmplinenum != 1)
                {
                    //윗 줄은 자신의 tmptilenum - 1 과 tmptilenum을 검사
                    if (BoardManager.Instance.Board[tmplinenum - 1][tmptilenum - 1].GetComponent<TileInfo>().TileType == tiletype)
                        count++;
                    if (BoardManager.Instance.Board[tmplinenum - 1][tmptilenum].GetComponent<TileInfo>().TileType == tiletype)
                        count++;
                }

                //아랫 줄은 자신의 tmptilenum과 tmptilenum + 1을 검사
                if (BoardManager.Instance.Board[tmplinenum + 1][tmptilenum].GetComponent<TileInfo>().TileType == tiletype)
                    count++;
                if (BoardManager.Instance.Board[tmplinenum + 1][tmptilenum + 1].GetComponent<TileInfo>().TileType == tiletype)
                    count++;
            }
            //5번째 라인이라면
            else if (tmplinenum == 5)
            {
                //윗 줄은 자신의 tmptilenum - 1 과 tmptilenum을 검사
                if (BoardManager.Instance.Board[tmplinenum - 1][tmptilenum - 1].GetComponent<TileInfo>().TileType == tiletype)
                    count++;
                if (BoardManager.Instance.Board[tmplinenum - 1][tmptilenum].GetComponent<TileInfo>().TileType == tiletype)
                    count++;

                //아랫 줄은 자신의 tmptilenum - 1 과 tmptilenum을 검사
                if (BoardManager.Instance.Board[tmplinenum + 1][tmptilenum - 1].GetComponent<TileInfo>().TileType == tiletype)
                    count++;
                if (BoardManager.Instance.Board[tmplinenum + 1][tmptilenum].GetComponent<TileInfo>().TileType == tiletype)
                    count++;
            }
            //5번째 라인 이상이라면
            else
            {
                //윗 줄은 자신의 tmptilenum과 tmptilenum + 1을 검사
                if (BoardManager.Instance.Board[tmplinenum - 1][tmptilenum].GetComponent<TileInfo>().TileType == tiletype)
                    count++;
                if (BoardManager.Instance.Board[tmplinenum - 1][tmptilenum + 1].GetComponent<TileInfo>().TileType == tiletype)
                    count++;

                //마지막 라인이 아니라면 밑에도 검사
                if (tmplinenum != 9)
                {
                    //아랫 줄은 자신의 tmptilenum - 1 과 tmptilenum을 검사
                    if (BoardManager.Instance.Board[tmplinenum + 1][tmptilenum - 1].GetComponent<TileInfo>().TileType == tiletype)
                        count++;
                    if (BoardManager.Instance.Board[tmplinenum + 1][tmptilenum].GetComponent<TileInfo>().TileType == tiletype)
                        count++;
                }
            }

            //상하 검사 끝
            //좌우 검사 시작

            //그 라인의 첫번째 타일이 아니라면
            if (tmptilenum != 1)
                //왼쪽 타일 검사
                if (BoardManager.Instance.Board[tmplinenum][tmptilenum - 1].GetComponent<TileInfo>().TileType == tiletype)
                    count++;

            //그 라인의 마지막 타일이 아니라면
            if (tmptilenum != BoardManager.Instance.Board[tmplinenum].Count)
                //오른쪽 타일 검사
                if (BoardManager.Instance.Board[tmplinenum][tmptilenum + 1].GetComponent<TileInfo>().TileType == tiletype)
                    count++;

            //개수 return
            return count;
        }
    }
}