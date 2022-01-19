using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelManager : MonoBehaviour
{
    selectable sel;

    [SerializeField] private GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void retourne()
    {
        sel.retourneBtn();
        panel.SetActive(false);

    }

    public void displayPanel(selectable sel)
    {
        this.sel = sel;
        panel.SetActive(true);
    }
}
