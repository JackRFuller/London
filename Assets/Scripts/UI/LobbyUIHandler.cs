using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUIHandler : UIHandler
{
    [Header("Join Lobby Menu")]
    [Header("Objects")]
    [SerializeField]
    private GameObject playerNameInputObject;
    [SerializeField]
    private GameObject joinLobbyButton;

    [Header("Values")]
    [SerializeField]
    private TMP_InputField inputField;

    private string playerName;
    public string PlayerName
    {
        get
        {
            return playerName;
        }
    }

    [Header("Lobby Menu")]
    [SerializeField]
    private TMP_Text[] playerNameTexts;

    private void Start()
    {
        HideJoinLobbyOptions();
    }

    /// <summary>
    /// Triggered when the player connects to Master Server
    /// </summary>
    public void OnConnectedToMaster()
    {
        ShowJoinLobbyOptions();
    }

    /// <summary>
    /// Triggered from Network Manager
    /// </summary>
    public void OnJoinedRoom()
    {
        HideJoinLobbyOptions();
    }

    private void ShowJoinLobbyOptions()
    {
        playerNameInputObject.SetActive(true);
        joinLobbyButton.SetActive(true);
    }

    private void HideJoinLobbyOptions()
    {
        playerNameInputObject.SetActive(false);
        joinLobbyButton.SetActive(false);
    }   

    public void SetPlayerName()
    {
        playerName = inputField.text;
    }
}
