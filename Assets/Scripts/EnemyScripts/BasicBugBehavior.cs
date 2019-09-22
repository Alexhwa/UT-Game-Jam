using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

	private Animator anim;
	private Rigidbody2D rb;
	private static GameObject planet;
	private BasicBugScanner leftScanner;
	private BasicBugScanner rightScanner;

	public float gravity = 0.5f;
	public float movementSpeed = 10f;
	private Direction _currDir = Direction.RIGHT;
	private Direction _nextDir = Direction.LEFT;
	private State _currState = State.CHASING;
	private State _nextState = State.IDLE;

	public Direction currDir { get => _currDir;}
	public Direction nextDir { get => _nextDir; set => _nextDir = value; }
	public State currState { get => _currState; set => _currState = value; }
	public State nextState { get => _nextState; set => _nextState = value; }

	// Start is called before the first frame update
	void Start()
    {
		anim = gameObject.GetComponent<Animator>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		planet = GameObject.Find("Planet");
		leftScanner = transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<BasicBugScanner>();
		rightScanner = transform.parent.GetChild(1).GetChild(1).gameObject.GetComponent<BasicBugScanner>();
	}

	void Update()
	{
		if(currDir != nextDir)
		{
			transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y,
												transform.localScale.z);
			_currDir = nextDir;
		}
	}

	void FixedUpdate()
	{

		if(currState != nextState)
		{
			if(nextState == State.IDLE)
			{
				anim.SetTrigger("BasicBugIdle");
			}
			else
			{
				anim.SetTrigger("BasicBugRun");
			}
			currState = nextState;
		}

		if(currState == State.IDLE)
		{
			if(leftScanner.plrInView || rightScanner.plrInView)
			{
				nextState = State.CHASING;
			}
		}
		if(currState == State.CHASING)
		{
			move();

			if(!leftScanner.plrInView && !rightScanner.plrInView)
			{
				nextState = State.CHASING;
			}
		}

		Vector3 dir = new Vector3(planet.transform.position.x - transform.position.x, planet.transform.position.y - transform.position.y, 0f);
		transform.up = dir * -1;

		rb.AddForce(gravity * dir);
	}

	private void move()
	{
		if(currDir == Direction.LEFT)
		{
			rb.AddForce(transform.right * -movementSpeed);
		}
		else
		{
			rb.AddForce(transform.right * movementSpeed);
		}
	}

	public Vector3 getTransformUp()
	{
		return transform.up;
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
