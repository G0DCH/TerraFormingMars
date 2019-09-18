using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 startpos;//시작 위치

    //true면 도움말 창이 없음, false면 도움말 창이 있음
    public static bool IsScroll = true;

    [Tooltip("카메라 이동속도")]
    public float CameraSpeed = 10f;//카메라 이동속도

    [Tooltip("마우스 휠 줌 속도")]
    public float ZoomSpeed = 10f;//휠 줌 속도


    private void Start()
    {
        //0으로 초기화
        startpos = Vector3.zero;
        IsScroll = true;
    }

    private void Update()
    {
        if (IsScroll)
        {
            //스크롤 입력 받음
            float scroll = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;

            if (Camera.main.orthographicSize <= 3f && scroll > 0f)//줌 인 제한
                Camera.main.orthographicSize = 3f;
            else if (Camera.main.orthographicSize >= 15f && scroll < 0f)// 줌 아웃 제한
                Camera.main.orthographicSize = 15f;
            else
                Camera.main.orthographicSize -= scroll;
        }
    }

    private void FixedUpdate()
    {
        //만약 드래그 시작하지 않았다면
        if (startpos == Vector3.zero)
        {
            //마우스 우클릭을 했다면
            if (Input.GetMouseButtonDown(1))
            {
                startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//드래그 시작 위치를 저장
                return;
            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                //현재 마우스 위치 저장
                Vector3 nowpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 diff = nowpos - startpos;
                if (diff == Vector3.zero)
                    return;
                //이전 위치와 현재 위치 거리 계산
                diff = new Vector3(diff.x, diff.y, 0);
                //카메라 이동
                transform.Translate(-diff * Time.deltaTime * CameraSpeed);
                //드래그 시작 위치 갱신
                startpos = nowpos;
            }
            else
                startpos = Vector3.zero;
        }
    }
}
