using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Responsible for the Card menu system 
public class CardMenu : MonoBehaviour
{
    public bool inCard = false;
    public GameObject cardUI;
    public Camera camera;
    public GameObject lockedCanvas;
    public PauseMenu pMenu;
    public QuizManager quizManager;
    public GameObject controlCard;
    public FixedButton cardButton;

    void Update()
    {
        //Allows to check the remaining cards with tab, while not in certain menus
        if(pMenu.isPaused == false && quizManager.inQuiz == false)
        {
#if UNITY_ANDROID

            if (cardButton.Pressed)
            {
                lockedCanvas.SetActive(true);
            }
            else
            {
                lockedCanvas.SetActive(false);
            }
#endif
#if !UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                lockedCanvas.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                lockedCanvas.SetActive(false);
            }
#endif
        }
        //Fix behaviour while in menus
        if(inCard == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
            }
        }
    }
    //Resumes the game by enabling and disabling UI elements, and starting time again
    public void Resume()
    {
        controlCard.SetActive(false);
        cardUI.SetActive(false);
        Time.timeScale = 1f;
        inCard = false;
#if !UNITY_ANDROID
            Cursor.lockState = CursorLockMode.Locked;
#endif
        Cursor.visible = false;
        //Resets the camera if focused on the first NPC, after the dialogue check
        if (camera.GetComponent<MouseLook>().getFocusActive())
            camera.GetComponent<MouseLook>().toggleFocusActive();
    }
    //Pauses the game by enabling and disabling UI elements, and stopping time
    public void Pause(GameObject card)
    {
        cardUI = card;
        cardUI.SetActive(true);
        Time.timeScale = 0f;
        inCard = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
