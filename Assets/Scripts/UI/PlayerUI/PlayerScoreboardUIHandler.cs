using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreboardUIHandler : UIHandler
{
    [Header("Scoreboard Elements")]
    [SerializeField]
    private TMP_Text[] playerScoresText;
    [SerializeField]
    private TMP_Text[] playerNamesText;

    private MatchManager matchManager;

    protected override void Start()
    {
        base.Start();

        matchManager = globalManager.MatchManager;

        SetPlayerNames();

        matchManager.SortedPlayerScoreList += SetPlayerNames;
    }

    private void SetPlayerNames()
    {
        for(int i =0; i < playerNamesText.Length; i++)
        {
            if (i < matchManager.PlayerScoresList.Count)
            {
                playerNamesText[i].text = matchManager.PlayerScoresList[i].playerName;
                playerScoresText[i].text = matchManager.PlayerScoresList[i].playerScore.ToString();
            }
            else
            {
                playerNamesText[i].text = "";
                playerScoresText[i].text = "";
            }
        }
    }
}
