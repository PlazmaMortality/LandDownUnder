using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides interaction with the npc in case player does not initially talk to them
public class IntroEventColider : MonoBehaviour
{
    public GameObject lookAtTarget;
    public Camera camera;
    public DialogueManager dMenu;
    public NPC npc;
    public GameObject box;

    public bool hadChat;

    void Start()
    {
        hadChat = false;
    }

    //Called when player collides with a specified collider next to the first npc
    void OnTriggerEnter(Collider other)
    {
        if (!hadChat)
        {
            // Shifts the camera to look at the npc
            Vector3 man = lookAtTarget.transform.position;
            man.y += 2.5f;        //stop looking at feet

            //Triggers dialogue and deactivates the collider in game
            dMenu.Pause();
            npc.Trigger(npc.name);
            camera.GetComponent<MouseLook>().setFocusTarget(man);
            hadChat = true;
            box.SetActive(false);
        }
    }
}
