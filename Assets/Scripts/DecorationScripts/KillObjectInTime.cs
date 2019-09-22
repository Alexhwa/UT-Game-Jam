using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectInTime : MonoBehaviour
{
	public float timeToLive = 3f;
	private float currTime = 0f;

    // Update is called once per frame
    void Update()
    {
		currTime += Time.deltaTime;
		if(currTime >= timeToLive)
		{
			Destroy(gameObject);
		}
    }
}
