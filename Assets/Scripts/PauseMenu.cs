using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages behaviour of the pause menu
public class PauseMenu : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject pMenuUI;
    public GameObject crosshair;
    public QuizManager quizManager;
    public GameObject controlMenu;

    // Pauses the game, and displays the controls at the start of the game
    void Start()
    {
        Pause();
        ControlMenu();
    }
    void Update()
    {
        //Allows pausing and resuming of the game while not in the quiz
        if(quizManager.inQuiz == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    // Resumes time, and sets appropriate game objects to active or inactive. Locks and stops displaying the cursor
    public void Resume()
    {
        pMenuUI.SetActive(false);
        controlMenu.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        #if !UNITY_ANDROID
            Cursor.lockState = CursorLockMode.Locked;
        #endif
        Cursor.visible = false;
    }

    // Pauses time, and sets appropriate game objects to active or inactive. Frees and displays the cursor
    public void Pause()
    {
        pMenuUI.SetActive(true);
        crosshair.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Loads the main menu by switched back scenes
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Displays the controls
    public void ControlMenu()
    {
        crosshair.SetActive(false);
        pMenuUI.SetActive(false);
        controlMenu.SetActive(true);
    }

    // Exits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
