using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rattus;

namespace Rattus
{
    public class bedroom_verification : MonoBehaviour
    {
        private float rotationSpeed = 45;
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();

        // Start is called before the first frame update
        void Start()
        {
            Conditions.Add("GotKeyBed", false);
            Conditions.Add("GotDoorknob", false); // requires keybed
            Conditions.Add("GotKeyBedside", false); // requires gotdoorknob
        }

        // Update is called once per frame
        void Update()
        {
            if(!Conditions["GotDoorknob"])
                Conditions["GotDoorknob"] = inventory.containsItem("door_knob");
            if(!Conditions["GotKeyBedside"])
                Conditions["GotKeyBedside"] = inventory.containsItem("rust_key");

            if (clickableObj.getLastClicked() != null)
            {
                //Debug.Log(clickableObj.getLastClicked().name);
                if (clickableObj.getLastClicked().name == "Key" || clickableObj.getLastClicked().name == "pillBottle" || clickableObj.getLastClicked().name=="BedBedding")
                {
                    inventory.addItemToInventory(GameObject.Find("Key"));
                    Conditions["GotKeyBed"] = true;
                    GameObject.Find("pillBottle").SetActive(false);
                }

                if (clickableObj.getLastClicked().name == "MedRackKnob" && Conditions["GotKeyBed"] && !Conditions["GotDoorknob"])
                {
                    Transform t = GameObject.Find("MedRackKnobDoor_L").transform;
                    Conditions["GotDoorknob"] = true;
                    t.localEulerAngles = new Vector3(-180, 90, 0);
                    inventory.addItemToInventory(GameObject.Find("door_knob"));
                    GameObject.Find("door_knob").SetActive(false);
                }

                if ((clickableObj.getLastClicked().name == "MirrorShelf_Case" || clickableObj.getLastClicked().name == "MirrorShelf_DoorL") && Conditions["GotDoorknob"] && !Conditions["GotKeyBedside"])
                {
                    Transform t = GameObject.Find("MirrorShelf_DoorL").transform;
                    t.localEulerAngles = new Vector3(0, 90, 0);
                    t.localScale = new Vector3(1, 1, 1);
                    Conditions["GotKeyBedside"] = true;
                    GameObject.Find("rust_key").SetActive(false);
                }

                if(clickableObj.getLastClicked().name=="Door" && Conditions["GotKeyBedside"])
                {
                    //TODO add move to next room
                }
            }
        }
    }
}