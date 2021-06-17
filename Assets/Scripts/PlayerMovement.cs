using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages player input for movement and behaviours

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float pSpeed = 10f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 2f;

    public Transform floorCheck;
    public float floorDistance = 1f;
    public LayerMask floorMask;
    bool onFloor;

    public AudioSource audioSource;
    bool isMoving;

    public FixedJoystick joystick;

    Vector3 velocity;

    void Update()
    {
       //Checks that the player is colliding within the ground
        onFloor = Physics.CheckSphere(floorCheck.position, floorDistance, floorMask);

        // Keeps player on the ground, in case of clipping
        if(onFloor && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        #if !UNITY_ANDROID
            // Collects input from WASD 
            float xDirection = Input.GetAxis("Horizontal");
            float zDirection = Input.GetAxis("Vertical");
            
            // Stops footsteps audio from playing if an interaction occurs while walking
            isMoving = ((xDirection * Time.deltaTime) != 0 || (zDirection * Time.deltaTime) != 0);

            // Moves player influenced by movement speed and delta time
            Vector3 movement = transform.right * xDirection + transform.forward * zDirection;
            controller.Move(movement * pSpeed * Time.deltaTime);
        #endif
        #if UNITY_ANDROID
            Vector3 movement = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
            controller.Move(movement * pSpeed * Time.deltaTime);
        #endif
        // Player can jump while on the ground
        if (Input.GetButtonDown("Jump") && onFloor)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Plays footsteps audio when walking
        if (isMoving && onFloor)
        {
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();

        // Gravity influences y velocity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
