using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardMenu : MonoBehaviour
{

    public static bool inCard = false;
    public GameObject cardUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!inCard)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        cardUI.SetActive(false);
        Time.timeScale = 1f;
        inCard = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        cardUI.SetActive(true);
        Time.timeScale = 0f;
        inCard = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void setInCard(bool type)
    {
        inCard = type;
    }
}
