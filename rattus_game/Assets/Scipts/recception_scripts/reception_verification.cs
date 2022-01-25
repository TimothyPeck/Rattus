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

        dialogue.AddSentence("Mysterious voice", "The room where it all began.", 4);
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
                dialogue.AddSentence("Me", "This appears to be one of the nurse's cats.", 4);
                dialogue.AddSentence("Me", "It says it's name is Dusty and it belongs to Ethyll", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if(lastClicked.name== "Plate_LOD0" || lastClicked.name== "Plate_LOD1" || lastClicked.name=="Switch")
            {
                GameObject sceneLight = GameObject.Find("Room_light");

                Debug.Log(sceneLight.GetComponent<Light>().intensity);

                if (sceneLight.GetComponent<Light>().intensity == 0)
                {
                    sceneLight.GetComponent<Light>().intensity = 3.5F;
                }
                else
                {
                    sceneLight.GetComponent<Light>().intensity = 0;
                }
            }
            else if (lastClicked.name == "postIt")
            {
                dialogue.AddSentence("Me", "It says that the password recently change.",4);
                dialogue.AddSentence("Me", "Now it's:",3);
                dialogue.AddSentence("Me", "animalName_BirthYear_FavoriteColour",5);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "menuboard")
            {
                dialogue.AddSentence("Me", "The names and dates of birth of the on call staff" + "\n" + "Ethyll: 07/04/1924", 8);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "Case_Door_R")
            {
                dialogue.AddSentence("Me", "The door seems to have coloured stickers on it.", 6);
                dialogue.AddSentence("Me", "Betty likes green and Ethyll likes orange", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "angle_dog_picture")
            {
                dialogue.AddSentence("Me", "Clearly a very good boi", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "PC_Monitor" && !Conditions["RepairedCable"])
            {
                dialogue.AddSentence("Me", "The computer doesn't have power", 4);
                dialogue.AddSentence("Me", "Maybe I should find a way to fix it.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else  if (lastClicked.name == "Case_Door_L" && !Conditions["OpenLockerL"])
            {
                Conditions["OpenLockerL"] = true;
                Transform t = GameObject.Find("Case_Door_L").transform;
                t.localEulerAngles = new Vector3(0, -90, 0);

                dialogue.AddSentence("Me", "What's this in here then?", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if ((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && Conditions["GotTape"])
            {
                Conditions["RepairedCable"] = true;
                inventory.removeFromInventory("Tape");
                Transform t = GameObject.Find("Cardboard_box_1").transform;
                t.localPosition = new Vector3(-11F, -0.2087748F, -8F);

                dialogue.AddSentence("Me", "Aha, the computer works again!", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && !Conditions["GotTape"])
            {
                dialogue.AddSentence("Me", "Its seems to be broken, I need something to repair it", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "PC_Monitor" && Conditions["RepairedCable"])
            {
                panelComputer.SetActive(true);
            }
            else if(lastClicked.name == "Door" && correctPassword) //n'arrive jamais ici
            {
                dialogue.AddSentence("Me", "I know exactly where to go now.", 4);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(lastClicked.name == "Door" && !correctPassword)
            {
                dialogue.AddSentence("Me", "I don't know where to go now, I need to find answers.", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }

            clickableObj.resetLastClicked();
        }
        dialogue.empty();
    }

    /// <summary>
    /// Piecks up an item and shows dialogue when put down
    /// </summary>
    /// <param name="objOnCam">GameObject of the last clicked object, available via clickableObj</param>
    public void objToInventory(GameObject objOnCam)
    {
        if (objOnCam.name == "Tape_parent" && Conditions["OpenLockerL"])
        {
            Conditions["GotTape"] = true;
            inventory.addItemToInventory(GameObject.Find("Tape"));
            GameObject.Find("Tape").SetActive(false);

            dialogue.AddSentence("Me", "Some electrical tape, might be useful.", 4);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else
        {
            dialogue.AddSentence("Me", "I don't think it's worth taking this",4);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }

    }
}