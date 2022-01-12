using System.Collections.Generic;
using UnityEngine;

namespace Rattus
{
    public class office_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        public Dialogue dialogue;

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
                    dialogue.AddSentence("Me", "A letter, with my name on it.");
                    dialogue.AddSentence("Me", "It reads:", 1);
                    dialogue.AddSentence("Letter", "Hello John,\nMy name is Fred Taylor, husband of Josephine Taylor.", 4);
                    dialogue.AddSentence("Letter", "She was a patient here many years, while you were still the Chief of Medicine", 5);
                    dialogue.AddSentence("Me", "So that's how I know those things.", 3);
                    dialogue.AddSentence("Letter", "You greenlit a new procedure on her, said it would help, you said there was very little risk invovled", 6);
                    dialogue.AddSentence("Letter", "What you never said was the name of the procedure, which I later found from one of the nurses who went throught the same ordeal as you, was that is was a transcranial lobotomy.", 8);
                    dialogue.AddSentence("Letter", "But that wasn't the only procedure you did to her that day was it, you also performed a hemispherectomy, against our will.", 7);
                    dialogue.AddSentence("Letter", "So whatever was left of her after the lobotomy was removed with half of her brain.", 4);
                    dialogue.AddSentence("Letter", "She became a \"zombie\" as they say in the films.", 4);
                    dialogue.AddSentence("Letter", "So I contacted some people. Paid some money, and brought her back.", 4);
                    dialogue.AddSentence("", "*footsteps*", 2);
                    dialogue.AddSentence("Letter", "And she wants to take her revenge.", 3);
                    dialogue.AddSentence("", "...", 4);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                    GameObject.Find("RotationAxe").SetActive(false);
                    RenderSettings.skybox.SetColor("_SkyTint", Color.black);
                    GameObject.Find("btnLeft").SetActive(false);
                    GameObject.Find("btnRight").SetActive(false);
                }

                clickableObj.resetLastClicked();
            }
        }
    }
}