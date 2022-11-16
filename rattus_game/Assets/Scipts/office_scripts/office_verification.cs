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
        public GameObject blockerPlane;

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
                    dialogue.AddSentence("Moi", "Une lettre avec mon nom dessus.");
                    dialogue.AddSentence("Moi", "Il est écrit:", 2);
                    dialogue.AddSentence("Lettre", "Bonjour John,\nJe m'appelle Fred Taylor,", 4);
                    dialogue.AddSentence("Lettre", "le mari Josephine Taylor.", 4);
                    dialogue.AddSentence("Lettre", "C'était une patiente ici, il y a des années,", 5);
                    dialogue.AddSentence("Lettre", "pendant l'époque où vous étiez le chef médecin de cet hôpital", 5);
                    dialogue.AddSentence("Moi", "C'est donc pour ça que je connais toutes ces choses.", 4);
                    dialogue.AddSentence("Lettre", "Vous avez accepté un nouveau type d'opération sur elle,", 6);
                    dialogue.AddSentence("Lettre", "vous avez dit que ça aiderait,", 4);
                    dialogue.AddSentence("Lettre", "qu'il n'y avait que quelque risque d'échecs.", 7);
                    dialogue.AddSentence("Lettre", "Ce que vous avez omis de dire, était le nom de cette opération,", 8);
                    dialogue.AddSentence("Lettre", "que j'ai par la suite appris par l'une des infirmières,", 6);
                    dialogue.AddSentence("Lettre", "le nom était une lobotomie transcrânienne.", 6);
                    dialogue.AddSentence("Lettre", "Mais ce n'était pas la seule opération que vous avez fait ce jour-là", 6);
                    dialogue.AddSentence("Lettre", "n'est-ce pas ?");
                    dialogue.AddSentence("Lettre", "Vous avez aussi fait une hemispherectomie,", 6);
                    dialogue.AddSentence("Lettre", "contre notre gré.", 4);
                    dialogue.AddSentence("Lettre", "Donc le peu qu'il restait d'elle après la lobotomie", 7);
                    dialogue.AddSentence("Lettre", "a été retiré avec la moitié de son cerveau.", 6);
                    dialogue.AddSentence("Lettre", "Elle est devenue un \"zombie\" comme ils disent dans les films.", 7);
                    dialogue.AddSentence("Lettre", "Donc j'ai contacté des gens.", 5);
                    dialogue.AddSentence("Lettre", "Payer une certaine somme, et je l'ai ramené.", 6);
                    dialogue.AddSentence("", "*Bruit de porte*\n*Bruit de pas*", 2);
                    dialogue.AddSentence("Lettre", "Et elle veut prendre sa revanche.", 5);
                    dialogue.AddSentence("", "...", 4);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
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