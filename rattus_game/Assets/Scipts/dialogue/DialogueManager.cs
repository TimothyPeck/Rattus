using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text diagText;
    public Animator animator;

    private Queue<string> dialogues;
    private Queue<string> diag_names;
    private Queue<int> diag_times;


    // Start is called before the first frame update
    void Start()
    {
        dialogues = new Queue<string>();
        diag_names = new Queue<string>();
        diag_times = new Queue<int>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        
        string[] names = dialogue.names.ToArray();
        string[] sentences = dialogue.sentences.ToArray();
        int[] times = dialogue.times.ToArray();

        dialogues.Clear();

        foreach(string diag in sentences)
        {
            dialogues.Enqueue(diag);
        }

        foreach(string name in names)
        {
            diag_names.Enqueue(name);
        }

        foreach(int time in times)
        {
            diag_times.Enqueue(time);
        }

        DisplayDialogue();
    }

    public void DisplayDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(showMessageForSeconds(5));
    }

    IEnumerator showMessageForSeconds(int seconds)
    {
        string sentence = dialogues.Dequeue();
        string name = diag_names.Dequeue();
        int time = diag_times.Dequeue();
        //StartCoroutine(TypeSentence(sentence));
        nameText.text = name;
        diagText.text = sentence;
        yield return new WaitForSecondsRealtime(time);
        DisplayDialogue();
    }

    IEnumerator TypeSentence(string sentence)
    {
        diagText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            diagText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
