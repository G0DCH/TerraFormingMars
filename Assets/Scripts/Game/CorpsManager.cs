using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


namespace TerraFormingMars
{
    /// <summary>
    /// 기업
    /// </summary>
    public class Corporation
    {
        /// <summary>
        /// 이미지 파일 이름
        /// </summary>
        public string ImageName;

        /// <summary>
        /// 기업 이름
        /// </summary>
        public string CorpName;

        /// <summary>
        /// 기업 태그
        /// </summary>
        public string Tag;

        /// <summary>
        /// 기업 태그 갯수
        /// </summary>
        public int TagNum;

        /// <summary>
        /// 시작 자원.
        /// </summary>
        public string[] StartResource;

        /// <summary>
        /// 시작 자원 양
        /// </summary>
        public int[] StartResourceNum;

        /// <summary>
        /// 시작 자원 생산량.이름
        /// </summary>
        public string[] StartResourceProduct;

        /// <summary>
        /// 시작 자원 생산량
        /// </summary>
        public int[] StartResourceProductNum;

        /// <summary>
        /// 기업 효과 이름
        /// </summary>
        public string CorpEffectName;

        /// <summary>
        /// 기업 이미지
        /// </summary>
        public GameObject CorpImage;

        public Corporation()
        {
            ImageName = null;
            CorpName = null;
            Tag = null;
            TagNum = 0;
            StartResource = null;
            StartResourceNum = null;
            StartResourceProduct = null;
            StartResourceProductNum = null;
            CorpEffectName = null;
        }

        public Corporation(string myImageName, string myCorpName,
            string myTag, int myTagNum, string[] myStartResource,
            int[] myStartResourceNum,
            string[] myStartResourceProduct,
            int[] myStartResourceProductNum,
            string myCorpEffectName)
        {
            ImageName = myImageName;
            CorpName = myCorpName;
            Tag = myTag;
            TagNum = myTagNum;
            StartResource = myStartResource;
            StartResourceNum = myStartResourceNum;
            StartResourceProduct = myStartResourceProduct;
            StartResourceProductNum = myStartResourceProductNum;
            CorpEffectName = myCorpEffectName;
        }
    }

    public class CorpsManager : Singleton<CorpsManager>
    {
        public List<Corporation> Corps = new List<Corporation>();

        public GameObject CorpsImage;

        public Transform CorpTrans;

        /// <summary>
        /// 내가 선택한 기업
        /// </summary>
        public Corporation myCorporation;

        public Corporation otherCorporation;

        public void LoadCorps()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("InfoJSONFiles/CorpsInfo");

            if (textAsset != null)
            {
                string jsonStr = textAsset.ToString();

                //Debug.Log(jsonStr);

                JsonData playerData = JsonMapper.ToObject(jsonStr);

                int n = playerData.Count;

                for(int i = 0; i<n; i++)
                {
                    JsonData tmp = playerData[i];
                    string tmpTagName = null;
                    int tmpTagNum = 0;
                    List<string> tmpStartResourceName = new List<string>();
                    List<int> tmpStartResourceNum = new List<int>();
                    List<string> tmpStartResourceProduct = new List<string>();
                    List<int> tmpStartResourceProductNum = new List<int>();
                    string tmpCorpEffectName = null;

                    if(tmp["Tag"]!=null)
                    {
                        tmpTagName = (string)tmp["Tag"]["TagName"];
                        tmpTagNum = (int)tmp["Tag"]["TagCount"];
                    }
                    if(tmp["StartResource"]!=null)
                    {
                        int num = tmp["StartResource"].Count;

                        for(int j = 0; j<num; j++)
                        {
                            tmpStartResourceName.Add((string)tmp["StartResource"][j]["ResourceName"]);
                            tmpStartResourceNum.Add((int)tmp["StartResource"][j]["ResourceAmount"]);
                        }
                    }

                    if (tmp["StartResourceProduct"] != null)
                    {
                        int num = tmp["StartResourceProduct"].Count;

                        for (int j = 0; j < num; j++)
                        {
                            tmpStartResourceProduct.Add((string)tmp["StartResourceProduct"][j]["ResourceName"]);
                            tmpStartResourceProductNum.Add((int)tmp["StartResourceProduct"][j]["ResourceAmount"]);
                        }
                    }

                    if(tmp["CorpEffectName"]!=null)
                    {
                        tmpCorpEffectName = (string)tmp["CorpEffectName"];
                    }

                    Corps.Add(new Corporation((string)tmp["ImageName"],
                        (string)tmp["CorpName"], tmpTagName, tmpTagNum,
                        tmpStartResourceName.ToArray(), tmpStartResourceNum.ToArray(),
                        tmpStartResourceProduct.ToArray(), tmpStartResourceProductNum.ToArray(),
                        tmpCorpEffectName));

                    Corps[i].CorpImage = Instantiate(CorpsImage, CorpTrans);

                    Corps[i].CorpImage.GetComponent<UnityEngine.UI.Image>().sprite
                        = Resources.Load("Corps/"
                        + Corps[i].ImageName, typeof(Sprite)) as Sprite;

                    Corps[i].CorpImage.transform.localPosition = Vector3.zero;
                    Corps[i].CorpImage.name = Corps[i].CorpName;
                }
            }

            else
                Debug.Log("파일이 없음");

            //마스터 클라이언트라면
            //기업 인덱스 리스트 생성 후
            //모든 플레이어에게 알려줌
            if (PhotonNetwork.isMasterClient)
                SelectCorps();
        }

        /// <summary>
        /// 기업 리스트에서
        /// 쓸 기업의 인덱스를 뽑아서
        /// 모든 플레이어에게 알림
        /// </summary>
        public void SelectCorps()
        {
            if (PhotonNetwork.isMasterClient)
            {
                int len = PhotonNetwork.playerList.Length;

                //기업 리스트에서 뽑아서 쓸 인덱스 리스트
                int[] corpsIndexes = new int[len * 2];

                for (int i = 0; i < len * 2; i++)
                    corpsIndexes[i] = -1;

                int front = 0;

                //인덱스 리스트에 중복되지 않는 난수를 채움
                while (front != corpsIndexes.Length)
                {
                    int num;
                    bool isInList = true;


                    while (isInList)
                    {
                        num = Random.Range(0, Corps.Count);
                        int i;

                        for (i = 0; i <= front; i++)
                        {
                            if (num == corpsIndexes[i])
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
                            corpsIndexes[front] = num;
                            front++;
                        }
                    }
                }

                string[] playerList = new string[len];

                for (int i = 0; i < len; i++)
                    playerList[i] = PhotonNetwork.playerList[i].NickName;

                GameManager.Instance.SendCorpIndList(PhotonTargets.All, corpsIndexes, playerList);
            }
        }
    }
}