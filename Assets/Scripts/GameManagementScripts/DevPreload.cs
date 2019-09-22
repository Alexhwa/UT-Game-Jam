using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreload : MonoBehaviour
{
	// Ensures the preload is ran, even if running test starting at a different scene index
	void Awake()
	{
		GameObject check = GameObject.Find("GameController");
		if(check == null)
		{ UnityEngine.SceneManagement.SceneManager.LoadScene("_preload"); }
	}
}
