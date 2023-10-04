using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameController gameController;  // IsGameStart가 true일때 움직이기 위해
    public AudioClip audioJump;


    // X축이동
    float   moveXWidth  = 1.5f;     // 1회 이동 시 이동거리 (x축)
    float   moveTimeX   = 0.1f;     // 1회 이동에 소요되는 시간(y축)
    bool    isXMove     = false;    // true : 이동중, false : 이동 가능
    // Y축이동
    float   originY     = 0.55f;    // 점프 및 착지하는 y축 값
    float   gravity     = -9.81f;   // 중력
    float   moveTimeY   = 0.3f;     // 1회 이동에 소요되는 시간 (y축)
    bool    isJump      = false;    // true : 점프중, false : 점프가능
    // z축 이동
    [SerializeField]
    float   moveSpeed   = 20.0f;    // 이동속도 (z축)

    float   limity = -1.0f;         //플레이어가 사망하는 y위치;

    Rigidbody rigidboody;
    Animator anim;
    AudioSource audioSource;


    void Awake()
	{
        rigidboody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
	{
		

		Invoke("PlayerStop",2.5f);

        // 현재 상태가 게임 시작이 아니면 UPdate()를 실행하지 않는다.
        if (gameController.IsGameStart == false) return;

        // z축 이동
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        if (gameController.IsGameStart == true)
        {
            anim.SetFloat("Run", 1);
        }

        // 떨어지면 플레이어 사망
        if ( transform.position.y < limity)
		{
            Debug.Log("게임오버");
		}
	}

    public void MoveToX(int x)
	{
        // 현재 x축 이동 중으로 이동 불가능
        if (isXMove == true) return;

        if (x > 0 && transform.position.x < moveXWidth)
		{
            StartCoroutine(OnMoveToX(x));
		}
		else if (x < 0 && transform.position.x > -moveXWidth)
		{
            StartCoroutine(OnMoveToX(x));
        }
	}

	public void MoveToY()
	{
        // 현재 점프 중으로 점프 불가능
        if (isJump == true) return;

        StartCoroutine(OnMoveToY());
	}

    IEnumerator OnMoveToX(int direction)
	{
        float currnet   = 0;
        float percent   = 0;
        float start     = transform.position.x;
        float end       = transform.position.x + direction * moveXWidth;

        isXMove = true;

		while (percent < 1)
		{
            currnet += Time.deltaTime;
            percent = currnet / moveTimeX;

            float x = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
		}

        isXMove = false;
	}

    IEnumerator OnMoveToY()
	{
        float current = 0;
        float percent = 0;
        float v0      = -gravity; // y 방향의 초기 속도

        isJump = true;
        anim.SetTrigger("Jump");
        audioSource.clip = audioJump;
        audioSource.Play();
        rigidboody.useGravity = false;

		while (percent < 1 )
		{
            current += Time.deltaTime;
            percent = current / moveTimeY;

            // 시간 경과에 따라 오브젝트의 y위치를 바꿔준다
            // 포물선 운동 : 시작위치 + 초기속도 * 시간 + 중력 * 시간제곱
            float y = originY + (v0 * percent) + (gravity * percent * percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield return null;
		}

        isJump = false;
        rigidboody.useGravity = true;
	}

    void PlayerStop()
	{
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Down"))
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 10;
        }
    }

}
