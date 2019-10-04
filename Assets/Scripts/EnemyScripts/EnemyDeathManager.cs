using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeathManager : MonoBehaviour
{
	public int enemyCount;
	public GameObject deathParticles;
	
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if(SceneManager.GetActiveScene().buildIndex > SceneController.mainMenuIndex && SceneManager.GetActiveScene().buildIndex <= SceneController.lastLevelIndex)
		{
			FindNumOfEnemies();
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void FindNumOfEnemies()
	{
		enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
	}

	public void KillEnemy(Vector2 pos)
	{
		Instantiate(deathParticles, pos, Quaternion.identity);
		if(enemyCount > 0)
		{
			enemyCount--;
		}
	}
}
