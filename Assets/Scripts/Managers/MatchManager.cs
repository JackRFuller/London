using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon;
using System.Linq;

public class MatchManager : Manager
{  
    //Unity Actions
    public UnityAction StartMatchTriggered;
    public UnityAction SortedPlayerScoreList;

    [SerializeField]
    private Transform[] playerSpawnPoints;

    [Header("Player Colours")]
    [SerializeField]
    private Material[] playerJointMaterials;
    [SerializeField]
    private Material[] playerSurfaceMaterials;

    public Material[] PlayerJointMaterials
    {
        get
        {
            return playerJointMaterials;
        }
    }
    public Material[] PlayerSurfaceMaterials
    {
        get
        {
            return playerSurfaceMaterials;
        }       
    }



    private List<PlayerScore> playerScoreList;
    public List<PlayerScore> PlayerScoresList
    {
        get
        {
            return playerScoreList;
        }
    }

    protected override void Start()
    {
        base.Start();

        playerScoreList = new List<PlayerScore>();
    }

    public void TriggerStartOfMatchFromHost()
    {
        photonView.RPC("StartMatch", PhotonTargets.All);
    }

    [PunRPC]
    private void StartMatch()
    {
        if (StartMatchTriggered != null)
            StartMatchTriggered();

        AddPlayersToMatch();

        int playerPos = PhotonNetwork.player.ID;
        PhotonNetwork.Instantiate("ShieldThrower", playerSpawnPoints[playerPos].position, Quaternion.identity, 0);
    }

    public void AddPlayersToMatch()
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            playerScoreList.Add(new PlayerScore(PhotonNetwork.playerList[i].NickName));
        }
    }

    /// <summary>
    /// Called from Player Health Handler
    /// </summary>
    /// <param name="player"></param>
    [PunRPC]
    private void PlayerScored(string player)
    {
        //Add on Score
        for(int i = 0; i < playerScoreList.Count; i++)
        {
            if(player == playerScoreList[i].playerName)
            {
                playerScoreList[i].playerScore++;
                break;
            }
        }

        PlayerScore playerScoreTemp = new PlayerScore("temp");

        playerScoreList = playerScoreList.OrderByDescending(o => o.playerScore).ToList();

        if (SortedPlayerScoreList != null)
            SortedPlayerScoreList();
    }
}

public class PlayerScore
{
    public string playerName;
    public int playerScore;

    public PlayerScore(string _playerName)
    {
        playerName = _playerName;
        playerScore = 0;
    }
}
