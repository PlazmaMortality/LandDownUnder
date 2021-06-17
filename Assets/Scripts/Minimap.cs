using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changes the position of the minimap camera
public class Minimap : MonoBehaviour
{
    public Transform player;
    //Late Update in order to change after player movement
    void LateUpdate()
    {
        // Follows the player along the x - z axis while staying at a constant y value.
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
