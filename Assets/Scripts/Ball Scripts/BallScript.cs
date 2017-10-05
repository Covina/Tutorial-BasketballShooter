using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    private int touchedGround = 0;

    private bool touchedRim = false;

    // If our ball collides with the basketball hoop or the ground, play a sound
    void OnCollisionEnter2D (Collision2D target)
    {
        // ball hit the post
        if(target.gameObject.tag == "Backboard")
        {
            // classify as bank shot
            touchedRim = true;

            if (Random.Range(0,2) >= 1 )
            {
                GameManager.instance.PlaySound(3);
            } else
            {
                GameManager.instance.PlaySound(4);
            }

            
        }

        // ball hit the rim
        if(target.gameObject.tag == "Rim")
        {

            // classify as bank shot
            touchedRim = true;

            if (Random.Range(0, 2) >= 1)
            {
                GameManager.instance.PlaySound(2);
            }
            else
            {
                GameManager.instance.PlaySound(5);
            }

        }

        // ball hit the ground
        if(target.gameObject.tag == "Ground")
        {

            // dont play sound after 3 times
            touchedGround++;

            if (touchedGround < 3)
            {
                if (Random.Range(0, 2) >= 1)
                {
                    GameManager.instance.PlaySound(3);
                } else
                {
                    GameManager.instance.PlaySound(4);
                }
            }
        }


    }


    // When the ball travels through the net, play the sound.
    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Net") {

            GameManager.instance.PlaySound(1);

            // Bank or swish?
            if (touchedRim == true)
            {
                // bank
                GameManager.instance.IncrementBalls(1);
            } else
            {
                // swish
                GameManager.instance.IncrementBalls(2);
            }

        }
    
    }


}
