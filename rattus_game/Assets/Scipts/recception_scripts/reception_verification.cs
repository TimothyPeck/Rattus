using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rattus;

namespace Rattus
{
    public class reception_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();

        // Start is called before the first frame update
        void Start()
        {
            Conditions.Add("GotTape", false);
            Conditions.Add("RepairedCable", false); // requires gotTape
            Conditions.Add("CorrectPassword", false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}