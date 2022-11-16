using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reception_verification : MonoBehaviour
{
    private Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
    private Inventory inventory = new Inventory();
    public Dialogue dialogue;
    public GameObject panelComputer;
    public bool correctPassword;
    public GameObject blockerPlane;

    // Start is called before the first frame update
    void Start()
    {
        Conditions.Add("OpenLockerL", false);
        Conditions.Add("GotTape", false);
        Conditions.Add("RepairedCable", false); // requires gotTape
        //Conditions.Add("CorrectPassword", false);
        correctPassword = false;

        dialogue.AddSentence("Voix mystérieuse", "La pièce où tout a commencé.", 4);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject lastClicked = clickableObj.getLastClicked();
        if (lastClicked != null)
        {
            //Debug.Log(lastClicked.name);
            if (lastClicked.name == "picture")
            {
                dialogue.AddSentence("Moi", "C'est le chat d'un des employés.", 4);
                dialogue.AddSentence("Moi", "Il est écrit que son nom est Dusty et qu'il appartient à Ethyll", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if(lastClicked.name== "Plate_LOD0" || lastClicked.name== "Plate_LOD1" || lastClicked.name=="Switch")
            {
                GameObject sceneLight = GameObject.Find("Room_light");

                Debug.Log(sceneLight.GetComponent<Light>().intensity);

                if (sceneLight.GetComponent<Light>().intensity == 0)
                {
                    sceneLight.GetComponent<Light>().intensity = 3.5F;
                }
                else
                {
                    sceneLight.GetComponent<Light>().intensity = 0;
                }
            }
            else if (lastClicked.name == "postIt")
            {
                dialogue.AddSentence("Moi", "Il est écrit que le mot de passe vient d'être changé.",4);
                dialogue.AddSentence("Moi", "le nouveau mot de passe est:",3);
                dialogue.AddSentence("Moi", "NOMANIMAL_anneeNaissance_couleurFavorite",5);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "menuboard")
            {
                dialogue.AddSentence("Moi", "Les noms et date de naissance des employés" + "\n" + "Ethyll: 07/04/1924", 8);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "Case_Door_R")
            {
                dialogue.AddSentence("Moi", "Il y a des post-it de couleur sur la porte.", 6);
                dialogue.AddSentence("Moi", "Betty aime le vert et Ethyll l'orange", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "angle_dog_picture")
            {
                dialogue.AddSentence("Moi", "Clairement un petit démon", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "PC_Monitor" && !Conditions["RepairedCable"])
            {
                dialogue.AddSentence("Moi", "L'ordinateur n'est pas alimenté", 4);
                dialogue.AddSentence("Moi", "Il faut que je trouve comment le réparer.");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else  if (lastClicked.name == "Case_Door_L" && !Conditions["OpenLockerL"])
            {
                Conditions["OpenLockerL"] = true;
                Transform t = GameObject.Find("Case_Door_L").transform;
                t.localEulerAngles = new Vector3(0, -90, 0);

                dialogue.AddSentence("Moi", "Qu'est-ce qu'il y a dedans?", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if ((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && Conditions["GotTape"])
            {
                Conditions["RepairedCable"] = true;
                inventory.removeFromInventory("Tape");
                Transform t = GameObject.Find("Cardboard_box_1").transform;
                t.localPosition = new Vector3(-11F, -0.2087748F, -8F);

                dialogue.AddSentence("Moi", "Aha, l'ordinateur marche!", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if((lastClicked.name == "Cable_Cyl_1" || lastClicked.name == "Cable_Cyl_2") && !Conditions["GotTape"])
            {
                dialogue.AddSentence("Moi", "Il semble cassé, il faut que je le répare", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "PC_Monitor" && Conditions["RepairedCable"])
            {
                panelComputer.SetActive(true);
            }
            else if(lastClicked.name == "Door" && correctPassword) //n'arrive jamais ici
            {
                dialogue.AddSentence("Moi", "Je sais exactement où aller.", 4);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);

                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(lastClicked.name == "Door" && !correctPassword)
            {
                dialogue.AddSentence("Moi", "Je ne sais pas où aller après, il faut d'abord que je trouve des réponses.", 6);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }

            clickableObj.resetLastClicked();
        }
        dialogue.empty();
    }

    /// <summary>
    /// Piecks up an item and shows dialogue when put down
    /// </summary>
    /// <param name="objOnCam">GameObject of the last clicked object, available via clickableObj</param>
    public void objToInventory(GameObject objOnCam)
    {
        if (objOnCam.name == "Tape_parent" && Conditions["OpenLockerL"])
        {
            Conditions["GotTape"] = true;
            inventory.addItemToInventory(GameObject.Find("Tape"));
            GameObject.Find("Tape").SetActive(false);

            dialogue.AddSentence("Moi", "Du scotch d'électricien, pratique pour réparer les câbles électrique.", 4);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
        }
        else
        {
            dialogue.AddSentence("Moi", "Je ne pense pas que ça soit utile à prendre",4);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
        }

    }
}