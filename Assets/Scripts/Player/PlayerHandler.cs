using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    protected PlayerView playerView;

    protected virtual void Start()
    {
        playerView = GetComponent<PlayerView>();
    }
	
}
