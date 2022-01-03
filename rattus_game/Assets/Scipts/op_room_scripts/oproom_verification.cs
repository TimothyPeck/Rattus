using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rattus;

namespace Rattus
{
    public class oproom_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();

        // Start is called before the first frame update
        void Start()
        {

            Conditions.Add("GotOpReport", false);
            Conditions.Add("GotSaw", false);
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
        }

        // Update is called once per frame
        void Update()
        {
            if (Conditions["ReplacedFuseFloor"] && Conditions["ReplacedFuseTable"] && Conditions["ReplacedFuseCabinet"])
            {
                //Debug.Log(lastClicked.name);
                if (lastClicked.name == "Fuse_Box_01" && !Conditions["LightOn"])
                {
                    Debug.Log("The fuses seem to be missing, maybe I should find new ones");
                }

                if (lastClicked.name == "MedRackDoor_L" && !Conditions["OpenedMedRack"] && Conditions["ChargedSaw"])
                {
                    Conditions["OpenedMedRack"] = true;
                    Transform t = GameObject.Find("MedRackDoor_L").transform;
                    t.localEulerAngles = new Vector3(-180, 60, 0);
                }

                if ((lastClicked.name == "MedRack") && Conditions["OpenedMedRack"])
                {
                    Conditions["GotOpReport"] = true;
                    inventory.addItemToInventory(GameObject.Find("Clipboard"));
                    GameObject.Find("Clipboard").SetActive(false);
                }

                if (lastClicked.name == "Chainsaw01")
                {
                    Conditions["GotSaw"] = true;
                    inventory.addItemToInventory(GameObject.Find("Chainsaw01"));
                    GameObject.Find("Chainsaw01").SetActive(false);
                }

                if ((lastClicked.name == "CaseMetallic" || lastClicked.name == "Case_Door_R") && !Conditions["CaseOpen"])
                {
                    Conditions["CaseOpen"] = true;
                    Transform t = GameObject.Find("Case_Door_R").transform;
                    t.localEulerAngles = new Vector3(0, -120, 0);
                }

                if (lastClicked.name == "Battery" && Conditions["CaseOpen"])
                {
                    Conditions["GotBattery"] = true;
                    inventory.addItemToInventory(GameObject.Find("Battery"));
                    GameObject.Find("Battery").SetActive(false);
                    GameObject.Find("BatteryLight").SetActive(false);
                }

                if (lastClicked.name == "fuseTableNode")
                {
                    Conditions["GotFuseTable"] = true;
                    inventory.addItemToInventory(GameObject.Find("fuseTable"));
                    GameObject.Find("fuseTable").SetActive(false);
                }

                if (lastClicked.name == "fuseFloorNode")
                {
                    Conditions["GotFuseFloor"] = true;
                    inventory.addItemToInventory(GameObject.Find("fuseFloor"));
                    GameObject.Find("fuseFloor").SetActive(false);
                }

                if (lastClicked.name == "fuseContainerNode")
                {
                    Conditions["GotFuseCabinet"] = true;
                    inventory.addItemToInventory(GameObject.Find("fusecontainer"));
                    GameObject.Find("fusecontainer").SetActive(false);
                }

                if(lastClicked.name== "Fuse_Box_01")
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

                if(Conditions["GotBattery"] && Conditions["GotSaw"])
                {
                    Conditions["ChargedSaw"] = true;
                }

                if(Conditions["ReplacedFuseFloor"] && Conditions["ReplacedFuseCabinet"] && Conditions["ReplacedFuseTable"])
                {
                    Conditions["LightOn"] = true;
                    GameObject.Find("RoomLight").GetComponent<Light>().intensity = 3;
                }

                if (Conditions["LightOn"] && (lastClicked.name.Contains("DoorD_V2")))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}