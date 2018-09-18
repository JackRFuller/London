using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;

public class LobbyMenuUIHandler : UIHandler
{
    [SerializeField]
    private GameObject playerListObject;
    [SerializeField]
    private GameObject startMatchButton;

    [SerializeField]
    private TMP_Text[] playerNamesText;

    protected override void Start()
    {
        base.Start();

        globalManager.NetworkManager.JoinedRoom += ShowLobbyMenu;
        globalManager.MatchManager.StartMatchTriggered += HideLobbyMenu;
        globalManager.NetworkManager.PhotonPlayerConnected += UpdatePlayerNameList;
        

        HideLobbyMenu();
    }

    private void ShowLobbyMenu()
    {
        playerListObject.SetActive(true);

        if(PhotonNetwork.isMasterClient)
        {
            startMatchButton.SetActive(true);
        }
        else
        {
            startMatchButton.SetActive(false);
        }

        UpdatePlayerNameList();
    }

    private void HideLobbyMenu()
    {
        playerListObject.SetActive(false);
        startMatchButton.SetActive(false);
    }

    private void UpdatePlayerNameList()
    {
        int numberOfPlayersInLobby = PhotonNetwork.playerList.Length;

        for (int i = 0; i < playerNamesText.Length; i++)
        {
            if(i < numberOfPlayersInLobby)
            {
                playerNamesText[i].text = PhotonNetwork.playerList[i].NickName;
            }
            else
            {
                playerNamesText[i].text = null;
            }
        }
    }


}
