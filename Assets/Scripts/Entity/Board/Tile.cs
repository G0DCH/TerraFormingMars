using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity.Board
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileData myTileData;
        public TileData MyTileData { get { return myTileData; } }

        [SerializeField]
        private List<Tile> nearTiles;

        private void Start()
        {
            InitNearTiles();
        }

        public int GetScore()
        {
            int score = 0;

            if(MyTileData.TileType == TileType.Tree)
            {
                score = 1;
            }
            else if(MyTileData.TileType == TileType.City)
            {
                //주변의 나무 1개마다 1점씩 추가
                foreach(Tile nearTile in nearTiles)
                {
                    if(nearTile.MyTileData.TileType == TileType.Tree)
                    {
                        score++;
                    }
                }
            }
            else if(MyTileData.TileType == TileType.Capital)
            {
                //주변의 물과 나무 1개마다 1점씩 추가
                foreach (Tile nearTile in nearTiles)
                {
                    if (nearTile.MyTileData.TileType == TileType.Tree)
                    {
                        score++;
                    }
                    else if(nearTile.MyTileData.TileType == TileType.Ocean)
                    {
                        score++;
                    }
                }
            }
            else if(MyTileData.TileType == TileType.Market)
            {
                //주변의 도시 1개 마다 1점씩 추가
                foreach (Tile nearTile in nearTiles)
                {
                    if (nearTile.MyTileData.TileType == TileType.City)
                    {
                        score++;
                    }
                }
            }

            return score;
        }

        private void InitNearTiles()
        {
            string lineName = transform.parent.name;
            string myName = transform.name;

            int lineNum = int.Parse(lineName[4].ToString());
            int tileNum = int.Parse(myName[4].ToString());

            const string LINE = "Line";
            const string TILE = "Tile";

            //위 아래 추가
            if(lineNum <5)
            {
                if(lineNum != 1)
                {
                    //위에 라인 추가
                    GameObject upperLine = GameObject.Find(LINE + (lineNum - 1).ToString());

                    //타일 번호가 1번이나 라인의 마지막 번호가 아니면 두개 추가
                    if (tileNum != 1 && tileNum != transform.parent.childCount)
                    {                        
                        nearTiles.Add(upperLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                        nearTiles.Add(upperLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
                    }
                    else if(tileNum == 1)
                    {
                        nearTiles.Add(upperLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
                    }
                    else if(tileNum == transform.parent.childCount)
                    {
                        nearTiles.Add(upperLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                    }
                }
                
                //아래 라인 추가
                GameObject lowerLine = GameObject.Find(LINE + (lineNum + 1).ToString());

                nearTiles.Add(lowerLine.transform.Find(TILE + (tileNum + 1).ToString()).GetComponent<Tile>());
                nearTiles.Add(lowerLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
            }
            else if(lineNum == 5)
            {
                //위에 라인 추가
                GameObject upperLine = GameObject.Find(LINE + (lineNum - 1).ToString());

                //아래 라인 추가
                GameObject lowerLine = GameObject.Find(LINE + (lineNum + 1).ToString());

                //타일 번호가 1번이나 라인의 마지막 번호가 아니면 두개 추가
                if (tileNum != 1 && tileNum != transform.parent.childCount)
                {
                    nearTiles.Add(upperLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                    nearTiles.Add(upperLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());

                    nearTiles.Add(lowerLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                    nearTiles.Add(lowerLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
                }
                else if (tileNum == 1)
                {
                    nearTiles.Add(upperLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());

                    nearTiles.Add(upperLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
                }
                else if (tileNum == transform.parent.childCount)
                {
                    nearTiles.Add(upperLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());

                    nearTiles.Add(upperLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                }
            }
            else if(lineNum > 5)
            {
                if (lineNum != 9)
                {
                    //아래 라인 추가
                    GameObject lowerLine = GameObject.Find(LINE + (lineNum + 1).ToString());

                    //타일 번호가 1번이나 라인의 마지막 번호가 아니면 두개 추가
                    if (tileNum != 1 && tileNum != transform.parent.childCount)
                    {
                        nearTiles.Add(lowerLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                        nearTiles.Add(lowerLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
                    }
                    else if (tileNum == 1)
                    {
                        nearTiles.Add(lowerLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
                    }
                    else if (tileNum == transform.parent.childCount)
                    {
                        nearTiles.Add(lowerLine.transform.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                    }
                }

                //위에 라인 추가
                GameObject upperLine = GameObject.Find(LINE + (lineNum - 1).ToString());

                nearTiles.Add(upperLine.transform.Find(TILE + (tileNum + 1).ToString()).GetComponent<Tile>());
                nearTiles.Add(upperLine.transform.Find(TILE + tileNum.ToString()).GetComponent<Tile>());
            }

            //위 아래 끝 좌우 추가 시작

            //타일 번호가 1번이나 라인의 맨 끝 번호가 아니라면
            if(tileNum != 1 && tileNum != transform.parent.childCount)
            {
                nearTiles.Add(transform.parent.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
                nearTiles.Add(transform.parent.Find(TILE + (tileNum + 1).ToString()).GetComponent<Tile>());
            }
            else if(tileNum == 1)
            {
                nearTiles.Add(transform.parent.Find(TILE + (tileNum + 1).ToString()).GetComponent<Tile>());
            }
            else if(tileNum == transform.parent.childCount)
            {
                nearTiles.Add(transform.parent.Find(TILE + (tileNum - 1).ToString()).GetComponent<Tile>());
            }
        }
    }
}