using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores pest information and behaviour

public class Pest : MonoBehaviour
{
    public GameObject card;
    public GameObject lockedCard;
    public string name;
    public bool isFound;
    public ParticleSystem foundParticles;

    // Bool to check if the pest has been found before. in order to not increment total pests found again with the same pest.
    void Start()
    {
        isFound = false;
    }

    // Plays the particle effects and audio cue when a pest is found
    public void PlayFound()
    {
        isFound = true;
        foundParticles.Play();
        FindObjectOfType<AudioManager>().Play("Achievement");
    }
}
