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

    protected override void Start()
    {
        base.Start();       
        playerCC = GetComponent<CharacterController>();      
    }

    public void SetMovementDirection(Vector2 inputDirection, Vector2 inputDirectionRaw)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = inputDirection.x;
        moveDirection.z = inputDirection.y;        

        moveDirection = transform.TransformDirection(moveDirection);
        Debug.Log(moveDirection);
        bool running = playerView.PlayerInputHandler.IsSprinting;        

        float targetSpeed = ((running) ? runSpeed : walkSpeed);
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothTime, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        moveDirection = moveDirection * currentSpeed + Vector3.up * velocityY;       

        float targetRotation = playerView.PlayerCameraHandler.CameraTransform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,targetRotation,transform.eulerAngles.z);

        moveDirection.y += Time.deltaTime * gravity;      
        playerCC.Move(moveDirection * Time.deltaTime);
        playerView.PlayerAnimationHandler.SetMovementAnimations(playerCC.velocity, running,inputDirection, inputDirectionRaw);


        

        //Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;       

        //playerView.PlayerAnimationHandler.SetMovementAnimation(running, inputDirection,speedSmoothTime,playerCC.velocity,runSpeed,walkSpeed);

        

        if(IsPlayerGrounded())
        {
            velocityY = 0;
        }
       
    }

    public void Jump()
    {
        if(IsPlayerGrounded())
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
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
