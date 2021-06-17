using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for the storing dialogue of a single npc
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
