using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class NetworkManager : Photon.MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }
    


}
