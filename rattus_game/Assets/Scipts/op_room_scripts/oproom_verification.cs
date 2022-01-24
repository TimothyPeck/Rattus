using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class oproom_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();
        public Dialogue dialogue;

        // Start is called before the first frame update
        void Start()
        {

            Conditions.Add("GotOpReport", false);
            Conditions.Add("GotSaw", false);
            Conditions.Add("CaseOpen", false);
            Conditions.Add("GotBattery", false);
            Conditions.Add("GotFuseFloor", false);
            Conditions.Add("GotFuseTable", false);
            Conditions.Add("GotFuseCabinet", false);
            Conditions.Add("ReplacedFuseFloor", false);
            Conditions.Add("ReplacedFuseTable", false);
            Conditions.Add("ReplacedFuseCabinet", false);
            Conditions.Add("ChargedSaw", false); // requires gotbattery and GotSaw
            Conditions.Add("OpenedMedRack", false); // requires charged saw
            Conditions.Add("LightOn", false); // requires all fuses
                                              //Conditions.Add("CanOpenDoor", false); // redundant if all others are true

            dialogue.AddSentence("Mysterious voice", "You have finally entered my final challenge.", 5);
            dialogue.AddSentence("Mysterious voice", "The operating room of horrific suffering.", 7);
            dialogue.AddSentence("Mysterious voice", "This is where the most barbaric operations took place.", 9);
            dialogue.AddSentence("Mysterious voice", "Just look at all those lobotomy instruments. So beautiful.", 9);
            dialogue.AddSentence("Me", "My captor seems to think all of this is fun", 6);
            dialogue.AddSentence("Me", "as if it's all some kind of game.", 6);
            dialogue.AddSentence("Me", "Even if it is, I still need to escape.", 4);
            dialogue.AddSentence("Me", "If only I had some light.", 4);
        
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogue.empty();
        }

        // Update is called once per frame
        void Update()
        {
            GameObject lastClicked = clickableObj.getLastClicked();
            if (lastClicked != null)
            {
                //Debug.Log(lastClicked.name);

                if (lastClicked.name == "MedRackDoor_L" && !Conditions["OpenedMedRack"] && Conditions["ChargedSaw"])
                {
                    Conditions["OpenedMedRack"] = true;
                    Transform t = GameObject.Find("MedRackDoor_L").transform;
                    t.localEulerAngles = new Vector3(-180, 60, 0);

                    dialogue.AddSentence("Me", "Finally I can get to this clipboard.", 4);
                    dialogue.AddSentence("Mysterious voice", "Now you will understand why I put you through all this,", 10);
                    dialogue.AddSentence("Mysterious voice", "why I had to make you suffer,", 10);
                    dialogue.AddSentence("Mysterious voice", "why I needed to see you fear like she feared.", 10);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
                else if ((lastClicked.name == "CaseMetallic" || lastClicked.name == "Case_Door_R") && !Conditions["CaseOpen"])
                {
                    Conditions["CaseOpen"] = true;
                    Transform t = GameObject.Find("Case_Door_R").transform;
                    t.localEulerAngles = new Vector3(0, -120, 0);

                    dialogue.AddSentence("Me", "Something is flashing in there", 2);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
                else if (Conditions["GotBattery"] && Conditions["GotSaw"] && !Conditions["ChargedSaw"])
                {
                    Conditions["ChargedSaw"] = true;

                    dialogue.AddSentence("Me", "Now I can open the cabinet.");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
                else  if (Conditions["GotOpReport"] && (lastClicked.name.Contains("DoorD_V2")))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if(!Conditions["GotOpReport"] && (lastClicked.name.Contains("DoorD_V2")))
                {
                dialogue.AddSentence("Me", "I need to find out why I am here.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }            
                else if(lastClicked.name == "MedRack" && !Conditions["ChargedSaw"])
                {
                    dialogue.AddSentence("Me", "This door is locked and the keyhole has been filed,", 4);
                    dialogue.AddSentence("Me", "I need to find a different way in.", 4);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
                if (lastClicked.name == "Fuse_Box_01" && !Conditions["LightOn"])
                {
                    dialogue.AddSentence("Me", "The fuses seem to be missing, maybe I should find new ones");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
            if (lastClicked.name == "Fuse_Box_01")
            {
                Transform t = GameObject.Find("Fuse_Box_Door").transform;
                t.localEulerAngles = new Vector3(0, 150, 0);

                if (Conditions["GotFuseFloor"])
                {
                    Conditions["ReplacedFuseFloor"] = true;
                    inventory.removeFromInventory("fuseFloor");
                }

                if (Conditions["GotFuseTable"])
                {
                    Conditions["ReplacedFuseTable"] = true;
                    inventory.removeFromInventory("fuseTable");
                }

                if (Conditions["GotFuseCabinet"])
                {
                    Conditions["ReplacedFuseCabinet"] = true;
                    inventory.removeFromInventory("fusecontainer");
                }
            }
            if (Conditions["ReplacedFuseFloor"] && Conditions["ReplacedFuseCabinet"] && Conditions["ReplacedFuseTable"] && !Conditions["LightOn"])
            {
                Conditions["LightOn"] = true;
                GameObject.Find("RoomLight").GetComponent<Light>().intensity = 3;

                dialogue.AddSentence("Me", "Finally, I can see again, wow this room is disgusting.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            if ((lastClicked.name == "OpReport") && Conditions["OpenedMedRack"] && Conditions["LightOn"])
            {
                Conditions["GotOpReport"] = true;
                inventory.addItemToInventory(GameObject.Find("Clipboard"));
                GameObject.Find("Clipboard").SetActive(false);

                dialogue.AddSentence("Me", "It's an operation report for a Josephine Taylor.", 6);
                dialogue.AddSentence("Me", "Date of birth: 21/06/1953", 6);
                dialogue.AddSentence("Mysterious voice", "She was everything to me.", 3);
                dialogue.AddSentence("Me", "The operation is noted as being a Hemispherectomy.", 6);
                dialogue.AddSentence("Me", "removal of one half of the brain to reduce seizures.", 6);
                dialogue.AddSentence("Me", "Wait, why do I know that?", 3);
                dialogue.AddSentence("Mysterious voice", "She was never the same after,", 7);
                dialogue.AddSentence("Mysterious voice", "it was as if she was turned into a mere statue of her former self", 7);
                dialogue.AddSentence("Me", "The doctor says the operation was a success,", 6);
                dialogue.AddSentence("Me", "with no noted incidents prior to or during the procedure.", 6);
                dialogue.AddSentence("Me", "But he did note that it took twice as long as usual", 4);
                dialogue.AddSentence("Me", "due to a lack of staff.", 4);
                dialogue.AddSentence("Mysterious voice", "You see John, you let the doctors run loose in your hospital,", 6);
                dialogue.AddSentence("Mysterious voice", "ruining lives and separating families.", 6);
                dialogue.AddSentence("Mysterious voice", "She wasn't just a guinea pig for your medical experimentation,", 8);
                dialogue.AddSentence("Mysterious voice", "she was my Josie, and you will pay for what you did.", 8);
                dialogue.AddSentence("Me", "So he must Josephine's husband,", 6);
                dialogue.AddSentence("Me", "he seems to have it out for me, I should go looking for him.", 6);
                dialogue.AddSentence("Mr. Taylor", "...", 2);
                dialogue.AddSentence("Mr. Taylor", "You'll find me soon enough, see you later John");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if ((lastClicked.name == "OpReport") && Conditions["OpenedMedRack"] && !Conditions["LightOn"])
            {
                dialogue.AddSentence("Me", "It's too dark, I can't read");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
            clickableObj.resetLastClicked();
            dialogue.empty();
        }
        public void objToInventory(GameObject objOnCam)
        {  

            if (objOnCam.name == "chainsaw")
            {
            Conditions["GotSaw"] = true;
            inventory.addItemToInventory(GameObject.Find("Chainsaw01"));
            GameObject.Find("Chainsaw01").SetActive(false);

            dialogue.AddSentence("Me", "Might be useful somewhere,", 4);
            dialogue.AddSentence("Me", "seems to be missing it's battery though.", 4);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (objOnCam.name == "Battery" && Conditions["CaseOpen"])
            {
            Conditions["GotBattery"] = true;
            inventory.addItemToInventory(GameObject.Find("Battery"));
            GameObject.Find("Battery").SetActive(false);
            GameObject.Find("BatteryLight").SetActive(false);

            dialogue.AddSentence("Me", "Might be useful for that electric saw over there.");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (objOnCam.name == "fusePurple")
            {
            Conditions["GotFuseTable"] = true;
            inventory.addItemToInventory(GameObject.Find("fuseTable"));
            GameObject.Find("fuseTable").SetActive(false);

            dialogue.AddSentence("Me", "I may be able to see again thanks to this.", 3);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (objOnCam.name == "fuseRed")
            {
            Conditions["GotFuseFloor"] = true;
            inventory.addItemToInventory(GameObject.Find("fuseFloor"));
            GameObject.Find("fuseFloor").SetActive(false);

            dialogue.AddSentence("Me", "This might come in handy.", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (objOnCam.name == "fuseYellow")
            {
            Conditions["GotFuseCabinet"] = true;
            inventory.addItemToInventory(GameObject.Find("fusecontainer"));
            GameObject.Find("fusecontainer").SetActive(false);

            dialogue.AddSentence("Me", "This might be useful.", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        else
            {
            dialogue.AddSentence("Me", "I don't think this is usefull to take it");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }

        }
    }
