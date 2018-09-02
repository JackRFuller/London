using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private PlayerCameraHandler playerCameraHandler;
    private PlayerInputHandler playerInputHandler;
    private PlayerMovementHandler playerMovementHandler;
    private PlayerShieldHandler playerShieldHandler;
    private PlayerAnimationHandler playerAnimationHandler;
    

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

    // Use this for initialization
    void Start ()
    {
        playerInputHandler = this.gameObject.AddComponent<PlayerInputHandler>();
        playerMovementHandler = this.gameObject.AddComponent<PlayerMovementHandler>();
        playerAnimationHandler = this.gameObject.AddComponent<PlayerAnimationHandler>();
        playerShieldHandler = GetComponent<PlayerShieldHandler>();
	}
	
	
}
