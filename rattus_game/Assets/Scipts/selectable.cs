using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class selectable : MonoBehaviour
{
    public panelManager panScript;
    public GameObject panel;

    public Transform view;

    public Vector3 basePlace;
    public float speed = 0.3f;
    public bool moveToCam = false;
    public bool returnBasePos = false;
    public static bool onCam = false;


    // Start is called before the first frame update
    void Start()
    {
        panScript = FindObjectOfType<panelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToCam)
        {
            Vector3 a = transform.position;
            Vector3 b = view.position;
            transform.position = Vector3.MoveTowards(a, b, speed);
        }

        if (returnBasePos)
        {
            Vector3 a = transform.position;
            Vector3 b = basePlace;
            transform.position = Vector3.MoveTowards(a, b, speed);

            if (a == b)
            {
                // important de désactiver ici, sinon la pièce peut tournée quand l'objet est en l'air
                panel.SetActive(false);
                returnBasePos = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!onCam)
        {
            moveToCam = true;
            basePlace = transform.position;
            Debug.Log(name);
            onCam = true;
            returnBasePos = false;
            panScript.displayPanel(this); // envoie au PanelManger l'objet cliqué avec (this)
        }
    }

    public void returneBtn()
    {
        returnBasePos = true;
        onCam = false;
        moveToCam = false;
    }
}