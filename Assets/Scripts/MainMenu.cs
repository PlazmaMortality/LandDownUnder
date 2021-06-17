using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages behaviour of the main menu
public class MainMenu : MonoBehaviour
{
    // Loads the next scene, the game
    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Exits the game
    public void quitGame()
    {
        Application.Quit();
    }
}
