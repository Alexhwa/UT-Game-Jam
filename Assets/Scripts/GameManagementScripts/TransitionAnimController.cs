using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimController : MonoBehaviour
{
	Animator anim;

	// Start is called before the first frame update
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
	}

	public void StartTransition()
	{
		anim.SetTrigger("Transition-Start");
	}
	public void EndTransition()
	{
		anim.SetTrigger("Transition-End");
	}
}