using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textTitle;
    [SerializeField]
    TextMeshProUGUI textTapToPlay;

    [SerializeField]
    TextMeshProUGUI textCoinCount;
    int             coinCount = 0;

    public bool IsGameStart { private set; get; }

    void Awake()
    {
        IsGameStart = false;

        textTitle.enabled = true;
        textTapToPlay.enabled = true;
        textCoinCount.enabled = false;
    }
   

    public void GameStart()
    {
        // 마우스 왼쪽 버튼을 누리기 전까지 시작하지 않고 대기

            IsGameStart = true;

            textTitle.enabled = false;
            textTapToPlay.enabled = false;
            textCoinCount.enabled = true;
    }


    public void IncreaseCoinCount()
	{
        coinCount++;
        textCoinCount.text = coinCount.ToString();

      
    }

    public void GameOver()
	{
        // 현재 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
