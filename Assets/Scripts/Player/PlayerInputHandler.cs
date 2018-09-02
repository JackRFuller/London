using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : PlayerHandler
{
    private bool isSprinting;

    public bool IsSprinting
    {
        get
        {
            return isSprinting;
        }

       
    }

    private void Update()
    {
        GetMovementInput();

        GetJumpInput();

        GetSprintInput();

        GetThrowShieldInput();

    }

    private void LateUpdate()
    {
        GetCameraInput();
    }

    private void GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y);
        Vector2 directionRaw = new Vector2(xRaw, yRaw);
        playerView.PlayerMovementHandler.SetMovementDirection(direction,directionRaw);
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

    private void GetThrowShieldInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerView.PlayerShieldHandler.ThrowShield();
        }
    }
}
