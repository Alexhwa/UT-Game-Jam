using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBugBehavior : MonoBehaviour
{
	public enum Direction
	{
		LEFT, RIGHT
	}
	public enum State
	{
		IDLE, CHASING
	}

	private Rigidbody2D rb;

	public float movementSpeed = 10f;
	private Direction _currDir;
	private State _currState;

	public Direction currDir { get => _currDir; set => _currDir = value; }
	public State currState { get => _currState; set => _currState = value; }

	// Start is called before the first frame update
	void Start()
    {
		rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if(currState == State.CHASING)
		{
			move(currDir);
		}
    }

	private void move(Direction dir)
	{
		if(dir == Direction.RIGHT)
		{
			rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
		}
		else if(dir == Direction.LEFT)
		{
			rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(currState == State.IDLE && col.tag.Equals("Player"))
		{
			currState = State.CHASING;
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if(currState == State.CHASING && col.tag.Equals("Player"))
		{
			currState = State.IDLE;
		}
	}
}
