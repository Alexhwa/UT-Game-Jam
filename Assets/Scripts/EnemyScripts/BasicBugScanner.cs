using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBugScanner : MonoBehaviour
{
	public BasicBugBehavior.Direction scanSide;
	
	private BasicBugBehavior bug;

	private bool _plrInView;
	public bool plrInView { get => _plrInView;}

	// Start is called before the first frame update
	void Start()
    {
		bug = transform.parent.parent.GetChild(0).GetComponent<BasicBugBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag.Equals("Player"))
		{
			_plrInView = true;
			bug.nextDir = scanSide;
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag.Equals("Player"))
		{
			_plrInView = false;
		}
	}
}
