using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Light Preset", menuName ="Scriptables/Light Preset", order = 1)]
// Preset for different light gradients and values. Used as a sciptable object
public class LightPreset : ScriptableObject
{
    public Gradient AmbientColour;
    public Gradient DirectionalColour;
    public Gradient FogColour;
}
