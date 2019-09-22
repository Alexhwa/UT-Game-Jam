using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticlesOnFinish : MonoBehaviour
{
	private ParticleSystem partic;
    // Start is called before the first frame update
    void Start()
    {
		partic = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!partic.isEmitting)
		{
			Destroy(gameObject);
		}
    }
}
