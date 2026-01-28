using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public TMP_Text myplayer;
    public TMP_Text MyfirstBestPlayer;
    public TMP_Text currentscore;
    public TMP_Text best_score;

    int currentScore_int;
    int best_score_int;
    private void Start()
    {
      


        myplayer.text = "Player: " + PlayerPrefs.GetString("PlayerName", "Guest");
        MyfirstBestPlayer.text = "Current Best: " + PlayerPrefs.GetString("PlayerName", "Guest");
        
        
    }



    // Update is called once per frame
    void Update()
    {
        currentScore_int = int.Parse(currentscore.text);
        best_score_int = int.Parse(best_score.text);

        if (currentScore_int > best_score_int) {

            best_score_int = currentScore_int;
            best_score.text = best_score_int.ToString();
            MyfirstBestPlayer.text = "Current Best: " + myplayer.text;
        }
    }


}
