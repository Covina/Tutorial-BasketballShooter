using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBasketball : MonoBehaviour {


    [SerializeField]
    private GameObject ball;

    // All of the sprites for the different color options
    [SerializeField]
    private Sprite[] ballImages;

    // Create the parameters
    private float minX = -4.7f;
    private float maxX = 8.0f;
    private float minY = -2.5f;
    private float maxY = 1.5f;


    public void CreateBall(int index)
    {
        // Spawn the game ball
        GameObject gameBall = Instantiate(ball, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity) as GameObject;

        gameBall.GetComponent<SpriteRenderer>().sprite = ballImages[index];
    }

} // CreateBasketball
