using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resposnible for storing the total pests found 

public class PestManager : MonoBehaviour
{
    public int pestsFound;

    void Start()
    {
        pestsFound = 0;
    }

    // Increases total pests found
    public void IncrementPest()
    {
        pestsFound += 1;
    }
}
