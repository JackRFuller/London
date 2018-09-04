using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldHandler : PlayerHandler
{    
    [SerializeField]
    private ShieldView shieldView;
  
    private float shieldSpeed;

    private float shieldSpeedMin = 4;
    private float shieldSpeedMax = 10;

    private ShieldState shieldEquippedState = ShieldState.Held;
    public enum ShieldState
    {
        Held,
        Free,
        Returning,
    }

    public ShieldState ShieldEquippedState
    {
        get
        {
            return shieldEquippedState;
        }
    }

    protected override void Start()
    {
        base.Start();

        shieldSpeed = shieldSpeedMin;
    }

    public void ChargeShieldThrow()
    {
        shieldSpeed += 1 * Time.deltaTime;
        if (shieldSpeed > shieldSpeedMax)
            shieldSpeed = 10;

        //Debug.Log(shieldSpeed);
    }

    public void ReleaseShield()
    {
        Vector3 rayOrigin = playerView.PlayerCameraHandler.PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 hitPoint = Vector3.zero;

        Debug.DrawRay(rayOrigin, playerView.PlayerCameraHandler.CameraTransform.forward, Color.red, 5);
        if (Physics.Raycast(rayOrigin, playerView.PlayerCameraHandler.CameraTransform.forward, out hit, Mathf.Infinity))
        {
            hitPoint = hit.point;
        }

        shieldView.ShieldMovementHandler.ReleaseShield(hit.point);
        shieldEquippedState = ShieldState.Free;
    }
    
    public void SummonShield()
    {
        shieldView.ShieldMovementHandler.StartReturningProcess();
    }

    public void SetShieldStateToHeld()
    {
        shieldEquippedState = ShieldState.Held;
    }
}
