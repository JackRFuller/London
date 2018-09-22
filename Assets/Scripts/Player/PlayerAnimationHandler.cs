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

        playerView.PlayerHealthHandler.Respawn.AddListener(Respawn);
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

    public void Dead()
    {
        playerAnimController.SetBool("isReviving", false);
        playerAnimController.SetBool("isDead", true);
    }

    /// <summary>
    /// Listens for Respawn Event trigger from Health Handler
    /// </summary>
    public void Respawn()
    {
        playerAnimController.SetBool("isReviving", true);
        playerAnimController.SetBool("isDead", false);
    }

    IEnumerator EmoteCooldown()
    {
        yield return new WaitForSeconds(2.0f);
        playerAnimController.SetFloat("EmoteIndex", 0);

    }

}
