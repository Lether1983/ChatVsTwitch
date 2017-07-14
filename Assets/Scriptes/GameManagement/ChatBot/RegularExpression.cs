using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class RegularExpression : MonoBehaviour
{
    static Regex fieldRegex = new Regex("([A-Za-z][0-9]{1,2})");
    static Regex rowRegex = new Regex("([A-Za-z])");
    static Regex coloumRegex = new Regex("([0-9]{1,2})");
    static Regex voteRegex = new Regex("([A-Za-z]{1,7})");


    Dictionary<char, int> myrow = new Dictionary<char, int>();
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    SpawnManager sManager;

    void Start()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
        sManager = this.gameObject.GetComponent<SpawnManager>();
        AddToDict();
    }

    void AddToDict()
    {
        myrow.Add('A', 0); myrow.Add('B', 1); myrow.Add('C', 2); myrow.Add('D', 3); myrow.Add('E', 4); myrow.Add('F', 5); myrow.Add('G', 6); myrow.Add('H', 7); myrow.Add('I', 8); myrow.Add('J', 9);
        myrow.Add('K', 10); myrow.Add('L', 11); myrow.Add('M', 12); myrow.Add('N', 13); myrow.Add('O', 14); myrow.Add('P', 15); myrow.Add('Q', 16); myrow.Add('R', 17); myrow.Add('S', 18);
        myrow.Add('T', 19); myrow.Add('U', 20); myrow.Add('V', 21); myrow.Add('W', 22); myrow.Add('X', 23); myrow.Add('Y', 24); myrow.Add('Z', 25);

    }

    void Update()
    {
        if (gameManager.joinedTheChat)
        {
            if (!string.IsNullOrEmpty(gameManager.message))
            {

                var matches = fieldRegex.Match(gameManager.message);
                int rowint = 0;
                int coloumint = 0;
                if (!string.IsNullOrEmpty(matches.Value))
                {
                    var row = rowRegex.Match(matches.Value);
                    var coloum = coloumRegex.Match(matches.Value);

                    rowint = row.Value.ToUpper()[0] - 'A';
                    coloumint = int.Parse(coloum.Value);
                    //Spawn the Incoming Fire
                    sManager.SpawnIncomeingFire(rowint, coloumint);
                }
            }
            else
            {
                var vote = voteRegex.Match(gameManager.message);

                if (vote.Value.ToLower() == gameManager.activeVote.Answer1)
                {
                    gameManager.activeVote.Answercount1++;
                    gameManager.activeVote.Totalcount++;
                }
                else if (vote.Value.ToLower() == gameManager.activeVote.Answer2)
                {
                    gameManager.activeVote.Answercount2++;
                    gameManager.activeVote.Totalcount++;
                }
                else if (vote.Value.ToLower() == gameManager.activeVote.Answer3)
                {
                    gameManager.activeVote.Answercount3++;
                    gameManager.activeVote.Totalcount++;
                }
            }
        }
    }
}
