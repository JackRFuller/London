﻿using System.Collections;
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
        GetMovementInput();

        GetJumpInput();

        GetRollInput();

        GetSprintInput();

        GetThrowShieldInput();

        GetEmoteInput();
    }

    private void LateUpdate()
    {
        GetCameraInput();
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
        if(Input.GetMouseButtonDown(0))
        {
            playerView.PlayerShieldHandler.ThrowShield();
        }
    }
}
