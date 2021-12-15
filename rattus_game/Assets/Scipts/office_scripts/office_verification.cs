using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rattus;

namespace Rattus
{
    public class office_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();

        // Start is called before the first frame update
        void Start()
        {
            Conditions.Add("ReadLetter", false);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}