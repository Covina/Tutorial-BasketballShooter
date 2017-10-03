using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


    // Animators
    private Animator menuButtonAnimator;
    private Animator ballSelectAnimator;


    private void Awake()
    {
        // main menu buttons    
        menuButtonAnimator = GameObject.Find("Menu Holder").GetComponent<Animator>();

        // Get the basketball color selector
        ballSelectAnimator = GameObject.Find("Balls Holder").GetComponent<Animator>();

    }

    public void PlayGame()
    {
        // load the game scene
        SceneManager.LoadScene("Gameplay");
    }


    // player wants to select ball color
    public void LoadBallSelection()
    {
        // fade out the menu buttons
        menuButtonAnimator.Play("FadeOut");

        // bring in the ball selection
        ballSelectAnimator.Play("FadeIn");

    }

    // Player wants to go back to the main menu
    public void BackToMenu()
    {

        // fade out the ball selection
        ballSelectAnimator.Play("FadeOut");

        // fade in the menu buttons
        menuButtonAnimator.Play("FadeIn");


    }

}
