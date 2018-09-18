using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon;

public class MatchManager : Photon.MonoBehaviour
{  
    private PhotonView photonView;

    //Unity Actions
    public UnityAction StartMatchTriggered;

    [SerializeField]
    private Transform[] playerSpawnPoints;

    private void Start()
    {
        photonView = this.GetComponent<PhotonView>();
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

        PhotonNetwork.Instantiate("ShieldThrower", playerSpawnPoints[0].position, Quaternion.identity, 0);
    }
}
