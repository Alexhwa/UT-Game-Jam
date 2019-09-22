using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : MonoBehaviour
{
    //Throw
    private bool thrown = false;
    public Rigidbody2D rgdbdg2D;
    public float spearSpeed;
    Plane plane;
    //Return
    public GameObject player;
    private Vector3 startPos;
    public float pullBackTime;
    private float counter;
    private bool keyPressedLastFrame;
    //Gravity
    public float gravity;
    private float normalDistFromPlanet;
    private GameObject planet;
    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Planet");
        normalDistFromPlanet = Vector2.Distance(transform.position, planet.transform.position);
        player = transform.parent.gameObject;
        startPos = transform.localPosition;
        counter = pullBackTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.NameToLayer("Ground") == collision.gameObject.layer && thrown)
        {
            becomeStatic();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.NameToLayer("Ground") == collision.gameObject.layer && thrown)
        {
            becomeStatic();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            counter = pullBackTime;
            keyPressedLastFrame = false;
        }
        if (thrown && Input.GetMouseButton(0))
        {
            keyPressedLastFrame = true;
            counter -= Time.deltaTime;
            var randomFloatX = Random.Range(-(1 - counter / pullBackTime) / 16, (1 - counter / pullBackTime) / 16);
            var randomFloatY = Random.Range(-(1 - counter / pullBackTime) / 16, (1 - counter / pullBackTime) / 16);
            transform.Translate(new Vector2(randomFloatX, randomFloatY));
            if (counter < 0)
            {
                returnToPlayer();
                thrown = false;
            }
        }
        else if (!thrown)
        {
            //Point spear at mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            plane = new Plane(Vector3.forward, new Vector3(Camera.main.transform.position.x, 
                                                            Camera.main.transform.position.y, 0));
            float enter = 0.0f;
            if (plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
            }
            var mousePos = ray.GetPoint(enter);
            var direction = mousePos - transform.position;
            transform.up = direction;
            
            if (Input.GetMouseButtonDown(0))
            {
                thrown = true;
                doThrow();
            }
        }
        rgdbdg2D.AddForce(gravity *
            (Vector2.Distance(transform.position, planet.transform.position) / normalDistFromPlanet) * planet.transform.position - this.transform.position);

    }
    public bool getThrown()
    {
        return thrown;
    }
    private void doThrow()
    {
        transform.parent = null;
        GetComponent<EdgeCollider2D>().isTrigger = false;
        rgdbdg2D.bodyType = RigidbodyType2D.Dynamic;
        rgdbdg2D.AddForce(transform.up * spearSpeed, ForceMode2D.Impulse);
    }
    private void becomeStatic()
    {
        rgdbdg2D.bodyType = RigidbodyType2D.Static;
        GetComponent<EdgeCollider2D>().isTrigger = false;
    }
    private void returnToPlayer()
    {
        thrown = false;
        transform.parent = player.transform;
        transform.localPosition = startPos;
        GetComponent<EdgeCollider2D>().isTrigger = true;
        rgdbdg2D.bodyType = RigidbodyType2D.Kinematic;
    }
}
