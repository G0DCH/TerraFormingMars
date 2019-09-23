using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormingMars
{
    public class ActionManager : Singleton<ActionManager>
    {
        /// <summary>
        /// 행동이 성공적으로 끝났는가
        /// </summary>
        public bool IsActionSuccess;

        /// <summary>
        /// 행동 중인가
        /// </summary>
        public bool IsDoingAction = false;

        /// <summary>
        /// FuncName의 이름을 가진 코루틴을 실행함
        /// 버튼용
        /// </summary>
        /// <param name="FuncName">코루틴 이름</param>
        public void FuncAction(string FuncName)
        {
            if (PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex] 
                == PlayerManager.Instance.Me &&
                GameManager.Instance.LeftAction > 0)
            {
                IsActionSuccess = false;
                StopCoroutine(ActionResultUpdater());
                StopCoroutine(SelectTile());

                StartCoroutine(FuncName);

                StartCoroutine(ActionResultUpdater());
            }
        }

        /// <summary>
        /// FuncName의 이름을 가진 코루틴을 실행함
        /// </summary>
        /// <param name="FuncName">코루틴 이름</param>
        /// <param name="FuncArgs">코루틴 인자</param>
        public void FuncAction(string FuncName, params object[] FuncArgs)
        {
            if (PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex]
                == PlayerManager.Instance.Me &&
                GameManager.Instance.LeftAction > 0)
            {
                IsActionSuccess = false;
                StopCoroutine(ActionResultUpdater());
                StopCoroutine(SelectTile());

                StartCoroutine(FuncName, FuncArgs);

                StartCoroutine(ActionResultUpdater());
            }
        }

        /// <summary>
        /// CardManager에서
        /// FuncName의 이름을 가진 코루틴을 실행함
        /// </summary>
        /// <param name="FuncName">코루틴 이름</param>
        /// <param name="FuncArgs">코루틴 인자</param>
        public void CardFuncAction(string FuncName, params object[] FuncArgs)
        {
            if (PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex]
                == PlayerManager.Instance.Me &&
                GameManager.Instance.LeftAction > 0)
            {
                IsActionSuccess = false;
                StopCoroutine(ActionResultUpdater());
                StopCoroutine(SelectTile());

                CardManager.Instance.StartCoroutine(CardManager.Instance.StartCardFunc(FuncName, FuncArgs));

                StartCoroutine(ActionResultUpdater());
            }
        }

        IEnumerator ActionResultUpdater()
        {

            while(!IsActionSuccess)
                yield return new WaitForEndOfFrame();

            GameManager.Instance.LeftAction--;
            BoardManager.Instance.GameInfoUpdate();

            if (PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex]
                    == PlayerManager.Instance.Me)
                PlayerManager.Instance.SourceUpdater();

            yield break;
        }

        /// <summary>
        /// 타일 선택 함수
        /// </summary>
        /// <returns></returns>
        public IEnumerator SelectTile(params object[] args)
        {
            GameObject obj = null;
            string tileType = (string)args[0];
            bool IsForced = (bool)args[1];

            while(true)
            {
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    obj = null;
                    obj = GetClickedObject();

                    if (obj != null && obj.name.Contains("Tile"))
                    {
                        TileInfo tileInfo =  obj.GetComponent<TileInfo>();

                        yield return new WaitForEndOfFrame();

                        if(tileInfo.Owner!=string.Empty)
                        {
                            if (tileInfo.TileType != 0)
                            {
                                Debug.Log("다시 선택");
                                continue;
                            }
                        }

                        if (tileInfo.LineNum == 0)
                        {
                            Debug.Log("다시 선택");
                            continue;
                        }
                            

                        switch(tileType)
                        {
                            case "Water":
                                if (tileInfo.IsWater)
                                {
                                    tileInfo.SetTile(4);
                                    break;
                                }
                                Debug.Log("다시 선택");
                                continue;
                                    
                            case "Forest":
                                if (IsForced || !tileInfo.IsWater)
                                {
                                    tileInfo.SetTile(2);
                                    break;
                                }
                                Debug.Log("다시 선택");
                                continue;
                            case "City":
                                if (IsForced || (!tileInfo.IsWater && (tileInfo.FindTileCount(1) == 0)))
                                {
                                    tileInfo.SetTile(1);
                                    break;
                                }
                                Debug.Log("다시 선택");
                                continue;
                            case null:
                                if(tileInfo.Owner == string.Empty)
                                {
                                    tileInfo.SetTile(0);
                                    break;
                                }
                                Debug.Log("다시 선택");
                                continue;
                        }
                        break;
                    }
                }

                yield return new WaitForEndOfFrame();
            }

            IsDoingAction = false;

            yield break;
        }

        private GameObject GetClickedObject()//자신을 클릭했으면 자신을 return 아니면 null return
        {
            RaycastHit hit;
            GameObject target = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//마우스 포인터 근처 좌표 만듬

            if (true == (Physics.Raycast(ray.origin, ray.direction, out hit)))//마우스 근처에 오브젝트 있는지 확인
                target = hit.collider.gameObject;//있다면 오브젝트 저장

            return target;
        }

        #region 일반 프로젝트 함수

        /// <summary>
        /// 특허권 매각 프로젝트.
        /// 플레이어(player)와 팔 카드(mySellCards)들을 인자로 받아서
        /// player의 Hands에서 해당 카드를 제거하고
        /// player의 1장당 1MC를 증가
        /// </summary>
        /// <param name="args">
        /// 1번째는 player가 팔 카드 배열</param>
        /// <returns></returns>
        IEnumerator ProjectSellPatent(params object[] args)
        {
            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];
            CardInfo[] mySellCards = (CardInfo[])args[0];

            for (int i = 0; i < mySellCards.Length; i++)
            {
                player.Hands.Remove(mySellCards[i]);
                player.MC.Amount++;
            }
            IsActionSuccess = true;
            yield break;
        }

        /// <summary>
        /// 발전소 건설 프로젝트.
        /// player의 에너지 생산량을 1 증가시키고
        /// player의 MC를 11만큼 감소
        /// </summary>
        /// <returns></returns>
        IEnumerator ProjectPowerPlant()
        {
            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            if (player.MC.Amount < 11 || (player.Corporation.CorpName == "ThorGate" && player.MC.Amount < 8))
                yield break;

            player.Energy.Production++;

            player.MC.Amount -= 11;

            //토르게이트라면 비용 3 감소
            if (player.Corporation.CorpName == "ThorGate")
                player.MC.Amount += 3;

            IsActionSuccess = true;

            yield break;
        }

        /// <summary>
        /// 소행성 충돌 유도 프로젝트.
        /// 플레이어(player)를 인자로 받아서
        /// 현재 보드판의 온도가 8도 미만이라면
        /// 온도를 2도 올리고
        /// player의 MC를 14만큼 감소 시킨 뒤
        /// player의 테라포밍 등급을 1만큼 상승
        /// </summary>
        /// <returns></returns>
        IEnumerator ProjectAsteroid()
        {
            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            if (player.MC.Amount < 14)
                yield break;

            if (BoardManager.Instance.CelciusDegree < 8)
            {
                BoardManager.Instance.CelciusDegree += 2;
                player.MC.Amount -= 14;
                player.TerraFormingLevel++;
                IsActionSuccess = true;
            }
            yield break;
        }

        /// <summary>
        /// 지하수 추출 프로젝트
        /// 플레이어(player)를 인자로 받아서
        /// 물 타일의 개수가 9개 미만이라면
        /// 물 타일의 개수를 1 증가시키고
        /// 보드판에서 물 타일을 선택 한 뒤
        /// 해당 타일에 물 타일을 배치하고
        /// player의 MC를 18만큼 감소 시킨 뒤
        /// player의 테라포밍 등급을 1 상승
        /// </summary>
        /// <returns></returns>
        IEnumerator ProjectAquifer()
        {
            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            if (player.MC.Amount < 18)
                yield break;

            if (BoardManager.Instance.Water >= 9)
                yield break;
            else
            {
                StartCoroutine(SelectTile("Water", false ));
                player.MC.Amount -= 18;
                player.TerraFormingLevel++;
                IsActionSuccess = true;
                yield break;
            }
        }

        /// <summary>
        /// 녹지 형성 프로젝트.
        /// 프로젝트를 실행 시킨 플레이어(player)를 인자로 받아서
        /// 그 플레이어의 MC를 23만큼 감소시키고
        /// 숲 타일을 깔 수 있게 한다.
        /// </summary>
        /// <returns></returns>
        IEnumerator ProjectGreenery()
        {
            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            if (player.MC.Amount < 23)
                yield break;

            StartCoroutine(SelectTile("Forest", false));

            player.MC.Amount -= 23;
            IsActionSuccess = true;

            yield break;
        }

        /// <summary>
        /// 도시 건설 프로젝트.
        /// 프로젝트를 실행 시킨 플레이어(player)를 인자로 받아서
        /// 그 플레이어의 MC를 25만큼 감소시키고
        /// 도시 타일을 깔 수 있게 한다.
        /// 그리고 MC생산력을 1 증가시킨다.
        /// </summary>
        /// <returns></returns>
        IEnumerator ProjectCity()
        {
            PlayerInfo player = PlayerManager.Instance.players[GameManager.Instance.TurnPlayerIndex];

            if (player.MC.Amount < 25)
                yield break;

            StartCoroutine(SelectTile("City", false));

            player.MC.Amount -= 25;
            player.MC.Production++;
            IsActionSuccess = true;

            yield break;
        }
        #endregion
    }
}