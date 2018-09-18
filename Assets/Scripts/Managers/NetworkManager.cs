using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon;

public class NetworkManager : Photon.MonoBehaviour
{
    public UnityAction ConnectedToMaster;
    public UnityAction JoinedRoom;
    public UnityAction PhotonPlayerConnected;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");       
    }

    public virtual void OnConnectedToMaster()
    {        
        if(ConnectedToMaster!= null)
            ConnectedToMaster();
    }

    /// <summary>
    /// Triggered by lobby button
    /// </summary>
    public virtual void JoinLobby()
    {
        string playerName = GlobalManager.Instance.UIManager.LobbyUIManager.PlayerLaunchUIHandler.PlayerName;

        if (string.IsNullOrEmpty(playerName))
            playerName = "Player" + PhotonNetwork.playerList.Length.ToString();

        PhotonNetwork.player.NickName = playerName;
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        if (JoinedRoom != null)
            JoinedRoom();
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        if (PhotonPlayerConnected != null)
            PhotonPlayerConnected();

        Debug.Log(player.NickName);
        Debug.Log(PhotonNetwork.playerList.Length);
    }




}
