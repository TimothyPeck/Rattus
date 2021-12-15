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

    void FlickerLight()
    {

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            Light.enabled = !Light.enabled;
            timer = Random.Range(minTime, maxTime);
        }
    }
}
