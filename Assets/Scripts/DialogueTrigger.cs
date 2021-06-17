using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for the trigger of dialogue
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(string name)
    {
        // Calls the dialogue manager, and passes the dialogue, and name of the npc
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, name);
    }
}
