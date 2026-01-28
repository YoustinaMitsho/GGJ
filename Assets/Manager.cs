using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public string bestplayer;
    public int bestscore;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateHighestScore(string playerName, int score)
    {
        if (bestscore == 0 || bestplayer == null)
        {
            bestscore = score;
            bestplayer = playerName;
            return;
        }

        if (score > bestscore)
        {
            bestscore = score;
            bestplayer = playerName;
        }
    }
}
