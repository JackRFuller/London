using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayerView : Photon.MonoBehaviour
{
    [SerializeField]
    private PlayerCameraHandler playerCameraHandler;
    private PlayerInputHandler playerInputHandler;
    private PlayerMovementHandler playerMovementHandler;
    private PlayerShieldHandler playerShieldHandler;
    private PlayerAnimationHandler playerAnimationHandler;
    private PhotonView photonView;    

    public PlayerInputHandler PlayerInputHandler
    {
        get
        {
            return playerInputHandler;
        }       
    }
    public PlayerMovementHandler PlayerMovementHandler
    {
        get
        {
            return playerMovementHandler;
        }       
    }
    public PlayerCameraHandler PlayerCameraHandler
    {
        get
        {
            return playerCameraHandler;
        }
    }
    public PlayerShieldHandler PlayerShieldHandler
    {
        get
        {
            return playerShieldHandler;
        }

        
    }
    public PlayerAnimationHandler PlayerAnimationHandler
    {
        get
        {
            return playerAnimationHandler;
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
    Quaternion realRotation;

    // Use this for initialization
    void Start ()
    {
        photonView = this.GetComponent<PhotonView>();

        playerInputHandler = this.gameObject.GetComponent<PlayerInputHandler>();
        playerMovementHandler = this.gameObject.GetComponent<PlayerMovementHandler>();
        playerAnimationHandler = this.gameObject.GetComponent<PlayerAnimationHandler>();
        playerShieldHandler = GetComponent<PlayerShieldHandler>();

        if(!photonView.isMine)
        {
            playerInputHandler.enabled = false;
            playerMovementHandler.enabled = false;
            playerAnimationHandler.enabled = false;
        }
	}

    private void Update()
    {
        if(!photonView.isMine)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, .1f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            //realRotation = (Quaternion)stream.ReceiveNext();
        }
    }



}
