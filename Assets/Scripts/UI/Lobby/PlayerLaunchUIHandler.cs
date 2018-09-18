using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLaunchUIHandler : UIHandler {

    [SerializeField]
    private GameObject playerNameInputObject;
    [SerializeField]
    private GameObject joinLobbyButton;

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


    protected override void Start()
    {
        base.Start();

        globalManager.NetworkManager.ConnectedToMaster += ShowPlayerSetupUI;
        globalManager.NetworkManager.JoinedRoom += HidePlayerSetupUI;       

        HidePlayerSetupUI();
    }

    /// <summary>
    /// Triggered from Unity Action - OnConnectToMaster
    /// </summary>
    private void ShowPlayerSetupUI()
    {
        playerNameInputObject.SetActive(true);
        joinLobbyButton.SetActive(true);
    }

    private void HidePlayerSetupUI()
    {
        playerNameInputObject.SetActive(false);
        joinLobbyButton.SetActive(false);
    }

    public void SetPlayerNameOnChangeFromInputField()
    {
        playerName = inputField.text;
    }
}
