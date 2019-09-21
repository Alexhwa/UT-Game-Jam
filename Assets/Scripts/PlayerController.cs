using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Walk and Jump
    public float xSpeed;
    public float jumpForce;
    private Rigidbody2D rgdbdy2;
    public float maxSpeed;

    //Gravity
    public float gravity;
    private static GameObject planet;
    private float normalDistFromPlanet;

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

        normalDistFromPlanet = Vector2.Distance(transform.position, planet.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rgdbdy2.AddForce(transform.right * xSpeed);
            if (rgdbdy2.velocity.magnitude >= maxSpeed)
            {
                var newVel = rgdbdy2.velocity;
                newVel /= 1.1f;
                rgdbdy2.velocity = newVel;
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            rgdbdy2.AddForce(transform.right * -xSpeed);
            if (rgdbdy2.velocity.magnitude >= maxSpeed)
            {
                var newVel = rgdbdy2.velocity;
                newVel /= 1.1f;
                rgdbdy2.velocity = newVel;
            }
        }
        /*else if(rgdbdy2.velocity.magnitude >= maxSpeed)
        {
            var newVel = rgdbdy2.velocity;
            newVel /= 1.1f;
            rgdbdy2.velocity = newVel;
        }*/
        Vector3 dir = planet.transform.position - this.transform.position;
        transform.up = dir * -1;

        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .03f, ground);
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //assume player is on the ground
            rgdbdy2.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        
        rgdbdy2.AddForce(gravity * 
            (Vector2.Distance(transform.position, planet.transform.position) / normalDistFromPlanet) * dir);
        
    }
}
