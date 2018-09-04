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
        float movementSpeed = 0;

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
        playerAnimController.SetFloat("movementSpeedFloat",movementSpeed);
        playerAnimController.SetInteger("movementSpeed", (int)movementSpeed);        
        playerAnimController.SetFloat("directionX", inputDir.x);
        playerAnimController.SetFloat("directionY", inputDir.y);
    }

    public void Jump()
    {
        playerAnimController.SetTrigger("Jump");
    }

    public void Roll()
    {
        playerAnimController.SetTrigger("Roll");
    }

    public void SetGroundedState(bool isGrounded)
    {
        playerAnimController.SetBool("isGrounded", isGrounded);       
    }

    public void PlayEmote(int emoteIndex)
    {
        playerAnimController.SetFloat("EmoteIndex", emoteIndex);        
    }

    public void ThrowShield()
    {
        playerAnimController.SetTrigger("ThrowShield");
    }

    IEnumerator EmoteCooldown()
    {
        yield return new WaitForSeconds(2.0f);
        playerAnimController.SetFloat("EmoteIndex", 0);

    }

}
