using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private TransitionAnimController transAnimCont;

	public static readonly int mainMenuIndex = 1;
	public static readonly int firstLevelIndex = 2;
	public static readonly int lastLevelIndex = 4;

	private float startTransitionTime = 1f;
	private float endTransitionTime = 0.4f;
	private float currentTimeTilSceneLoad = 0f;
	private float currentTimeTilReadyToLoad = 0f;
	private int sceneToLoad;
	private bool beginLoad = false;
	private bool endLoad = false;

	// Start is called before the first frame update
	void Start()
    {
		transAnimCont = GameObject.Find("TransitionController").GetComponent<TransitionAnimController>();
	}

    // Update is called once per frame
    void Update()
    {
		if(beginLoad)
		{
			endLoad = false;
			if(currentTimeTilSceneLoad < startTransitionTime)
			{
				currentTimeTilSceneLoad += Time.deltaTime;
			}
			else
			{
				if(SceneManager.GetActiveScene().buildIndex != sceneToLoad)
				{
					SceneManager.LoadScene(sceneToLoad);
				}
				else if(SceneManager.GetActiveScene().buildIndex == sceneToLoad && SceneManager.GetActiveScene().isLoaded)
				{
					if(currentTimeTilSceneLoad < endTransitionTime)
					{
						currentTimeTilReadyToLoad += Time.deltaTime;
					}
					else
					{
						transAnimCont.EndTransition();
						currentTimeTilSceneLoad = 0f;
						currentTimeTilReadyToLoad = 0f;
						beginLoad = false;
						endLoad = true;
					}
				}
			}
		}
	}

	public void TriggerLoadScene(int _sceneToLoad)
	{
		sceneToLoad = _sceneToLoad;
		//Prevents the system from running the trigger load scene more than once at a time
		if(!beginLoad && SceneManager.GetActiveScene().buildIndex != 0)
		{
			beginLoad = true;
			transAnimCont.StartTransition();
		}
		else if(SceneManager.GetActiveScene().buildIndex == 0)
		{
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}
