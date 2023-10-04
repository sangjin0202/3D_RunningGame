using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaerController : MonoBehaviour
{
    [SerializeField]
    Transform target; // 카메라가 추적하는 대상
    float zDistance;

	void Awake()
	{
		if ( target != null)
		{
			zDistance = target.position.z - transform.position.z;
		}
	}

	void LateUpdate()
	{
		// target이 존재하지 않으면 실행 하지 않는다
		if (target == null) return;

		// 카메라의 위치(Position) 정보 갱신
		Vector3 position	= transform.position;
		position.z			= target.position.z - zDistance;
		transform.position	= position;
	}
}
