using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // reference to the CreateBasketball script
    private CreateBasketball ballCreator;


    // for storing the selected ball, defaulting to ball zero
    private int ballIndex = 0;

    // Make the singleton	
	void Awake () {

        // Make the singleton
        MakeSingleton();

        // get reference to the script
        ballCreator = GetComponent<CreateBasketball>();

	}

    // Singleton function
    private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Fire this when the game object loads
    private void OnEnable()
    {
        // assign a delegate
        SceneManager.sceneLoaded += OnLevelLoaded;

    }

    // Fire this when the game object is unloaded (Never?)
    private void OnDisable()
    {
        // remove a delegate
        SceneManager.sceneLoaded -= OnLevelLoaded;

    }

    // Custom delegate function to replace OnLevelWasLoaded()
    public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        // if we are on the gameplay screen, create the basketball
        if(scene.name == "Gameplay")
        {
            SpawnBall();
        }

    }

    // Create the basketball game object
    public void SpawnBall()
    {
        // Call the create ball function
        ballCreator.CreateBall(ballIndex);
    }


    // Store which ball has been selected
    public void SelectBallColor(int index)
    {
        // get the ball selected
        this.ballIndex = index;
    }



}
