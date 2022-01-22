using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickableObj : MonoBehaviour
{
    /*
    public Transform[] objclickable;

    public Transform Objreturn;
    */
    private static GameObject lastClicked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.gameObject != null)
                {
                    lastClicked=(hit.transform.gameObject);
                    printName(hit.transform.gameObject);
                }
            }
        }
    }

    public static GameObject getLastClicked()
    {
        return lastClicked;
    }

    public static void resetLastClicked()
    {
        lastClicked = null;
    }

    void printName(GameObject go)
    {
        print(go.name);
    }
}