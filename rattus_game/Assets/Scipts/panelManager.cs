using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class panelManager : MonoBehaviour
{
    selectable selectableScript;
    bedroom_verification bedroomScript;
    reception_verification recepScript;

    [SerializeField] private GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        bedroomScript = FindObjectOfType<bedroom_verification>();
        recepScript = FindObjectOfType<reception_verification>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnToBasePos()
    {
        selectableScript.returneBtn();
        panel.SetActive(false);

    }

    public void displayPanel(selectable sel)
    {
        this.selectableScript = sel;
        panel.SetActive(true);
    }

    public void takeIt()
    {   
        returnToBasePos();
        //panel.SetActive(false);       

        if(SceneManager.GetActiveScene().name=="chambre")
        {
        bedroomScript.objToInventory(selectableScript.gameObject);
        }
        else if(SceneManager.GetActiveScene().name == "reception")
        {
        recepScript.objToInventory(selectableScript.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "salleOP")
        {
            //recepScript.objToInventory(selectableScript.gameObject);
        }
    }
}
