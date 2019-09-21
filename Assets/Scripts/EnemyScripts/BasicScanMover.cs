using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicScanMover : MonoBehaviour
{
	private Transform bugTrans;
	private BasicBugBehavior bug;

    // Start is called before the first frame update
    void Start()
    {
		bugTrans = transform.parent.GetChild(0).GetComponent<Transform>();
		bug = transform.parent.GetChild(0).GetComponent<BasicBugBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = bugTrans.position;
		transform.up = bug.getTransformUp();
    }
}
