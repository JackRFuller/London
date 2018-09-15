using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon;

public class NetworkManager : Photon.MonoBehaviour
{
    public UnityEvent ConnectToMaster;
    public UnityEvent JoinedRoom;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");       
    }

    /// <summary>
    /// Triggered by lobby button
    /// </summary>
    public virtual void JoinLobby()
    {
        string playerName = GlobalManager.Instance.UIManager.LobbyUIHandler.PlayerName;

        if (string.IsNullOrEmpty(playerName))       
            playerName = "Player" + PhotonNetwork.playerList.Length.ToString();

        PhotonNetwork.player.NickName = playerName;
        
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log(player.NickName);
        Debug.Log(PhotonNetwork.playerList.Length);
    }




}
