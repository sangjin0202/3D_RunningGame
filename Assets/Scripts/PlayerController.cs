using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	

	[SerializeField]
	float		dragDistance = 50.0f; // 드래그 거리
	Vector3		touchStart;			  //터치 시작위치
	Vector3		touchEnd;             //터치 종료 위치

	PlayerMove	playerMove;
	

	void Awake()
	{
		playerMove = GetComponent<PlayerMove>();
	}

	void Update()
    {
        if (Application.isMobilePlatform)
		{
			OnMobilePlatform();
		}
		else
		{
			OnPCPlatform();
		}
    }

	void OnMobilePlatform()
	{
		// 현재 화면을 터치하고 있지 않으면 메소드 종료
		if (Input.touchCount == 0) return;

		// 첫 번쨰 터치 정보 불러오기
		Touch touch = Input.GetTouch(0);

		// 터치 시작
		if (touch.phase == TouchPhase.Began)
		{
			touchStart = touch.position;
		}
		// 터치 & 드래그
		else if (touch.phase == TouchPhase.Moved)
		{
			touchEnd = touch.position;

			OnDragXY();
		}
	}

	void OnPCPlatform()
	{
		// 터치 시작
		if (Input.GetMouseButtonDown(0))
		{
			touchStart = Input.mousePosition;
		}
		// 터치 & 드래그
		else if (Input.GetMouseButton(0))
		{
			touchEnd = Input.mousePosition;

			OnDragXY();
		}
	}

	void OnDragXY()
	{
		// 터치 상태로 X축 드래그 범위가 dragDistance보다 클때
		if (Mathf.Abs(touchEnd.x - touchStart.x) >= dragDistance)
		{
			playerMove.MoveToX((int)Mathf.Sign(touchEnd.x - touchStart.x));
			return;
		}

		// 터치 상태로 y축 양의 방향으로 드래그 범위가 dragDistance보다 클떄
		if (touchEnd.y - touchStart.y >= dragDistance)
		{
			playerMove.MoveToY();
			return;
		}
	}

}
