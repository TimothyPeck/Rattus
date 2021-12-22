using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rattus;

namespace Rattus
{
    public class bedroom_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();

        // Start is called before the first frame update
        void Start()
        {
            Conditions.Add("GotKeyBed", false);
            Conditions.Add("GotDoorknob", false); // requires keybed
            Conditions.Add("OpenMedRack", false);
            Conditions.Add("OpenBedside", false);
            Conditions.Add("GotKeyBedside", false); // requires gotdoorknob
        }

        // Update is called once per frame
        void Update()
        {
            GameObject lastClicked = clickableObj.getLastClicked();
            if (lastClicked != null)
            {
                if (
                    lastClicked.name == "Key" || 
                    lastClicked.name == "pillBottleCap1" || 
                    lastClicked.name == "pillBottleCap2" || 
                    lastClicked.name == "pillBottleBody")
                {
                    inventory.addItemToInventory(GameObject.Find("Key"));
                    Conditions["GotKeyBed"] = true;
                    GameObject.Find("pillBottle").SetActive(false);
                }

                if ((
                        lastClicked.name == "MedRackKnob" || 
                        lastClicked.name == "MedRackKnobDoor_L") && 
                    Conditions["GotKeyBed"] && 
                    !Conditions["GotDoorknob"])
                {
                    Conditions["OpenMedRack"] = true;
                    Transform t = GameObject.Find("MedRackKnobDoor_L").transform;
                    t.localEulerAngles = new Vector3(-180, 90, 0);
                }

                if((
                        lastClicked.name=="door_knob_1" || 
                        lastClicked.name == "door_knob_2" || 
                        lastClicked.name == "door_knob_3" || 
                        lastClicked.name == "door_knob_4" )&& 
                    Conditions["OpenMedRack"])
                {
                    Conditions["GotDoorknob"] = true;
                    inventory.addItemToInventory(GameObject.Find("door_knob"));
                    GameObject.Find("door_knob").SetActive(false);
                }

                if ((
                        lastClicked.name == "MirrorShelf_Case" || 
                        lastClicked.name == "MirrorShelf_DoorL") && 
                    Conditions["GotDoorknob"] && 
                    !Conditions["GotKeyBedside"])
                {
                    Conditions["OpenBedside"] = true;
                    Transform t = GameObject.Find("MirrorShelf_DoorL").transform;
                    t.localEulerAngles = new Vector3(0, 90, 0);
                    t.localScale = new Vector3(1, 1, 1);
                }

                if ((
                        lastClicked.name=="rust_key" || 
                        lastClicked.name=="RABBIT") &&
                    Conditions["OpenBedside"])
                {
                    Conditions["GotKeyBedside"] = true;
                    GameObject.Find("rust_key").SetActive(false);
                }

                if(lastClicked.name=="Door" && Conditions["GotKeyBedside"])
                {
                    //TODO add move to next room
                    Debug.Log("Complete");
                }

                clickableObj.resetLastClicked();
            }
        }
    }
}