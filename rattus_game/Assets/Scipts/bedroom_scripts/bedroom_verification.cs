using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bedroom_verification : MonoBehaviour
{
    public Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
    private Inventory inventory = new Inventory();
    public Dialogue dialogue;
    public GameObject blockerPlane;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sceneLight = GameObject.Find("Spot Light");

        sceneLight.GetComponent<Light>().spotAngle = 126;
        sceneLight.GetComponent<Light>().range = 36;
        //sceneLight.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Mixed;
        sceneLight.GetComponent<Light>().color = new Color(0xFF, 0xD2, 0x8F);
        sceneLight.GetComponent<Light>().intensity = 0.025F;
        sceneLight.GetComponent<Light>().shadowStrength = 1;
        sceneLight.GetComponent<Light>().shadowBias = 0.011F;
        sceneLight.GetComponent<Light>().shadowNormalBias = 0.4F;
        sceneLight.GetComponent<Light>().shadowNearPlane = 0.2F;

        Conditions.Add("GotKeyBed", false);
        Conditions.Add("GotDoorknob", false); // requires keybed
        Conditions.Add("OpenMedRack", false);
        Conditions.Add("OpenBedside", false);
        Conditions.Add("GotKeyBedside", false); // requires gotdoorknob

        dialogue.AddSentence("Moi", "Aïe, qu'est-ce que j'ai mal à la tête, où suis-je ?", 4);
        dialogue.AddSentence("Moi", "Il semble que j'ai été capturé, j'ai besoin de trouver la sortie.", 4); 
        dialogue.AddSentence("Voix mystérieuse", "Bonjour John, comme vous pouvez le voir, je vous retiens ici dans cette petite pièce dont vous ne pourrez jamais partir.", 7);
        dialogue.AddSentence("Voix mystérieuse", "Vous comprendrez pourquoi je vous ai enfermé ici en temps voulu, en attendant je vous laisse cogiter dans cette pièce.", 7);

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);

        dialogue.empty();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject lastClicked = clickableObj.getLastClicked();

        if (lastClicked != null)
        {
            if (lastClicked.name == "Door" && !Conditions["GotKeyBedside"])
            {
                dialogue.AddSentence("Moi", "C'est fermé. \nJ'ai besoin de trouvé la clé.", 3);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "BedBedding")
            {
                dialogue.AddSentence("Moi", "Il semble y avoir un tube à pilule sur l'oreillé, on dirait qu'il y a quelque chose dedans");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "picture")
            {
                dialogue.AddSentence("Moi", "C'est juste un chat, il n'est pas utile pour ma fuite");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "Socket_03")
            {
                GameObject sceneLight = GameObject.Find("Spot Light");

                if (sceneLight.GetComponent<Light>().intensity == 0)
                {
                    sceneLight.GetComponent<Light>().intensity = 0.025F;
                }
                else
                {
                    sceneLight.GetComponent<Light>().intensity = 0;
                }
            }
            else if (lastClicked.name == "SofaPillow" || lastClicked.name == "SofaMain")
            {
                dialogue.AddSentence("Moi", "Ce sofa semble très confortable mais ce n'est pas le moment de me reposer", 5);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if ((lastClicked.name == "MedRackKnob" || lastClicked.name == "MedRackKnobDoor_L") && !Conditions["GotKeyBed"])
            {
                dialogue.AddSentence("Moi", "Fermé! Il doit y avoir une clé quelque part ", 4);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if ((lastClicked.name == "MedRackKnob" ||
                       lastClicked.name == "MedRackKnobDoor_L") &&
                       Conditions["GotKeyBed"] &&
                       !Conditions["GotDoorknob"])
            {
                Conditions["OpenMedRack"] = true;
                Transform t = GameObject.Find("MedRackKnobDoor_L").transform;
                t.localEulerAngles = new Vector3(-180, 90, 0);

                dialogue.AddSentence("Moi", "Il y a quelque chose à l'intérieur", 2);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if ((lastClicked.name == "MirrorShelf_Case" ||
                      lastClicked.name == "MirrorShelf_DoorL") &&
                      Conditions["GotDoorknob"] &&
                      !Conditions["GotKeyBedside"])
            {
                Conditions["OpenBedside"] = true;
                Transform t = GameObject.Find("MirrorShelf_DoorL").transform;
                t.localEulerAngles = new Vector3(0, 90, 0);
                t.localScale = new Vector3(1, 1, 1);

                dialogue.AddSentence("Moi", "Peut-être qu'il y a quelque chose derrière le lapin?", 4);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }
            else if (lastClicked.name == "Door" && Conditions["GotKeyBedside"])
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (lastClicked.name == "RABBIT" && Conditions["OpenBedside"])
            {
                dialogue.AddSentence("Moi", "Ce lapin me semble familier, comme une impression de déjà vu, mais je n'arrive pas m'en souvenir.", 5);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, blockerPlane);
            }

            clickableObj.resetLastClicked();
        }
        dialogue.empty();
    }

    /// <summary>
    /// Function that finds the dialogue manager and calls it. Doesn't really work.
    /// </summary>
    private void showDialogue()
    {
        blockerPlane.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        blockerPlane.SetActive(false);
        dialogue.empty();
    }

    /// <summary>
    /// Piecks up an item and shows dialogue when put down
    /// </summary>
    /// <param name="objOnCam">GameObject of the last clicked object, available via clickableObj</param>
    public void objToInventory(GameObject objOnCam)
    {
        if (objOnCam.name == "pills")
        {
            inventory.addItemToInventory(GameObject.Find("Key"));
            Conditions["GotKeyBed"] = true;
            GameObject.Find("pillBottle").SetActive(false);

            dialogue.AddSentence("Moi", "Ce tube contient une clé", 2);
            dialogue.AddSentence("Moi", "Elle doit bien ouvrir quelque chose.", 3);
            blockerPlane.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            blockerPlane.SetActive(false);
        }
        else if (objOnCam.name == "knob_door" && Conditions["OpenMedRack"])
        {
            Conditions["GotDoorknob"] = true;
            inventory.addItemToInventory(GameObject.Find("door_knob"));
            GameObject.Find("door_knob").SetActive(false);

            dialogue.AddSentence("Moi", "Cette poignée semble aller avec la table de nuit là-bas.\nPeut-être qu'elle contient quelque chose d'utile.", 6);
            blockerPlane.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            blockerPlane.SetActive(false);

        }
        else if (objOnCam.name == "Rusty_Key" && Conditions["OpenBedside"])
        {
            Conditions["GotKeyBedside"] = true;
            GameObject.Find("rust_key").SetActive(false);

            dialogue.AddSentence("Moi", "Une vieille clé, elle doit ouvrir la porte.");
            dialogue.AddSentence("Voix mystérieuse", "Je voie que vous avez trouvé la clé, mais ce n'est que le début de la partie!\nMwahahahaha", 6);
            blockerPlane.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            blockerPlane.SetActive(false);
        }
        else
        {
            dialogue.AddSentence("Moi", "Je ne pense pas que ça soit utile.", 3);
            blockerPlane.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            blockerPlane.SetActive(false);
        }
    }
}
