using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour {

    public void PlayGame()
    {
        // load the main menu scene
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // quit the application
        Application.Quit();
    }
}
