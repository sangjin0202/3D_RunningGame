using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    GameObject  coinEffectPrefab;
    float       rotateSpeed;

    void Awake()
    {
        rotateSpeed = Random.Range(0, 360);
    }

    void Update()
    {
        // 코인 오브젝트 회전
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }

	void OnTriggerEnter(Collider other)
	{
        // 코인 오브젝트 획득 효과 (coinEffectPrefab) 생성
        GameObject clone = Instantiate(coinEffectPrefab);
        clone.transform.position = transform.position;

        // 코인 오브젝트 삭제
        Destroy(gameObject);
    }
}
