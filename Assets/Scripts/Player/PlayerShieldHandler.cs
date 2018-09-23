using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldHandler : PlayerHandler
{
    [SerializeField]
    private Transform shieldSpawnPoint;    
    private ShieldView shieldView;

    private float shieldCharge;
    private float shieldSpeed;

    private const float shieldChargeRate = 10;
    private float shieldSpeedMin = 15;
    private float shieldSpeedMax = 25;

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

    public ShieldView ShieldView
    {
        get
        {
            return shieldView;
        }
    }

    protected override void Start()
    {
        base.Start();

        shieldSpeed = 0;

        if(playerView.photonView.isMine)
        {
            SetupShield();
        }
    }

    private void SetupShield()
    {
        GameObject shield = PhotonNetwork.Instantiate("Shield",transform.position,Quaternion.identity,0);
        shieldView = shield.GetComponent<ShieldView>();

        shieldView.SetControllingPlayerView(playerView,shieldSpawnPoint);

        shield.name = "Shield";
    }

    public void ChargeShieldThrow()
    {
        shieldCharge += shieldChargeRate * Time.deltaTime;

        float percentageComplete = shieldCharge / shieldSpeedMax;

        playerView.PlayerUIHandler.ChargeBar(percentageComplete);
    }

    /// <summary>
    /// Triggered from Animation Event on Throw SHield
    /// </summary>
    public void ReleaseShield()
    {
        if(playerView.photonView.isMine)
        {
            Vector3 rayOrigin = playerView.PlayerCameraHandler.PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 hitPoint = Vector3.zero;

            Debug.DrawRay(rayOrigin, playerView.PlayerCameraHandler.CameraTransform.forward, Color.red, 5);
            if (Physics.Raycast(rayOrigin, playerView.PlayerCameraHandler.CameraTransform.forward, out hit, Mathf.Infinity))
            {
                hitPoint = hit.point;
            }

            float percentageComplete = shieldCharge / shieldSpeedMax;
            shieldSpeed = shieldSpeedMax * percentageComplete;

            if (percentageComplete < 0.5f)
                shieldSpeed = shieldSpeedMin;

            if (percentageComplete > 0.8f && percentageComplete <= 0.85f)
                shieldSpeed = 30;

            if (percentageComplete > 0.85f )
                shieldSpeed = shieldSpeedMax;

            shieldView.ShieldMovementHandler.ReleaseShield(hit.point,shieldSpeed);
            shieldEquippedState = ShieldState.Free;

            ResetShieldValues();
        }
    }

    private void ResetShieldValues()
    {
        shieldSpeed = 0;
        shieldCharge = 0;
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
