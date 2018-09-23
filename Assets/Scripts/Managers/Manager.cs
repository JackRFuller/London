using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    protected PhotonView photonView;
    public PhotonView PhotonView
    {
        get
        {
            return photonView;
        }
    }

    protected virtual void Start()
    {
        photonView = this.GetComponent<PhotonView>();
    }
	
}
