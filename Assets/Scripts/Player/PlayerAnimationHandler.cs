using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : PlayerHandler
{
    private Animator playerAnimController;

    protected override void Start()
    {
        base.Start();
        playerAnimController = GetComponent<Animator>();
    }

    public void SetMovementAnimation(bool running, Vector2 input, float smoothTime, Vector3 controllerVelocity, float runSpeed, float walkSpeed)
    {
        //Used for animation



        //float currentSpeed = new Vector2(controllerVelocity.x, controllerVelocity.z).magnitude;
        //float animationSpeedPercent = (running ? 1 : .5f) * input.magnitude;
        //playerAnimController.SetFloat("speedPercent", animationSpeedPercent, smoothTime, Time.deltaTime);
    }

    public void SetMovementAnimations(Vector3 controllerVelocity,bool isRunning,Vector2 inputDir,Vector2 inputDirRaw)
    {
        int movementSpeed = 0;

        if(inputDirRaw == Vector2.zero)
        {
            movementSpeed = 0;
        }
        else if(inputDirRaw.x != 0 || inputDirRaw.y != 0)
        {
            if(isRunning)
            {
                movementSpeed = 2;
            }
            else
            {
                movementSpeed = 1;
            }           
        }
        playerAnimController.SetInteger("movementSpeed", movementSpeed);
        playerAnimController.SetBool("isSprinting", isRunning);
        playerAnimController.SetFloat("directionX", inputDir.x);
        playerAnimController.SetFloat("directionY", inputDir.y);
    }

    public void Jump(bool isIdle)
    {

    }
}
