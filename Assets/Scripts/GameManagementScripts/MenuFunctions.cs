using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
	public void play()
	{
		SceneController sceneCont = GameObject.Find("GameController").GetComponent<SceneController>();

		if(SceneManager.GetActiveScene().buildIndex == SceneController.mainMenuIndex)
		{
			sceneCont.TriggerLoadScene(SceneController.firstLevelIndex);
		}
	}

	public void quit()
	{
		Application.Quit();
	}
}
