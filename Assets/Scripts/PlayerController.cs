using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed;
    public float jumpForce;
    private Rigidbody2D rgdbdy2;

    private static float gravity = 6f;
    private static GameObject planet;

    //Jump checks
    public LayerMask ground;
    private bool grounded;
    private GameObject groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Planet");
        rgdbdy2 = GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            rgdbdy2.velocity = new Vector2 (xSpeed * Time.deltaTime, rgdbdy2.velocity.y);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
		{
			rgdbdy2.velocity = new Vector2(-xSpeed * Time.deltaTime, rgdbdy2.velocity.y);
		}
        Vector3 dir = new Vector3(planet.transform.position.x - transform.position.x, planet.transform.position.y - transform.position.y, 0f);
        transform.up = dir * -1;

        grounded = Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(0.05f, 0.05f), ground);
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //assume player is on the ground
            rgdbdy2.AddForce(transform.up * jumpForce);
        }
        else
        {
            rgdbdy2.AddForce(gravity * dir);
        }
    }

	public Vector2 getTransformUp()
	{
		return transform.up;
	}
}
