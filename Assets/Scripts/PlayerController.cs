using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed;
    public float jumpForce;
    private Rigidbody2D rgdbdy2;

    public float gravity;
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
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rgdbdy2.AddForce(transform.right * xSpeed);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            rgdbdy2.AddForce(transform.right * -xSpeed);
        }
        else if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            var newVel = rgdbdy2.velocity;
            newVel.x /= 1.1f;
            rgdbdy2.velocity = newVel;
        }
        Vector3 dir = planet.transform.position - this.transform.position;
        transform.up = dir * -1;

        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .1f, ground);
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //assume player is on the ground
            rgdbdy2.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        
        rgdbdy2.AddForce(gravity * dir);
        
    }
}
