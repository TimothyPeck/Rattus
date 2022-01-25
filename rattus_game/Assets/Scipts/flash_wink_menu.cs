using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash_wink_menu : MonoBehaviour
{
    public Light Light;

    public float minTime;
    public float maxTime;
    public float timer;
    public float maxIntensity;

    private bool on = true;


    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        FlickerLight();
    }

    /// <summary>
    /// turns the light on or off for varying amounts of time, to emulate a faulty fixture
    /// </summary>
    void FlickerLight()
    {

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            if (on)
            {
                Light.intensity = maxIntensity / 2;
            }
            else
            {
                Light.intensity = maxIntensity;
            }
            timer = Random.Range(minTime, maxTime);
            on = !on;
        }
    }
}
