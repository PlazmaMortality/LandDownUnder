using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for the behaviour of looking in the game world
public class MouseLook : MonoBehaviour
{
    public float mSensitivity = 100f;
    public Transform pBody;

    float xRotation = 0f;
    bool focusActive;
    public Vector3 focusTarget;
    public FixedTouchField touchField;
    void Start()
    {
        #if !UNITY_ANDROID
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        #endif
        focusActive = false;

    }

    void Update()
    {
        if (focusActive)
            // Used when focused on the first npc, with the intro collider
            transform.LookAt(focusTarget);
        else
        {
            #if !UNITY_ANDROID
                //X and Y rotation clamped to 90 degrees, allowing rotation of player look.
                // Can be increased or decreased with mouse sensitivity
                float mouseX = Input.GetAxis("Mouse X") * mSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mSensitivity * Time.deltaTime;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                pBody.Rotate(Vector3.up * mouseX);
            #endif
            #if UNITY_ANDROID
                float mouseX = touchField.TouchDist.x * mSensitivity * Time.deltaTime;
                float mouseY = touchField.TouchDist.y * mSensitivity * Time.deltaTime;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                pBody.Rotate(Vector3.up * mouseX);
            #endif
        }
    }
    // Setter for focus active
    public void setFocusTarget(Vector3 target)
    {
        focusTarget = target;
        focusActive = true;
    }

    //Changes value of focus active after use
    public void toggleFocusActive()
    {
        if (focusActive)
            focusActive = false;
        else
            focusActive = true;
    }

    //Getter for focus active 
    public bool getFocusActive()
    {
        return focusActive;
    }
}
