using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FlyingBugBehavior : MonoBehaviour
{
	public enum Direction
	{
		LEFT, RIGHT
	}

	private SceneController sceneCont;

	private EnemyDeathManager deathManager;

	public float MAX_TRAVEL_TIME = 3f;
	private float currentTravelTime = 0f;

	private float MIN_DIST_FROM_GROUND;
	private float currDistFromGround;
	public float flyUpForce = 5f;

	private Rigidbody2D rb;
	private GameObject planet;
	private CircleCollider2D planetCol;

	public float gravity = 0.5f;
	public float movementSpeed = 10f;
	private Direction _currDir = Direction.RIGHT;
	private Direction _nextDir = Direction.LEFT;

	public Direction currDir { get => _currDir; }

	// Start is called before the first frame update
	void Start()
	{
		sceneCont = GameObject.Find("GameController").GetComponent<SceneController>();
		deathManager = GameObject.Find("GameController").GetComponent<EnemyDeathManager>();

		rb = gameObject.GetComponent<Rigidbody2D>();
		planet = GameObject.Find("Planet");
		planetCol = planet.GetComponent<CircleCollider2D>();

		MIN_DIST_FROM_GROUND = calcDistFromGround();
	}

	void Update()
	{
		currentTravelTime += Time.deltaTime;

		if(currentTravelTime >= MAX_TRAVEL_TIME)
		{
			transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y,
												transform.localScale.z);
			if(_currDir == Direction.LEFT)
			{
				_currDir = Direction.RIGHT;
			}
			else
			{
				_currDir = Direction.LEFT;
			}

			rb.velocity = new Vector2(0f, 0f);
			currentTravelTime = 0f;
		}

		move();
	}

	void FixedUpdate()
	{
		Vector3 dir = new Vector3(planet.transform.position.x - transform.position.x, planet.transform.position.y - transform.position.y, 0f);
		transform.up = dir * -1;

		currDistFromGround = calcDistFromGround();

		if(currDistFromGround > MIN_DIST_FROM_GROUND)
		{
			rb.AddForce(gravity * dir);
		}
		else
		{
			rb.AddForce(-flyUpForce * dir);
		}
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
			sceneCont.RestartLevel();
		}

		if(col.gameObject.tag.Equals("Spear"))
		{

			deathManager.KillEnemy(transform.position);
			Destroy(gameObject);
		}
	}

	private float calcDistFromGround()
	{
		return (Vector2.Distance(transform.position, planet.transform.position)) -
			(planetCol.radius * planet.transform.localScale.x);
	}
}
