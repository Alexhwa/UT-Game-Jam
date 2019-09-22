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

    //Sprite Flip
    private SpriteRenderer spriteRend;
    private Direction direction;

    //Gravity
    public float gravity;
    private static GameObject planet;
    private float normalDistFromPlanet;

    //Animation
    private Animator anim;

    //Jump checks
    public LayerMask ground;
    private bool grounded;
    private GameObject groundCheck;

    //Spear
    private GameObject spear;

    public enum Direction
    {
        left, right
    }
    // Start is called before the first frame update
    void Start()
    {
        spear = transform.GetChild(1).gameObject;
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        planet = GameObject.Find("Planet");
        rgdbdy2 = GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).gameObject;
        direction = Direction.right;
        normalDistFromPlanet = Vector2.Distance(transform.position, planet.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walking", true);
            rgdbdy2.AddForce(transform.right * xSpeed);
            if (rgdbdy2.velocity.magnitude >= maxSpeed)
            {
                var newVel = rgdbdy2.velocity;
                newVel /= 1.1f;
                rgdbdy2.velocity = newVel;
                
            }
            if(direction == Direction.left)
            {
                direction = Direction.right;
                spriteRend.flipX = !spriteRend.flipX;
                var newScale = spear.transform.localScale;
                newScale.x *= -1;
                if (!spear.GetComponent<SpearController>().getThrown())
                {
                    spear.transform.localScale = newScale;
                }
            }
        }
        else if(Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Walking", true);
            rgdbdy2.AddForce(transform.right * -xSpeed);
            if (rgdbdy2.velocity.magnitude >= maxSpeed)
            {
                var newVel = rgdbdy2.velocity;
                newVel /= 1.1f;
                rgdbdy2.velocity = newVel;
            }
            if (direction == Direction.right)
            {
                direction = Direction.left;
                spriteRend.flipX = !spriteRend.flipX;
                var newScale = spear.transform.localScale;
                newScale.x *= -1;
                if (!spear.GetComponent<SpearController>().getThrown())
                {
                    spear.transform.localScale = newScale;
                }
            }
        }
        else if(!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("Walking", false);
        }
        /*else if(rgdbdy2.velocity.magnitude >= maxSpeed)
        {
            var newVel = rgdbdy2.velocity;
            newVel /= 1.1f;
            rgdbdy2.velocity = newVel;
        }*/
        Vector3 dir = planet.transform.position - this.transform.position;
        transform.up = dir * -1;

        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, .1f, ground);
        anim.SetBool("Grounded", grounded);
        
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            anim.SetBool("Jumped", true);
            if (anim.GetBool("Walking"))
            {
                rgdbdy2.AddForce(transform.up * jumpForce * (1 + .2f * rgdbdy2.velocity.magnitude / maxSpeed), ForceMode2D.Impulse);
            }
            //assume player is on the ground
            else
            {
                rgdbdy2.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        //gravity
        rgdbdy2.AddForce(gravity * 
            (Vector2.Distance(transform.position, planet.transform.position) / normalDistFromPlanet) * dir);
        
    }
}
