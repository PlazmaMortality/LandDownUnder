using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Manages the dialogue system within the game
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public bool inDialogue;
    public GameObject dialogueBox;
    public Camera camera;
    public PauseMenu pMenu;
    public GameObject sureBox;
    public GameObject quizBox;

    public QuizManager quizManager;

    string npcName;

    private Queue<string> sentences;

    void Start()
    {
        inDialogue = false;
        sentences = new Queue<string>();
    }

    //Allows to exit the dialogue with escape, without menu bugs. And resets the name of the npc
    void Update()
    {
        if (pMenu.isPaused == false && quizManager.inQuiz == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                npcName = "";
                EndDialogue();
            }
        }
    }

    //Starts the display of sentences whilein dialogue
    public void StartDialogue(Dialogue dialogue, string name)
    {
        npcName = name;
        inDialogue = true;
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //Displays next sentence after continue is pressed
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Small animation of letters appearing in the dialogue, until the entire sentence is on screen
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    //Exits dialogue, however presents an are you sure menu, when the last npc is spoken to.
    public void EndDialogue()
    {
        if(npcName == "ExitBroden")
        {
            dialogueBox.SetActive(false);
            sureBox.SetActive(true);
        }
        else
        {
            Resume();
        }
    }

    // Resumes time, and sets the appropriate gameobjects to active or inactive
    public void Resume()
    {
        inDialogue = false;
        npcName = "";
        dialogueBox.SetActive(false);
        sureBox.SetActive(false);

        Time.timeScale = 1f;
        #if !UNITY_ANDROID
        Cursor.lockState = CursorLockMode.Locked;
        #endif
        Cursor.visible = false;
        //Resets the camera if focused on the first NPC, after the dialogue check
        if (camera.GetComponent<MouseLook>().getFocusActive())
        {
            camera.GetComponent<MouseLook>().toggleFocusActive();
        }
    }

    //Pauses time, and displays the cursor
    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
