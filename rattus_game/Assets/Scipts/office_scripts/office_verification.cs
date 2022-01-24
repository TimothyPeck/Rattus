using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rattus
{
    public class office_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        public Dialogue dialogue;
        public AudioSource scream;
        public GameObject btnEnd;
        public GameObject panEnd;
        public GameObject btnMenu;

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
                    dialogue.AddSentence("Me", "It reads:", 2);
                    dialogue.AddSentence("Letter", "Hello John,\nMy name is Fred Taylor,", 4);
                    dialogue.AddSentence("Letter", "husband of Josephine Taylor.", 4);
                    dialogue.AddSentence("Letter", "She was a patient here many years,", 5);
                    dialogue.AddSentence("Letter", "while you were still the Chief of Medicine", 5);
                    dialogue.AddSentence("Me", "So that's how I know those things.", 3);
                    dialogue.AddSentence("Letter", "You greenlit a new procedure on her,", 6);
                    dialogue.AddSentence("Letter", "you said it would help,", 6);
                    dialogue.AddSentence("Letter", "you said there was very little risk invovled.", 6);
                    dialogue.AddSentence("Letter", "What you never said was the name of the procedure,", 8);
                    dialogue.AddSentence("Letter", "which I later found from one of the nurses", 8);
                    dialogue.AddSentence("Letter", "who went throught the same ordeal as you,", 8);
                    dialogue.AddSentence("Letter", "was that is was a transcranial lobotomy.", 8);
                    dialogue.AddSentence("Letter", "But that wasn't the only procedure", 7);
                    dialogue.AddSentence("Letter", "you did to her that day was it,", 7);
                    dialogue.AddSentence("Letter", "you also performed a hemispherectomy,", 7);
                    dialogue.AddSentence("Letter", "against our will.", 7);
                    dialogue.AddSentence("Letter", "So whatever was left of her after the lobotomy", 4);
                    dialogue.AddSentence("Letter", "was removed with half of her brain.", 4);
                    dialogue.AddSentence("Letter", "She became a \"zombie\" as they say in the films.", 4);
                    dialogue.AddSentence("Letter", "So I contacted some people.", 4);
                    dialogue.AddSentence("Letter", "Paid some money, and brought her back.", 4);
                    dialogue.AddSentence("", "*Door sound*\n*footsteps*", 2);
                    dialogue.AddSentence("Letter", "And she wants to take her revenge.", 3);
                    dialogue.AddSentence("", "...", 4);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                    GameObject.Find("RotationAxe").SetActive(false);
                    RenderSettings.skybox.SetColor("_SkyTint", Color.black);
                    GameObject.Find("btnLeft").SetActive(false);
                    GameObject.Find("btnRight").SetActive(false);
                    btnEnd.SetActive(true);

                    if (dialogue.sentences.Count == 3)
                    {
                        scream.Play();
                    }
                }
                clickableObj.resetLastClicked();
            }
           
        }

        public void TheEnd()
        {
            panEnd.SetActive(true);
        }

        public void menu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}