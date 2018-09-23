using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : PlayerHandler
{
    private float inputX;
    private float inputY;
    private float inputXRaw;
    private float inputYRaw;

    private bool isSprinting;

    public bool IsSprinting
    {
        get
        {
            return isSprinting;
        }

       
    }
    public float InputX
    {
        get
        {
            return inputX;
        }        
    }
    public float InputY
    {
        get
        {
            return inputY;
        }
    }
    public float InputXRaw
    {
        get
        {
            return inputXRaw;
        }
    }
    public float InputYRaw
    {
        get
        {
            return inputYRaw;
        }
    }

    private void Update()
    {
        if(playerView.PhotonView.isMine)
        {
            GetMovementInput();

            GetJumpInput();

            GetRollInput();

            GetSprintInput();

            GetThrowShieldInput();

            GetSummonShieldInput();

            GetEmoteInput();

            GetQuitApplicationInput();
        }
    }

    private void LateUpdate()
    {
        if(playerView.photonView.isMine)
        {
            GetCameraInput();
        }
    }

    private void GetMovementInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        inputXRaw = Input.GetAxisRaw("Horizontal");
        inputYRaw = Input.GetAxisRaw("Vertical");
    }

    private void GetSprintInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
            isSprinting = false;
    }

    private void GetRollInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            playerView.PlayerMovementHandler.Roll();
        }
    }

    private void GetJumpInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            playerView.PlayerMovementHandler.Jump();
        }
    }

    private void GetCameraInput()
    {
        float yaw = Input.GetAxis("Mouse X");
        float pitch = Input.GetAxis("Mouse Y");

        playerView.PlayerCameraHandler.MoveCamera(yaw, pitch);
    }

    private void GetEmoteInput()
    {
       
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerView.PlayerAnimationHandler.PlayEmote(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerView.PlayerAnimationHandler.PlayEmote(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerView.PlayerAnimationHandler.PlayEmote(3);
        }
    }

    private void GetThrowShieldInput()
    {
        if (playerView.PlayerShieldHandler.ShieldEquippedState != PlayerShieldHandler.ShieldState.Held)
            return;

        if(Input.GetMouseButton(0))
        {
            playerView.PlayerShieldHandler.ChargeShieldThrow();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            playerView.PlayerMovementHandler.FreezeMovement();
            playerView.PlayerAnimationHandler.ThrowShield();
            playerView.PlayerUIHandler.HidePlayerChargeBar();
        }
    }

    private void GetSummonShieldInput()
    {
        if (playerView.PlayerShieldHandler.ShieldEquippedState != PlayerShieldHandler.ShieldState.Free)
            return;

        if(Input.GetMouseButtonDown(1))
        {
            playerView.PlayerShieldHandler.SummonShield();
        }
    }

    private void GetQuitApplicationInput()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
