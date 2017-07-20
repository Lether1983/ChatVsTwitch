using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteValueHolder : MonoBehaviour
{
    [SerializeField]
    private Text question;
    [SerializeField]
    private Text answer1;
    [SerializeField]
    private Text answer2;
    [SerializeField]
    private Text answer3;
    [SerializeField]
    private Text answerCount;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float ActiveVoteTime;

    private float timer;



    public Text AnswerCount
    {
        get { return answerCount; }
        set { answerCount = value; }
    }
    public Text Answer3
    {
        get { return answer3; }
        set { answer3 = value; }
    }
    public Text Answer2
    {
        get { return answer2; }
        set { answer2 = value; }
    }
    public Text Answer1
    {
        get { return answer1; }
        set { answer1 = value; }
    }
    public Text Question
    {
        get { return question; }
        set { question = value; }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer <= ActiveVoteTime)
        {
            slider.value = timer;
        }
    }
}
