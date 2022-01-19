using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectable : MonoBehaviour
{
    public Transform view;
    public Vector3 basePlace;
    public float speed;
    public bool move = false;
    public bool retourne = false;
   public static bool onCam = false;
    panelManager pan;

    // Start is called before the first frame update
    void Start()
    {
        pan = FindObjectOfType<panelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Vector3 a = transform.position;
            Vector3 b = view.position;
            transform.position = Vector3.MoveTowards(a, b, speed);
        }

        if (retourne)
        {
            Vector3 a = transform.position;
            Vector3 b = basePlace;
            transform.position = Vector3.MoveTowards(a, b, speed);
        }
    }

    private void OnMouseDown()
    {
       if(!onCam)
       {
            move = true;
            basePlace = transform.position;
            Debug.Log(name);
            onCam = true;
            retourne = false;
            pan.displayPanel(this);
       }
       
    }

    public void retourneBtn()
    {
            retourne = true;
            onCam = false;
        move = false;
    }
}
