using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rattus
{
    public class reception_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();
        public Dialogue dialogue;

        // Start is called before the first frame update
        void Start()
        {
            Conditions.Add("OpenLockerL", false);
            Conditions.Add("GotTape", false);
            Conditions.Add("RepairedCable", false); // requires gotTape
            Conditions.Add("CorrectPassword", false);

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

                if (lastClicked.name == "menuboard")
                {
                    dialogue.AddSentence("Me", "The names and dates of birth of the on call staff" + "\n" + "Ethyll: 07/04/1924");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

                if (lastClicked.name == "Case_Door_R")
                {
                    dialogue.AddSentence("Me", "The door seems to have coloured stickers on it.");
                    dialogue.AddSentence("Me", "Betty likes green and Ethyll likes orange");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

                if (lastClicked.name == "angle_dog_picture")
                {
                    dialogue.AddSentence("Me", "Clearly a very good boi", 3);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

                if(lastClicked.name== "PC_Monitor" && !Conditions["RepairedCable"])
                {
                    dialogue.AddSentence("Me", "The computer doesn't have power, maybe I should find a way to fix it.");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

                if(lastClicked.name== "Case_Door_L" && !Conditions["OpenLockerL"])
                {
                    Conditions["OpenLockerL"] = true;
                    Transform t = GameObject.Find("Case_Door_L").transform;
                    t.localEulerAngles = new Vector3(0, -90, 0);

                    dialogue.AddSentence("Me", "What's this in here then?",2);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

                if (lastClicked.name == "Tape" && Conditions["OpenLockerL"])
                {
                    Conditions["GotTape"] = true;
                    inventory.addItemToInventory(GameObject.Find("Tape"));
                    GameObject.Find("Tape").SetActive(false);

                    dialogue.AddSentence("Me", "Some electrical tape, might be useful.");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }

                if ((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && Conditions["GotTape"])
                {
                    Conditions["RepairedCable"] = true;
                    inventory.removeFromInventory("Tape");
                    Transform t = GameObject.Find("Cardboard_box_1").transform;
                    t.localPosition = new Vector3(-11F, -0.2087748F, -8F);

                    dialogue.AddSentence("Me", "Aha, the computer works again.");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
            dialogue.empty();
        }
    }
}