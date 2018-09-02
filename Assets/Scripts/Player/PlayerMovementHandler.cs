using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : PlayerHandler
{
    private CharacterController playerCC;

    [Header("Movement Attributes")]
    [SerializeField]
    private float walkSpeed = 4.5f;
    [SerializeField]
    private float runSpeed = 7;    
    [SerializeField]
    private float speedSmoothTime = 0.1f;
    [SerializeField]
    private float gravity = -12;

    [Header("Jumping Attributes")]
    [SerializeField]
    private float jumpHeight = 2;
    [Range(0, 1)]
    [SerializeField]
    private float airControlPercent;

    private float speedSmoothVelocity;
    private float currentSpeed;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private float velocityY;

    private bool isRolling;

    protected override void Start()
    {
        base.Start();       
        playerCC = GetComponent<CharacterController>();      
    }

    private void Update()
    {
        SetMovementDirection();
    }

    public void SetMovementDirection()
    {
        playerView.PlayerAnimationHandler.SetGroundedState(IsPlayerGrounded());

        Vector2 inputDirection = GetPlayerInput();
        Vector2 inputDirectionRaw = GetPlayerRawInput();

        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = inputDirection.x;
        moveDirection.z = inputDirection.y;        

        moveDirection = transform.TransformDirection(moveDirection);
        bool running = playerView.PlayerInputHandler.IsSprinting;        

        float targetSpeed = ((running) ? runSpeed : walkSpeed);
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothTime, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        moveDirection = moveDirection * currentSpeed + Vector3.up * velocityY;       

        if(isRolling)
        {
            moveDirection *= 2;
        }

        float targetRotation = playerView.PlayerCameraHandler.CameraTransform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,targetRotation,transform.eulerAngles.z);

        moveDirection.y += Time.deltaTime * gravity;      
        playerCC.Move(moveDirection * Time.deltaTime);
        playerView.PlayerAnimationHandler.SetMovementAnimations(playerCC.velocity, running,inputDirection, inputDirectionRaw);

        if(IsPlayerGrounded())
        {
            velocityY = 0;            
        }
    }

    public void Jump()
    {
        if(IsPlayerGrounded())
        {
            bool running = playerView.PlayerInputHandler.IsSprinting;
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);

            if (running)
                jumpVelocity *= 1f;

            velocityY = jumpVelocity;

            playerView.PlayerAnimationHandler.Jump();
        }
    }

    public void Roll()
    {
        if(!isRolling)
        {
            isRolling = true;
            playerView.PlayerAnimationHandler.Roll();
            StartCoroutine(RollingCoolDown());
        }
            
    }

    IEnumerator RollingCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRolling = false;
    }

    
    
    private Vector2 GetPlayerInput()
    {
        return new Vector2(playerView.PlayerInputHandler.InputX, playerView.PlayerInputHandler.InputY);
    }

    private Vector2 GetPlayerRawInput()
    {
        return new Vector2(playerView.PlayerInputHandler.InputXRaw, playerView.PlayerInputHandler.InputYRaw);
    }

    private float GetModifiedSmoothTime(float smoothTime)
    {
        if(IsPlayerGrounded())
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
            return float.MaxValue;

        return smoothTime / airControlPercent;
    }

    private bool IsPlayerGrounded()
    {
        return playerCC.isGrounded;
    }

    IEnumerator ResetTrigger(string trigger)
    {
        yield return new WaitForSeconds(0.4f);       
    }
}
