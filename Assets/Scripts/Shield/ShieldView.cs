using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldView : MonoBehaviour
{
    private PlayerView playerView;
    private PhotonView photonView;
    private Rigidbody shieldRB;

    private ShieldMovementHandler shieldMovementHandler;
    public ShieldMovementHandler ShieldMovementHandler
    {
        get
        {
            return shieldMovementHandler;
        }
    }

    public Rigidbody ShieldRB
    {
        get
            {
            return shieldRB;
            }
    }
    public PlayerView PlayerView
    {
        get
        {
            return playerView;
        }
    }
    public PhotonView PhotonView
    {
        get
        {
            return photonView;
        }
    }

    //Temp
    public Transform shieldHeldTransform;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        shieldRB = GetComponent<Rigidbody>();
        shieldMovementHandler = GetComponent<ShieldMovementHandler>();

        if(!photonView.isMine)
        {
            shieldMovementHandler.enabled = false;
        }
    }

    public void SetControllingPlayerView(PlayerView _playerView, Transform _shieldHeldTransform)
    {
        playerView = _playerView;
        shieldHeldTransform = _shieldHeldTransform;
    }
}
