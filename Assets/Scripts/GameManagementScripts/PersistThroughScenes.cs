using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistThroughScenes : MonoBehaviour
{
    void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
