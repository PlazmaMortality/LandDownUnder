using UnityEngine;

[ExecuteAlways]

// Responsible for the day night cycle
public class LightManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    void Update()
    {
        // Checks lighting preset
        if (Preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            // Decreases the duration of night within the game
            if (TimeOfDay < 5 || TimeOfDay > 20)
            {
                TimeOfDay += (Time.deltaTime);
            }
            else
            {
                // Slows the day to increase the length
                TimeOfDay += (Time.deltaTime / 10); 
            }
            // Keeps time between 0 and 24 hours
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            //Allows for adjustment in the editor
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    void OnValidate()
    {
        // Validates the the light sources are obtained by the lighting manager
        if (DirectionalLight != null)
        {
            return;
        }
        
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            // Finds a light source within the game, if an error occurs with the RenderSettings sun
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void UpdateLighting(float timePercent)
    {
        // Sets the color within the scene from the preset gradients
        RenderSettings.ambientLight = Preset.AmbientColour.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColour.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColour.Evaluate(timePercent);
            // Rotates the directional light around the world along a 3d vector
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }
}
