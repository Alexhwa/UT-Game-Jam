using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xSpeed;
    public float jumpForce;
    private Rigidbody2D rgdbdy2;

    private static float gravity = 0.5f;
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
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rgdbdy2.AddForce(transform.right * xSpeed);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rgdbdy2.AddForce(transform.right * -xSpeed);
        }
        Vector3 dir = planet.transform.position - this.transform.position;
        transform.up = dir * -1;

        grounded = Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(0.5f, 0.05f), ground);
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
}
