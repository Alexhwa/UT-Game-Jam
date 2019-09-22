using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZoneController : MonoBehaviour
{
	private EnemyDeathManager deathManager;
	private SceneController sceneCont;

	private AudioSource winZoneAudio;
	public AudioClip winSound;
	public AudioClip deniedSound;

	// Start is called before the first frame update
	void Start()
	{
		winZoneAudio = gameObject.GetComponent<AudioSource>();

		deathManager = GameObject.Find("GameController").GetComponent<EnemyDeathManager>();
		sceneCont = GameObject.Find("GameController").GetComponent<SceneController>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Contains("Player"))
		{
			if(deathManager.enemyCount <= 0)
			{
				winZoneAudio.PlayOneShot(winSound);
				sceneCont.TriggerLoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
			else
			{
				winZoneAudio.PlayOneShot(deniedSound);
			}
		}
	}
}
