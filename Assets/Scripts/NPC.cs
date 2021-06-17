using UnityEngine;

// Responsible for npc behaviour
public class NPC : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public string name;
    public IntroEventColider intro;

    // Allows trigger of appropriate dialogue
    public void Trigger(string name)
    {
        intro.hadChat = true;
        dialogueTrigger.TriggerDialogue(name);
    }
}
