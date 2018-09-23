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
    private string playerName;

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
        playerName = GlobalManager.Instance.UIManager.LobbyUIManager.PlayerLaunchUIHandler.PlayerName;
        
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        if (string.IsNullOrEmpty(playerName))
            playerName = "Player" + PhotonNetwork.playerList.Length.ToString();

        PhotonNetwork.player.NickName = playerName;

        if (JoinedRoom != null)
            JoinedRoom();
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        StartCoroutine(WaitForPlayerToFullyConnectToLobby(player));
    }

    /// <summary>
    /// Buffer to make sure player's name has been set
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForPlayerToFullyConnectToLobby(PhotonPlayer player)
    {
        yield return new WaitForSeconds(1.0f);
        if (PhotonPlayerConnected != null)
            PhotonPlayerConnected();
    }




}
