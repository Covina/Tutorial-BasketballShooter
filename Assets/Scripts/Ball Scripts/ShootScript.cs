using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootScript : MonoBehaviour {

    public int dots = 30;

    // Ball movement
    public float power = 2.0f;

    // Alpha channel to fade out the ball
    public float life = 1.0f;

    // cancel player turn if too much drag
    public float dead_sense = 25f;

    [SerializeField]
    private GameObject dotObj;


    private Vector2 startPosition;

    private bool isShooting = false;
    private bool isAiming = false;
    private bool hit_ground = false;

    private GameObject Dots;

    private List<GameObject> projectilesPath;


    private Rigidbody2D myBody;

    private Collider2D myCollider;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<Collider2D>();
    }

	// Use this for initialization
	void Start () {

        Dots = GameObject.Find("DotsContainer");

        // disable physics
        myBody.isKinematic = true;

        myCollider.enabled = false;

        startPosition = transform.position;

        // Take all the transforms of the child objects of Dots, Add them to a List, and Convert them all to gameobjects
        // alternatively could do additional code and store the objects in an array
        projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);

        Debug.Log("projectilesPath.Count: [" + projectilesPath.Count + "]");

        // Disable all the dots for now
        for(int i = 0; i< projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = false;

        }

	}
	
	// Update is called once per frame
	void Update () {

        Aim();

        if (hit_ground == true)
        {
            // Subtract time to control how long it takes to fade away
            life -= Time.deltaTime;

            // get current color value (alpha included)
            Color c = GetComponent<Renderer>().material.GetColor("_Color");

            // adjust the alpha channel
            GetComponent<Renderer>().material.SetColor("_Color", new Color (c.r, c.g, c.b, life));

            if(life <= 0)
            {
                // reset hit ground
                hit_ground = false;

                // Spawn a new ball
                GameManager.instance.SpawnBall();

                // destroy the basketball
                Destroy(gameObject);

            }

        }

	}

    // Aim the trajectory path of the ball
    private void Aim()
    {
        // If we are actively shooting, exit the function
        if(isShooting) {
            return;
        }

        // Are we pressing with left mouse button?
        if (Input.GetAxis("Fire1") == 1) {

            // first click and we are not yet aiming?
            if (isAiming == false)
            {
                // we are now aiming
                isAiming = true;

                // coords of first click
                startPosition = Input.mousePosition;

                // Calculate trajectory path
                CalculatePath();

                // Display the trajectory
                ShowPath();

            } else
            {
                // if we're already aiming and holding down the button
                CalculatePath();

            }
        }  else if (isAiming && !isShooting) {

            // We're just aiming right now

            // Check if we dragged too far
            if(InDeadZone(Input.mousePosition) || InReleaseZone(Input.mousePosition) ) {

                // Break our of the Aim() function
                isAiming = false;
                HidePath();
                return;
            }

            // Prep ball for shooting
            myBody.isKinematic = false;     // Prepare ball for physics
            myCollider.enabled = true;      // enable collider to bounce off rim
            isShooting = true;              // put us in the shooting state
            isAiming = false;               // put us in the aiming state

            // Shoot the ball
            myBody.AddForce(GetForce(Input.mousePosition));

            // hide the dots
            HidePath();

            // Now that we have shot, decrement the balls remaining
            GameManager.instance.DecrementBalls(1);

        }


    }

    /// <summary>
    /// Take in current mouse position and calculate distance as a v2, multiplied by power
    /// </summary>
    /// <param name="mouse"></param>
    /// <returns></returns>
    private Vector2 GetForce(Vector3 mouse)
    {
        // Compare starting mouse click with current input location
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(mouse.x, mouse.y)) * power; 
    }


    private void CalculatePath()
    {
        // Calculate the velocity
        Vector2 velocity = GetForce(Input.mousePosition) * Time.fixedDeltaTime / myBody.mass;

        // Loop through the projectile dots and enable them
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            // Turn display on
            projectilesPath[i].GetComponent<Renderer>().enabled = true;

            // t is for Time
            float t = i / 30f;

            // caluclate the position of the dot point
            Vector3 point = PathPoint(transform.position, velocity, t);
            point.z = 1.0f; // force set the z coord

            // assign the location of the dot
            projectilesPath[i].transform.position = point;

        }


    }

    // Calculate the Dot position based on velocity and time
    private Vector2 PathPoint (Vector2 startPos, Vector2 startVelocity, float t)
    {

        // physics distance formula:  (v*t) + 1/2 * acceleration * time squared
        return startPos + (startVelocity * t) + (0.5f * Physics2D.gravity * t * t);
        
    }


    private void HidePath ()
    {
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            // Turn display off
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }


    private void ShowPath()
    {
        for(int i = 0; i < projectilesPath.Count; i++) { 
            // Turn display on
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
        }


    }

    // check if we've dragged too far
    private bool InDeadZone (Vector2 mouse)
    {
        if (Mathf.Abs(startPosition.x - mouse.x) <= dead_sense && Mathf.Abs(startPosition.y - mouse.y) <= dead_sense)
        {
            // if we've gone farther than the dead_sense allows
            return true;
        } else
        {
            return false;
        }

    }


    // are we in the OK zone?
    private bool InReleaseZone(Vector2 mouse)
    {

        if(mouse.x <= 70)
        {
            return true;
        } else {
            return false;
        }

    }

    // check if we hit the ground
    private void OnCollisionEnter2D(Collision2D target)
    {
        // check what we hit.
        if(target.gameObject.tag == "Ground")
        {

            // Set the hit ground state to true;
            hit_ground = true;

        }


    }



}
