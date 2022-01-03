using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rattus
{
    public class reception_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();

        // Start is called before the first frame update
        void Start()
        {
            Conditions.Add("OpenLockerL", false);
            Conditions.Add("GotTape", false);
            Conditions.Add("RepairedCable", false); // requires gotTape
            Conditions.Add("CorrectPassword", false);
        }

        // Update is called once per frame
        void Update()
        {
            GameObject lastClicked = clickableObj.getLastClicked();
            if (lastClicked != null)
            {
                //Debug.Log(lastClicked.name);
                if (lastClicked.name == "picture")
                {
                    //TODO dialog
                    Debug.Log("This appears to be one of the nurses cat's.");
                    Debug.Log("It says it's name is Dusty and it belongs to Ethyll");
                }

                if (lastClicked.name == "menuboard")
                {
                    //TODO dialog
                    Debug.Log("The names and dates of birth of the on call staff");
                    Debug.Log("Ethyll: 07/04/1924");
                }

                if (lastClicked.name == "Case_Door_R")
                {
                    Debug.Log("The door seems to have coloured stickers on it.");
                    Debug.Log("Betty apparently likes green and Ethyll likes orange");
                }

                if (lastClicked.name == "angle_dog_picture")
                {
                    Debug.Log("A good boi");
                }

                if(lastClicked.name== "Case_Door_L" && !Conditions["OpenLockerL"])
                {
                    Conditions["OpenLockerL"] = true;
                    Transform t = GameObject.Find("Case_Door_L").transform;
                    t.localEulerAngles = new Vector3(0, -90, 0);
                }

                if (lastClicked.name == "Tape" && Conditions["OpenLockerL"])
                {
                    Conditions["GotTape"] = true;
                    inventory.addItemToInventory(GameObject.Find("Tape"));
                    GameObject.Find("Tape").SetActive(false);
                }

                if ((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && Conditions["GotTape"])
                {
                    Conditions["RepairedCable"] = true;
                    inventory.removeFromInventory("Tape");
                    Transform t = GameObject.Find("Cardboard_box_1").transform;
                    t.localPosition = new Vector3(-11F, -0.2087748F, -8F);
                }

                if (lastClicked.name == "PC_Monitor" && Conditions["RepairedCable"])
                {
                    if (true) // TODO add ui to enter password
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
                clickableObj.resetLastClicked();
            }
        }
    }
}