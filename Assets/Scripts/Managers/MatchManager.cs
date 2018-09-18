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

        int playerPos = PhotonNetwork.player.ID;
        Debug.Log(playerPos);

        PhotonNetwork.Instantiate("ShieldThrower", playerSpawnPoints[playerPos].position, Quaternion.identity, 0);
    }
}
