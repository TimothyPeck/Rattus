using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rattus;

namespace Rattus
{
    public class oproom_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();

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

        }
    }
}