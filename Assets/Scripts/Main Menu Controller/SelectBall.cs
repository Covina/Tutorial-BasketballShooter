using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBall : MonoBehaviour {

    // Create list to hold all of our buttons
    private List<Button> ballButtons = new List<Button>();


    private void Awake()
    {
        // Add the onclick behavior for selecting balls
        GetButtonsAndAddListeners();
    }

    // Find all the ball button options and add select functionality
    void GetButtonsAndAddListeners()
    {
        // Find all available ball buttons
        GameObject[] btns = GameObject.FindGameObjectsWithTag("MenuBall");

        // Loop through the found buttons
        for(int i = 0; i < btns.Length; i++)
        {
            // add the button components to the list
            ballButtons.Add( btns[i].GetComponent<Button>() );

            // add an OnClick listener function to each button we just added to the list
            ballButtons[i].onClick.AddListener( () => SelectABall() );

        }

    }

    // Equip the ball 
    public void SelectABall()
    {
        // Get the ball int from its game object name when it's selected by the player
        int ballIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        //Debug.Log("The selected ball is [" + ballIndex + "]");

        // Inform the GameManager which ball the player equipped

    }

}
