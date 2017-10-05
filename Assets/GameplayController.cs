using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    // back button
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
