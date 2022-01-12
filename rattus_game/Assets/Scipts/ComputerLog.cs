using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerLog : MonoBehaviour
{
    public InputField password;
    public GameObject wrongPassword, desktop, log;

    // Start is called before the first frame update
    void Start()
    {
        log.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Reset()
    {
        password.text = "";
    }

    public void OK()
    {
        if(password.text == "dusty_1924_Orange")
        {
            //load l'écran de l'ordi
            desktop.SetActive(true);
            log.SetActive(false);
            Debug.Log("mdp correct");
        }
        else
        {
            //load mauvais mot de passe
            Debug.Log("mdp faux");
            wrongPassword.SetActive(true);
            password.text = "";
        }
    }

}
