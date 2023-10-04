using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    float       destoryDistance = 15;
    AreaSpawner areaSpawner;
    Transform   playerTransform;

	public void Setup(AreaSpawner areaSpawner, Transform playertransform)
	{
        this.areaSpawner        = areaSpawner;
        this.playerTransform    = playertransform;
	}
    void Update()
    {
		if (playerTransform.position.z - transform.position.z >= destoryDistance)
		{
            //새로운 구역을 생성
            areaSpawner.SpwanArea();
            // 현재 구역은 삭제
            Destroy(gameObject);
		}
    }
}
