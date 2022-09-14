using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    Text[] scoreText = null;

    int maxIndex;
    private void Start()
    {
        int highestScore = GetHighestScore();

        if (GameManager.data.dataUsers.Count < scoreText.Length)
            maxIndex = GameManager.data.dataUsers.Count;
        else
            maxIndex = scoreText.Length;

        scoreText[0].text = GetName(highestScore) + " : " + highestScore.ToString();

        for(int i = 1; i < maxIndex; i++)
         {
            highestScore = GetScore(highestScore);
            scoreText[i].text = GetName(highestScore) + " : " + highestScore.ToString();
         }
    }
    int GetHighestScore()
    {
        int n;
        n = GameManager.data.dataUsers[0].score;

        for (int i = 1; i < maxIndex; i++)
        {
            if (n < GameManager.data.dataUsers[i].score)
            {
                n = GameManager.data.dataUsers[i].score;
            }
        }
        return n;
    }

    int GetScore(int highestScore)
    {
        int n = -1;

        for(int i = 0; i < maxIndex; i++)
        {
            if(GameManager.data.dataUsers[i].score < highestScore && GameManager.data.dataUsers[i].score > n)
            {
                n = GameManager.data.dataUsers[i].score;
            }
        }

        return n;
    }

    int SearchIndex(int sQ)
    {
        int i = 0;
        foreach (DataUser datas in GameManager.data.dataUsers)
        {
            if(datas.score == sQ)
            {
                return i;
            }
            i++;
        }

        return -1;
    }

    string GetName(int score)
    {

        foreach(DataUser datas in GameManager.data.dataUsers)
        {
            if(datas.score == score)
            {
                return datas.nameUser;
            }
        }
        return "";
    }
}
