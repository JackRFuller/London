using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshHandler : PlayerHandler
{
    [SerializeField]
    private SkinnedMeshRenderer jointMesh;
    [SerializeField]
    private SkinnedMeshRenderer surfaceMesh;

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        int id = 0;

        PhotonView tempPhotonView = GetComponent<PhotonView>();            

        if(tempPhotonView.isMine)
        {
            id = PhotonNetwork.player.ID;
        }
        else
        {id = tempPhotonView.ownerId;
            
        }

        if (GlobalManager.Instance != null)
        {
            Material jointMaterial = GlobalManager.Instance.MatchManager.PlayerJointMaterials[id];
            Material surfaceMaterial = GlobalManager.Instance.MatchManager.PlayerSurfaceMaterials[id];

            Material[] jointMats = new Material[] { jointMaterial };
            jointMesh.materials = jointMats;

            Material[] surfaceMats = new Material[] { surfaceMaterial };
            surfaceMesh.materials = surfaceMats;
        }

        ;
    }

}
