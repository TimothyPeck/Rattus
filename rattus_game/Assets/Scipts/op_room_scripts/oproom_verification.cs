using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class oproom_verification : MonoBehaviour
    {
        private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
        private Inventory inventory = new Inventory();
        public Dialogue dialogue;
        public GameObject blockerPlane;

        // Start is called before the first frame update
        void Start()
        {

            Conditions.Add("GotOpReport", false);
            Conditions.Add("GotSaw", false);
            Conditions.Add("CaseOpen", false);
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

            dialogue.AddSentence("Voix mystérieuse", "L'heure est venue du challenge final.", 5);
            dialogue.AddSentence("Voix mystérieuse", "La salle d'opération des horreurs et de la souffrance.", 6);
            dialogue.AddSentence("Voix mystérieuse", "C'est ici que les opérations les plus barbares ont eu lieux.", 9);
            dialogue.AddSentence("Voix mystérieuse", "Regardez tous ces instruments de lobotomie. Magnifique.", 9);
            dialogue.AddSentence("Moi", "Mon ravisseur pense que tout ceci est drôle", 6);
            dialogue.AddSentence("Moi", "comme si tout ça n'était qu'un jeu.", 6);
            dialogue.AddSentence("Moi", "Même si c'en est un, je dois toujours m'échapper.", 4);
            dialogue.AddSentence("Moi", "Si seulement il y avait de la lumière.", 4);
        
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            dialogue.empty();
        }

        // Update is called once per frame
        void Update()
        {
            GameObject lastClicked = clickableObj.getLastClicked();
            if (lastClicked != null)
            {
                //Debug.Log(lastClicked.name);

                if (lastClicked.name == "MedRackDoor_L" && !Conditions["OpenedMedRack"] && Conditions["ChargedSaw"])
                {
                    Conditions["OpenedMedRack"] = true;
                    Transform t = GameObject.Find("MedRackDoor_L").transform;
                    t.localEulerAngles = new Vector3(-180, 60, 0);

                    dialogue.AddSentence("Moi", "Je peux finalement atteindre ces notes.", 4);
                    dialogue.AddSentence("Voix mystérieuse", "Vous allez enfin comprendre pourquoi je vous ai capturé,", 10);
                    dialogue.AddSentence("Voix mystérieuse", "pourquoi vous avez dû souffrir,", 8);
                    dialogue.AddSentence("Voix mystérieuse", "pourquoi j'ai eu besoin de vous voir apeuré comme ELLE l'était.", 10);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
                }
                else if ((lastClicked.name == "CaseMetallic" || lastClicked.name == "Case_Door_R") && !Conditions["CaseOpen"])
                {
                    Conditions["CaseOpen"] = true;
                    Transform t = GameObject.Find("Case_Door_R").transform;
                    t.localEulerAngles = new Vector3(0, -120, 0);

                    dialogue.AddSentence("Moi", "Quelque chose clignote à l'intérieur", 2);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
                }
                else if (Conditions["GotBattery"] && Conditions["GotSaw"] && !Conditions["ChargedSaw"])
                {
                    Conditions["ChargedSaw"] = true;

                    dialogue.AddSentence("Moi", "Maintenant, je peux ouvrir cette armoire.", 3);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
                }
                else  if (Conditions["GotOpReport"] && (lastClicked.name.Contains("DoorD_V2")))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if(!Conditions["GotOpReport"] && (lastClicked.name.Contains("DoorD_V2")))
                {
                dialogue.AddSentence("Moi", "Je dois découvrir ce que mon ravisseur attend de moi.", 4);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
                }            
                else if(lastClicked.name == "MedRack" && !Conditions["ChargedSaw"])
                {
                    dialogue.AddSentence("Moi", "Il y a une chaine qui ferme la porte et il n'y a pas de serrure,", 5);
                    dialogue.AddSentence("Moi", "J'ai besoin de casser cette chaîne, une scie électrique ferais l'affaire.", 4);
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
                }
                if (lastClicked.name == "Fuse_Box_01" && !Conditions["LightOn"])
                {
                    dialogue.AddSentence("Moi", "Il manque les fusibles, ils doivent être quelque part ici");
                    dialogue.AddSentence("Moi", "Il y a 3 emplacements vides");
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
                }
            if (lastClicked.name == "Fuse_Box_01")
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
            if (Conditions["ReplacedFuseFloor"] && Conditions["ReplacedFuseCabinet"] && Conditions["ReplacedFuseTable"] && !Conditions["LightOn"])
            {
                Conditions["LightOn"] = true;
                GameObject.Find("RoomLight").GetComponent<Light>().intensity = 3;

                dialogue.AddSentence("Moi", "Et la lumière fut ! Wow cette pièce est immonde.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            if (lastClicked.name == "OpReport" && Conditions["OpenedMedRack"] && Conditions["LightOn"])
            {
                Conditions["GotOpReport"] = true;
                inventory.addItemToInventory(GameObject.Find("Clipboard"));
                GameObject.Find("Clipboard").SetActive(false);

                dialogue.AddSentence("Moi", "C'est un rapport d'opération, d'une certaine Joséphine Taylor.", 7);
                dialogue.AddSentence("Moi", "Date de naissance: 21/06/1953", 7);
                dialogue.AddSentence("Voix mystérieuse", "Elle était tout pour moi.", 6);
                dialogue.AddSentence("Moi", "L'opàration était une Hémisphérectomie.", 7);
                dialogue.AddSentence("Moi", "C'est une opération qui consiste a enlevé ou déconnecté une moitié du cerveau.", 7);
                dialogue.AddSentence("Moi", "Elle est souvent pratiquée pour traiter le syndrome de Rasmussen ou alléger les crise d'épilepsie.", 8);
                dialogue.AddSentence("Moi", "Attends, comment je sais tout ça moi?", 4);
                dialogue.AddSentence("Voix mystérieuse", "Elle n'a plus jamais été pareil,", 5);
                dialogue.AddSentence("Voix mystérieuse", "elle n'était plus que l'ombre d'elle-même", 5);
                dialogue.AddSentence("Moi", "Le docteur a écrit que l'opération était un succès,", 6);
                dialogue.AddSentence("Moi", "aucune note d'une quelconque complication.", 6);
                dialogue.AddSentence("Moi", "Par contre, il a spécifié que ça lui a pris 2 fois plus de temps que d'habitude,", 4);
                dialogue.AddSentence("Moi", "à cause d'un manque de personnel.", 4);
                dialogue.AddSentence("Voix mystérieuse", "Vous voyez John, vous avez laissé vos docteurs se déchaîner dans ton hopital,", 6);
                dialogue.AddSentence("Voix mystérieuse", "détruire des vies et des familles.", 6);
                dialogue.AddSentence("Voix mystérieuse", "Elle n'était pas juste un simple cobaye pour vos expérimentations,", 8);
                dialogue.AddSentence("Voix mystérieuse", "c'était MA Joséphine, vous allez payer pour ce que vous avez fait.", 8);
                dialogue.AddSentence("Moi", "Mon ravisseur est donc le mari de Joséphine,", 6);
                dialogue.AddSentence("Moi", "Il semble m'en vouloir pour ce qui est arrivé à sa femme dans cet hôpital.", 6);
                dialogue.AddSentence("Mr. Taylor", "...", 2);
                dialogue.AddSentence("Mr. Taylor", "Nous nous rencontrerons bientôt John");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if ((lastClicked.name == "OpReport") && Conditions["OpenedMedRack"] && !Conditions["LightOn"])
            {
                dialogue.AddSentence("Moi", "Il fait trop sombre, je n'arrive pas à lire");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
        }
            clickableObj.resetLastClicked();
            dialogue.empty();
        }

    /// <summary>
    /// Piecks up an item and shows dialogue when put down
    /// </summary>
    /// <param name="objOnCam">GameObject of the last clicked object, available via clickableObj</param>
    public void objToInventory(GameObject objOnCam)
        {  

            if (objOnCam.name == "chainsaw")
            {
            Conditions["GotSaw"] = true;
            inventory.addItemToInventory(GameObject.Find("Chainsaw01"));
            GameObject.Find("Chainsaw01").SetActive(false);

            dialogue.AddSentence("Moi", "Elle sera sûrement utile,", 4);
            dialogue.AddSentence("Moi", "elle est déchargée, il faut que je trouve une batterie.", 4);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (objOnCam.name == "BatteryContainer" && Conditions["CaseOpen"])
            {
            Conditions["GotBattery"] = true;
            inventory.addItemToInventory(GameObject.Find("Battery"));
            GameObject.Find("Battery").SetActive(false);
            GameObject.Find("BatteryLight").SetActive(false);

            dialogue.AddSentence("Moi", "Une batterie. \nElle a l'air de rentrée dans la scie électrique là-bas.");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (objOnCam.name == "fusePurple")
            {
            Conditions["GotFuseTable"] = true;
            inventory.addItemToInventory(GameObject.Find("fuseTable"));
            GameObject.Find("fuseTable").SetActive(false);

            dialogue.AddSentence("Moi", "Il me servira pour la lumière.", 3);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (objOnCam.name == "fuseRed")
            {
            Conditions["GotFuseFloor"] = true;
            inventory.addItemToInventory(GameObject.Find("fuseFloor"));
            GameObject.Find("fuseFloor").SetActive(false);

            dialogue.AddSentence("Moi", "La taille parfaite.", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (objOnCam.name == "fuseYellow")
            {
            Conditions["GotFuseCabinet"] = true;
            inventory.addItemToInventory(GameObject.Find("fusecontainer"));
            GameObject.Find("fusecontainer").SetActive(false);

            dialogue.AddSentence("Moi", "C'est sûrement utile", 2);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
        else
            {
            dialogue.AddSentence("Moi", "Je ne penses pas que ça me servira");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }

        }
    }
