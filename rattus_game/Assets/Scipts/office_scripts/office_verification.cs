using System.Collections.Generic;
using UnityEngine;

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
            GameObject lastClicked = clickableObj.getLastClicked();
            if (lastClicked != null)
            {
                //Debug.Log(lastClicked.name);
                if (!Conditions["ReadLetter"] && (lastClicked.name == "Board" || lastClicked.name == "PAGE" || lastClicked.name == "clipboardText"))
                {
                    Conditions["ReadLetter"] = true;
                    //TODO Show letter to player
                }

                clickableObj.resetLastClicked();
            }
        }
    }
}