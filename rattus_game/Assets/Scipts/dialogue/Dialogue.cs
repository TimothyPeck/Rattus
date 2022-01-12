using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{ 
    public List<string> sentences;
    public List<string> names;
    public List<int> times;

    public void AddSentence(string name, string sentence)
    {
        this.names.Add(name);
        this.sentences.Add(sentence);
        this.times.Add(5);
    }

    public void AddSentence(string name, string sentence, int time)
    {
        this.names.Add(name);
        this.sentences.Add(sentence);
        this.times.Add(time);
    }

    internal void empty()
    {
        sentences.Clear();
        names.Clear();
    }
}
