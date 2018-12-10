using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void instructionScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // need one more method here for HelpButton which will open a new scene with rules... from that menu there will be another play button and back button
}
