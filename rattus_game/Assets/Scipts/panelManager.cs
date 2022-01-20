using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelManager : MonoBehaviour
{
    selectable selectableScript;
    bedroom_verification bedroomScript;

    [SerializeField] private GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        bedroomScript = FindObjectOfType<bedroom_verification>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnToBasePos()
    {
        selectableScript.returneBtn();
        //panel.SetActive(false);

    }

    public void displayPanel(selectable sel)
    {
        this.selectableScript = sel;
        panel.SetActive(true);
    }

    public void takeIt()
    {
        bedroomScript.objToInventory(selectableScript.gameObject);
        panel.SetActive(false);
        returnToBasePos();
    }
}
