using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteObject : ScriptableObject
{
    public string Question;
    public string Answer1, Answer2, Answer3;
    public int Answercount1, Answercount2, Answercount3;
}
