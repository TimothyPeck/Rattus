using UnityEngine;
using System.Collections;

public class cabinet_light_flasher : MonoBehaviour
{

    public float totalSeconds;     // The total of seconds the flash wil last
    public float maxIntensity;     // The maximum intensity the flash will reach
    public Light myLight;        // Your light
    private int cpt=1;
    private bool increase = true;
    private float flashTime;

    void Start()
    {
        flashTime = totalSeconds * 60;
    }

    /// <summary>
    /// Turns the light on and off for the given number of seconds
    /// </summary>
    void Update()
    {
        if (increase)
        {
            cpt++;
            increase = cpt < flashTime ? true : false;
            myLight.intensity = (maxIntensity * cpt) / flashTime;
        }
        else
        {
            cpt--;
            increase = cpt <= 0 ? true : false;
            myLight.intensity = (maxIntensity * cpt) / flashTime;
        }
    }
}
