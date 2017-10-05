using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private AudioSource audioSource;

    private int ballCount = 10;

    // assign sound volume level
    private float volume = 1.0f;

    [SerializeField]
    private AudioClip rim_hit1;

    [SerializeField]
    private AudioClip rim_hit2;

    [SerializeField]
    private AudioClip bounce1;

    [SerializeField]
    private AudioClip bounce2;

    [SerializeField]
    private AudioClip net_sound;

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

        // get reference to the audio source
        audioSource = GetComponent<AudioSource>();

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

    // add a ball on successful basket
    public void IncrementBalls(int increment)
    {
        ballCount += increment;

        // cap at 10
        if(ballCount > 10)
        {
            ballCount = 10;
        }

        // change display of balls to the amount
        GameObject.Find("BallsCountValue").GetComponent<Text>().text = ballCount.ToString();

    }


    // Remove ball on miss
    public void DecrementBalls(int increment)
    {
        ballCount -= increment;

        // Game over
        if (ballCount <= 0)
        {
            // game over stuff
        }

        // change display of balls to the amount
        GameObject.Find("BallsCountValue").GetComponent<Text>().text = ballCount.ToString();


    }


    // Manage all the audio sounds
    public void PlaySound(int id)
    {
        // 1 - net
        // 2 - rim 
        // 3 - bounce
        // 4 - bounce, half volume
        // 5 - rim, half volume


        switch (id)
        {
            case 1:
                audioSource.PlayOneShot(net_sound, volume);
                break;
            case 2:
                if(Random.Range(0,2) > 1)
                {
                    audioSource.PlayOneShot(rim_hit1, volume);
                }
                else
                {
                    audioSource.PlayOneShot(rim_hit2, volume);
                }
                break;
            case 3:
                if (Random.Range(0, 2) > 1)
                {
                    audioSource.PlayOneShot(bounce1, volume);
                } 
                else
                {
                    audioSource.PlayOneShot(bounce2, volume);
                }
                break;
            case 4:
                if (Random.Range(0, 2) > 1)
                {
                    audioSource.PlayOneShot(bounce1, volume / 2);
                }
                else
                {
                    audioSource.PlayOneShot(bounce2, volume / 2);
                }
                break;
            case 5:
                if (Random.Range(0, 2) > 1)
                {
                    audioSource.PlayOneShot(rim_hit1, volume / 2);
                }
                else
                {
                    audioSource.PlayOneShot(rim_hit2, volume / 2);
                }
                break;

        }


    }


}
