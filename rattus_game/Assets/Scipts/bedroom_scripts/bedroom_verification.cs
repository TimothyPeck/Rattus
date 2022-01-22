using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class bedroom_verification : MonoBehaviour
    {
        public Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();
        public Dialogue dialogue;

        // Start is called before the first frame update
        void Start()
        {
            GameObject sceneLight = GameObject.Find("Spot Light");

            sceneLight.GetComponent<Light>().spotAngle = 126;
            sceneLight.GetComponent<Light>().range = 36;
            sceneLight.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Mixed;
            sceneLight.GetComponent<Light>().color = new Color(0xFF, 0xD2, 0x8F);
            sceneLight.GetComponent<Light>().intensity = 0.025F;
            sceneLight.GetComponent<Light>().shadowStrength = 1;
            sceneLight.GetComponent<Light>().shadowBias = 0.011F;
            sceneLight.GetComponent<Light>().shadowNormalBias = 0.4F;
            sceneLight.GetComponent<Light>().shadowNearPlane = 0.2F;

            Conditions.Add("GotKeyBed", false);
            Conditions.Add("GotDoorknob", false); // requires keybed
            Conditions.Add("OpenMedRack", false);
            Conditions.Add("OpenBedside", false);
            Conditions.Add("GotKeyBedside", false); // requires gotdoorknob

            dialogue.AddSentence("Me", "I seem to have been trapped in here, I need to find a way out.");
            dialogue.AddSentence("Mysterious voice", "Hello John, as you can see I have trapped you in this small room from which you will never escape.");
            dialogue.AddSentence("Mysterious voice", "In time you will understand why I have trapped you here, but for now I shall let you rot in the prison cell");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogue.empty();
        }

        // Update is called once per frame
        void Update()
        {
            GameObject lastClicked = clickableObj.getLastClicked();

        if (lastClicked != null)
        {
            if (lastClicked.name == "Door")
            {
                dialogue.AddSentence("Me", "It's locked \nI need to find the key");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "BedBedding")
            {
                dialogue.AddSentence("Me", "There appears to be a bottle on the pillow, it seems to have something in it.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "picture")
            {
                dialogue.AddSentence("Me", "Just a derpy cat, it's not useful to my escape.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "SofaPillow" || lastClicked.name == "SofaMain")
            {
                dialogue.AddSentence("Me", "A very comfortable looking sofa, but now is not the time for relaxation.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if ((lastClicked.name == "MedRackKnob" || lastClicked.name == "MedRackKnobDoor_L") && !Conditions["GotKeyBed"])
            {
                dialogue.AddSentence("Me", "Locked, there must be a key somewhere.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if ((lastClicked.name == "MedRackKnob" ||
                       lastClicked.name == "MedRackKnobDoor_L") &&
                       Conditions["GotKeyBed"] &&
                       !Conditions["GotDoorknob"])
            {
                Conditions["OpenMedRack"] = true;
                Transform t = GameObject.Find("MedRackKnobDoor_L").transform;
                t.localEulerAngles = new Vector3(-180, 90, 0);

                dialogue.AddSentence("Me", "There's something in here.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if ((lastClicked.name == "MirrorShelf_Case" ||
                      lastClicked.name == "MirrorShelf_DoorL") &&
                      Conditions["GotDoorknob"] &&
                      !Conditions["GotKeyBedside"])
            {
                Conditions["OpenBedside"] = true;
                Transform t = GameObject.Find("MirrorShelf_DoorL").transform;
                t.localEulerAngles = new Vector3(0, 90, 0);
                t.localScale = new Vector3(1, 1, 1);

                dialogue.AddSentence("Me", "Maybe there is something behind the rabbit?");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            else if (lastClicked.name == "Door" && Conditions["GotKeyBedside"])
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        
            clickableObj.resetLastClicked();
        }
            dialogue.empty();
        }

        private void showDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogue.empty();
        }

        public void objToInventory(GameObject objOnCam)
        {
        if (objOnCam.name == "pills")
        {
            inventory.addItemToInventory(GameObject.Find("Key"));
            Conditions["GotKeyBed"] = true;
            GameObject.Find("pillBottle").SetActive(false);

            dialogue.AddSentence("Me", "This bottle contains a key, I might be able to open something with it.");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else if (objOnCam.name == "knob_door" && Conditions["OpenMedRack"])
        {
            Conditions["GotDoorknob"] = true;
            inventory.addItemToInventory(GameObject.Find("door_knob"));
            GameObject.Find("door_knob").SetActive(false);

            dialogue.AddSentence("Me", "This doorknob seems to fit that bedside cabinet over there.\nMaybe it contains something useful.");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        }
        else if ( objOnCam.name == "Rusty_Key" && Conditions["OpenBedside"])
        {
            Conditions["GotKeyBedside"] = true;
            GameObject.Find("rust_key").SetActive(false);

            dialogue.AddSentence("Me", "A rusty key, it must fit the door.");
            dialogue.AddSentence("Mysterious voice", "I see you have found the key, but this is just the start of the fun!\nMwahahahaha");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        } else
        {
            dialogue.AddSentence("Me", "I don't think this is usefull.");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
    }
