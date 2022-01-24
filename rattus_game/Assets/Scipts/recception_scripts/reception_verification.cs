using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reception_verification : MonoBehaviour
{
    private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
    private Inventory inventory = new Inventory();
    public Dialogue dialogue;
    public GameObject panelComputer;
    public bool correctPassword;

    // Start is called before the first frame update
    void Start()
    {
        Conditions.Add("OpenLockerL", false);
        Conditions.Add("GotTape", false);
        Conditions.Add("RepairedCable", false); // requires gotTape
        //Conditions.Add("CorrectPassword", false);
        correctPassword = false;

        dialogue.AddSentence("Mysterious voice", "The room where it all began.", 3);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
                dialogue.AddSentence("Me", "This appears to be one of the nurse's cats.");
                dialogue.AddSentence("Me", "It says it's name is Dusty and it belongs to Ethyll");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "postIt")
            {
                dialogue.AddSentence("Me", "it says that the password recently change");
                dialogue.AddSentence("Me", "now it's");
                dialogue.AddSentence("Me", "animalName_BirthYear_FavoriteColour",5);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "menuboard")
            {
                dialogue.AddSentence("Me", "The names and dates of birth of the on call staff" + "\n" + "Ethyll: 07/04/1924");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "Case_Door_R")
            {
                dialogue.AddSentence("Me", "The door seems to have coloured stickers on it.");
                dialogue.AddSentence("Me", "Betty likes green and Ethyll likes orange");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "angle_dog_picture")
            {
                dialogue.AddSentence("Me", "Clearly a very good boi", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "PC_Monitor" && !Conditions["RepairedCable"])
            {
                dialogue.AddSentence("Me", "The computer doesn't have power");
                dialogue.AddSentence("Me", "Maybe I should find a way to fix it.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else  if (lastClicked.name == "Case_Door_L" && !Conditions["OpenLockerL"])
            {
                Conditions["OpenLockerL"] = true;
                Transform t = GameObject.Find("Case_Door_L").transform;
                t.localEulerAngles = new Vector3(0, -90, 0);

                dialogue.AddSentence("Me", "What's this in here then?", 2);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if ((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && Conditions["GotTape"])
            {
                Conditions["RepairedCable"] = true;
                inventory.removeFromInventory("Tape");
                Transform t = GameObject.Find("Cardboard_box_1").transform;
                t.localPosition = new Vector3(-11F, -0.2087748F, -8F);

                dialogue.AddSentence("Me", "Aha, the computer work again!");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && !Conditions["GotTape"])
            {
                dialogue.AddSentence("Me", "It's seem to be broken, I need something to repaire it");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "PC_Monitor" && Conditions["RepairedCable"])
            {
                panelComputer.SetActive(true);
            }
            else if(lastClicked.name == "Door" && correctPassword) //n'arrive jamais ici
            {
                dialogue.AddSentence("Me", "I know exactly where to go now");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(lastClicked.name == "Door" && !correctPassword)
            {
                dialogue.AddSentence("Me", "I don't know where to go now, I need to find answers");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }

            clickableObj.resetLastClicked();
        }
        dialogue.empty();
    }
    public void objToInventory(GameObject objOnCam)
    {
        if (objOnCam.name == "Tape" && Conditions["OpenLockerL"])
        {
            Conditions["GotTape"] = true;
            inventory.addItemToInventory(GameObject.Find("Tape"));
            GameObject.Find("Tape").SetActive(false);

            dialogue.AddSentence("Me", "Some electrical tape, might be useful.");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else
        {
            dialogue.AddSentence("Me", "I don't think this is usefull to take it");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }

    }
}