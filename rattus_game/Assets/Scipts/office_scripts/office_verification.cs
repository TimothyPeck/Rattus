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
                    dialogue.AddSentence("Moi", "Il est �crit:", 2);
                    dialogue.AddSentence("Lettre", "Bonjour John,\nJe m'appelle Fred Taylor,", 4);
                    dialogue.AddSentence("Lettre", "le mari Josephine Taylor.", 4);
                    dialogue.AddSentence("Lettre", "C'�tait une patiente ici, il y a des ann�es,", 5);
                    dialogue.AddSentence("Lettre", "pendant l'�poque o� vous �tiez le chef m�decin de cet h�pital", 5);
                    dialogue.AddSentence("Moi", "C'est donc pour �a que je connais toutes ces choses.", 4);
                    dialogue.AddSentence("Lettre", "Vous avez accept� un nouveau type d'op�ration sur elle,", 6);
                    dialogue.AddSentence("Lettre", "vous avez dit que �a aiderait,", 4);
                    dialogue.AddSentence("Lettre", "qu'il n'y avait que quelque risque d'�checs.", 7);
                    dialogue.AddSentence("Lettre", "Ce que vous avez omis de dire, �tait le nom de cette op�ration,", 8);
                    dialogue.AddSentence("Lettre", "que j'ai par la suite appris par l'une des infirmi�res,", 6);
                    dialogue.AddSentence("Lettre", "le nom �tait une lobotomie transcr�nienne.", 6);
                    dialogue.AddSentence("Lettre", "Mais ce n'�tait pas la seule op�ration que vous avez fait ce jour-l�", 6);
                    dialogue.AddSentence("Lettre", "n'est-ce pas ?");
                    dialogue.AddSentence("Lettre", "Vous avez aussi fait une hemispherectomie,", 6);
                    dialogue.AddSentence("Lettre", "contre notre gr�.", 4);
                    dialogue.AddSentence("Lettre", "Donc le peu qu'il restait d'elle apr�s la lobotomie", 7);
                    dialogue.AddSentence("Lettre", "a �t� retir� avec la moiti� de son cerveau.", 6);
                    dialogue.AddSentence("Lettre", "Elle est devenue un \"zombie\" comme ils disent dans les films.", 7);
                    dialogue.AddSentence("Lettre", "Donc j'ai contact� des gens.", 5);
                    dialogue.AddSentence("Lettre", "Payer une certaine somme, et je l'ai ramen�.", 6);
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