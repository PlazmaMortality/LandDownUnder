using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for camera behaviours
public class HitScan : MonoBehaviour
{
    public float range = 2f;
    public float zoomedRange = 50f;
    public float zoomedFOV = 30f;
    private float normalFOV = 60f;

    public Camera mainCamera;
    public GameObject overlay;
    public GameObject crosshair;
    public CardMenu cMenu;
    public PauseMenu pMenu;

    public FixedButton buttonA;
    public FixedButton buttonB;
    public ParticleSystem cameraFlash;
    public DialogueManager dMenu;

    public Animator animator;
    private bool isZoomed = false;
    public GameObject camCamera;
    public bool aNotReleased;
    public bool bNotReleased;
    void Update()
    {
#if !UNITY_ANDROID
        //Checks the input cannot occur while in certain menus
        if(cMenu.inCard == false && pMenu.isPaused == false && dMenu.inDialogue == false)
        {
            //Takes a photo or interacts with an npc
            if (Input.GetButtonDown("Fire1"))
            {
                TakePhoto();
            }
            //Zooms in camera
            if (Input.GetButtonDown("Fire2"))
            {
                isZoomed = !isZoomed;
                //Sets appropriate animation for the camera
                animator.SetBool("Zoomed", isZoomed);
                if (isZoomed)
                {
                    StartCoroutine(ZoomedIn());
                }
                else
                {
                    ZoomedOut();
                }
            }
        }
#endif
#if UNITY_ANDROID
        if (cMenu.inCard == false && pMenu.isPaused == false && dMenu.inDialogue == false)
        {
            if (buttonA.Pressed && !aNotReleased)
            {
                aNotReleased = true;
                TakePhoto();
            }
            else if(!buttonA.Pressed && aNotReleased)
            {
                aNotReleased = false;
            }
            if (buttonB.Pressed && !bNotReleased)
            {
                bNotReleased = true;
                isZoomed = !isZoomed;
                //Sets appropriate animation for the camera
                animator.SetBool("Zoomed", isZoomed);
                if (isZoomed)
                {
                    StartCoroutine(ZoomedIn());
                }
                else
                {
                    ZoomedOut();
                }
            }
            else if (!buttonB.Pressed && bNotReleased)
            {
                bNotReleased = false;
            }
        }
#endif
    }
    void TakePhoto()
     {
        // Plays a flash and audio cue when a photo is taken
        cameraFlash.Play();
        FindObjectOfType<AudioManager>().Play("TakePhoto");

        // Raycast is sent from the camera to a GameObject in the world. A check is made to see whether the hit object contains either...
        // ... a pest or npc component, in which case the appropriate funciton is called

        // Two different ranges are used, which allows there to be a greater distance between the player and animal when a photo is taken while zoomed in
        RaycastHit hit;
        if(isZoomed)
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, zoomedRange))
            {
                Pest pest = hit.transform.gameObject.GetComponent<Pest>();
                NPC npc = hit.transform.gameObject.GetComponent<NPC>();

                CheckPest(pest);
                CheckNPC(npc);
            }
        }
        else
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
            {
                Pest pest = hit.transform.gameObject.GetComponent<Pest>();
                NPC npc = hit.transform.gameObject.GetComponent<NPC>();

                CheckPest(pest);
                CheckNPC(npc);
            }
        }

        //Player automatically zooms out when a photo is taken
        isZoomed = false;
        animator.SetBool("Zoomed", isZoomed);
        ZoomedOut();
    }

    // Waits for a small duration in order for the animtion to finish before overlays and gameobjects are set to active or inactive.
    // Also changes the field of view to be smaller
    IEnumerator ZoomedIn()
    {
        yield return new WaitForSeconds(0.15f);

        overlay.SetActive(true);
        camCamera.SetActive(false);
        crosshair.SetActive(false);

        mainCamera.fieldOfView = zoomedFOV;
    }

    // Sets elements such as the overlay to active and reverts the field of view to the original size
    void ZoomedOut()
    {
        overlay.SetActive(false);
        crosshair.SetActive(true);
        camCamera.SetActive(true);
        mainCamera.fieldOfView = normalFOV;
    }

    // Checks whether a hit object contains a pest component
    void CheckPest(Pest pest)
    {
        if (pest != null)
        {
            overlay.SetActive(false);
            crosshair.SetActive(true);
            //Displays appropriate card
            cMenu.Pause(pest.card);
            // Unlocks locked pest card in tab menu
            pest.lockedCard.GetComponent<UnlockCard>().unlock();
                
            if(pest.isFound == false)
            {
                // Increments the pests found once for that animal
                FindObjectOfType<PestManager>().IncrementPest();
            }
            //Plays audio and particle effect cue when found
            pest.PlayFound();
        }
    }

    // Checks whether a hit object contains an npc component
    void CheckNPC(NPC npc)
    {
        if (npc != null)
        {
            overlay.SetActive(false);
            dMenu.Pause();
            //Triggers npc dialogue
            npc.Trigger(npc.name);
        }
    }
}
