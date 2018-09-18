using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestRange_SpawnInPlayer : Photon.MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public virtual void OnConnectedToMaster()
    {
        JoinLobby();
    }

    public virtual void JoinLobby()
    {        
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("ShieldThrower", new Vector3(0, 2.3f, 0), Quaternion.identity, 0);
    }
}
