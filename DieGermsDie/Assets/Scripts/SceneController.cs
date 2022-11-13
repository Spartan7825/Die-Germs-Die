using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void startGameLevel() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel");
    }
    public void loadMainMenu() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void quitGame() 
    {
        Application.Quit();
    }

}
