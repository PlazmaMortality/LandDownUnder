using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardMenu : MonoBehaviour
{

    public static bool inCard = false;
    public GameObject fox;

    public void Resume()
    {
        fox.SetActive(false);
        Time.timeScale = 1f;
        inCard = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        fox.SetActive(true);
        Time.timeScale = 0f;
        inCard = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
