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
        if (MyfirstBestPlayer.text == "New Text") { MyfirstBestPlayer.text = "Current Best: " + PlayerPrefs.GetString("PlayerName", "Guest"); }

    }



    // Update is called once per frame
    void Update()
    {
        if (int.TryParse(currentscore.text, out currentScore_int) &&
        int.TryParse(best_score.text, out best_score_int))
        {
            if (currentScore_int > best_score_int)
            {
                best_score_int = currentScore_int;
                best_score.text = best_score_int.ToString();

                // This updates the UI to show the current player is now the best
                MyfirstBestPlayer.text = "Current Best: " + PlayerPrefs.GetString("PlayerName", "Guest");
            }
        }
    }


}
