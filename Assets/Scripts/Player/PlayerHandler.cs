using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayerHandler : Photon.MonoBehaviour
{
    protected PlayerView playerView;

    protected virtual void Start()
    {
        playerView = GetComponent<PlayerView>();
    }
	
}
