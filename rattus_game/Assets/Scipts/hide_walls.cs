using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hide_walls : MonoBehaviour
{
    public GameObject btnLeft, btnRight;
    List<GameObject> walls = new List<GameObject>();
    GameObject diagCanvas;
    int indexL = 1 , indexR = 2;

    // Start is called before the first frame update
    void Start()
    {
       
        GameObject wallXNeg = GameObject.Find("XNeg");
        GameObject wallXPos = GameObject.Find("XPos");
        GameObject wallZNeg = GameObject.Find("ZNeg");
        GameObject wallZPos = GameObject.Find("ZPos");

        walls.Add(wallXPos);
        walls.Add(wallZNeg);
        walls.Add(wallXNeg);
        walls.Add(wallZPos);

        foreach(GameObject wall in walls)
        {
            wall.SetActive(true);
        }

<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> ea93c4d (Remove test spheres, comment in hide_walls.cs)
=======
>>>>>>> cb33bf6 (rebase)
        //0-> XPos
=======
        //0-> xNeg
>>>>>>> cd53f00 (good rotation object)
        //1-> ZNeg
        //2-> XNeg
        //3-> ZPos
        walls[indexL].SetActive(false);
        walls[indexR].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            walls[indexL].SetActive(true);
            walls[indexR].SetActive(false);
            indexL = indexR;
            indexR++;
            if (indexR == 4)
                indexR = 0;
            walls[indexR].SetActive(false);
        }else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            walls[indexR].SetActive(true);
            walls[indexL].SetActive(false);
            indexR = indexL;
            indexL--;
            if (indexL < 0)
                indexL = 3;
            walls[indexL].SetActive(false);
        }
        */
    }

    public void buttonLeftClicked()
    {
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> d20f69c (mettre au propre, changer btn)
        walls[indexL].SetActive(true);
        walls[indexR].SetActive(false);
        indexL = indexR;
        indexR++;
        if (indexR == 4)
            indexR = 0;
        walls[indexR].SetActive(false);

    }

    public void buttonRightClicked()
    {
        
<<<<<<< HEAD
=======
>>>>>>> f9ff39b (rebase)
=======
>>>>>>> d20f69c (mettre au propre, changer btn)
        walls[indexR].SetActive(true);
        walls[indexL].SetActive(false);
        indexR = indexL;
        indexL--;
        if (indexL < 0)
            indexL = 3;
        walls[indexL].SetActive(false);
<<<<<<< HEAD
=======
    }
<<<<<<< HEAD

    public void buttonRightClicked()
    {

        walls[indexL].SetActive(true);
        walls[indexR].SetActive(false);
        indexL = indexR;
        indexR++;
        if (indexR == 4)
            indexR = 0;
        walls[indexR].SetActive(false);
>>>>>>> f9ff39b (rebase)
    }
=======
>>>>>>> d20f69c (mettre au propre, changer btn)
}
