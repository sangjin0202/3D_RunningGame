using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
	[SerializeField]
	GameObject[]		areaPrefabs; // 구역 프리팹 배열
	[SerializeField]
	int					spawnAreaCountAtStart = 3; // 게임 시작 시 최초 생성되는 구역 개수
	[SerializeField]
	float				zDistance = 20; // 구역 사이의 거리 (z)
	int					areaIndex = 0; // 구역 인덱스 (배치되는 구역의 z 위치 연산에 사용)

	[SerializeField]
	Transform			playerTransform; //플레이어 transform

	void Awake()
	{
		//spawnAreaCountAtStart에 저장된 개수만큼 최초 구역 생성
		for (int i = 0; i < spawnAreaCountAtStart; i++)
		{
			// 첫 번쨰 구역은 항상 0번 구역 프리팹으로 설정
			if ( i == 0 )
			{
				SpwanArea(false);
			}
			else
			{
				SpwanArea();
			}	
		}
	}

	public void SpwanArea(bool isRandom = true)
	{
		GameObject clone = null;

		if (isRandom == false)
		{
			clone = Instantiate(areaPrefabs[0]);
		}
		else
		{
			int index = Random.Range(0, areaPrefabs.Length);
			clone = Instantiate(areaPrefabs[index]);
		}

		// 구역이 배치되는 위치 설정 (z축은 현재 구역 인덱스 * zDistence
		clone.transform.position = new Vector3(0, 0, areaIndex * zDistance);
		// 구역이 삭제될 때 새로운 구역을 생성할 수 있도록 this와 player의 transform 정보 전달
		clone.GetComponent<Area>().Setup(this, playerTransform);

		areaIndex++;
	}
}
