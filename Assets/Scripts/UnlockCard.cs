using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Responsible for behaviour of remaining card menu

public class UnlockCard : MonoBehaviour
{
    public bool locked;
    Image image;
    public Sprite lockedImage;
    public Sprite UnlockedImage;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        getCorrectImage();
    }

    //Changes the display to locked or unlocked for apest card, depending on if it has been identified
    public void getCorrectImage()
    {
        if (locked)
            image.sprite = lockedImage;
        else
            image.sprite = UnlockedImage;
    }

    public void unlock()
    {
        locked = false;
    }

}
