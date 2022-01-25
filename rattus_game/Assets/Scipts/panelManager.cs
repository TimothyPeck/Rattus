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
    oproom_verification opRoomScript;

    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        bedroomScript = FindObjectOfType<bedroom_verification>();
        recepScript = FindObjectOfType<reception_verification>();
        opRoomScript = FindObjectOfType<oproom_verification>();
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
        //Il sait précisement quel objet a été cliqué, bien que tout les objets on le même script
        //Grâce au "this" passer a la fonction dans le script selectable
        this.selectableScript = sel;
        panel.SetActive(true);
    }

    public void takeIt()
    {
        returnToBasePos();
        //panel.SetActive(false);       


        //Regarde dans quel scene il se trouve, pour lancé la bonne fonction
        if (SceneManager.GetActiveScene().name == "chambre")
        {
            bedroomScript.objToInventory(selectableScript.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "reception")
        {
            recepScript.objToInventory(selectableScript.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "salleOP")
        {
            opRoomScript.objToInventory(selectableScript.gameObject);
        }
    }
}