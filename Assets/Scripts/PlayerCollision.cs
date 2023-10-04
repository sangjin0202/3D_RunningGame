using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
	public AudioClip audioItem;
	public AudioClip audioDamaged;
	public GameController gameController;
	public Animator anim;
	public Collider collider;
	public Image[] lifeImage;
	public int hp = 3;

	AudioSource audioSource;

	private void Awake()
	{
		anim.GetComponent<Animator>();
		collider.GetComponent<Collider>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if ( other.tag.Equals("Obstacle"))
		{
			hp--;
			audioSource.clip = audioDamaged;
			audioSource.Play();
			LifeIcon(hp);
			anim.SetTrigger("Down");
			Invoke("StopTime", 5f);
			if (hp == 0)
			{
				gameController.GameOver();
			}
		}
		else if (other.tag.Equals("Coin"))
		{
			gameController.IncreaseCoinCount();
			audioSource.clip = audioItem;
			audioSource.Play();
		}
	}

	void StopTime()
	{
		collider.isTrigger = false;
	}
	void LifeIcon(int life)
	{
		for ( int index = 0; index < 3; index ++ )
		{
			lifeImage[index].color = new Color(255, 0, 0, 0);
		}

		for (int index = 0; index < life; index++)
		{
			lifeImage[index].color = new Color(255, 0, 0, 1);
		}
	}

	private void Update()
	{
		if (collider.isTrigger == true)
			return;

		collider.isTrigger = true;
	}

}

