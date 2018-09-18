using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldHandler : PlayerHandler
{
    [SerializeField]
    private Transform shieldSpawnPoint;    
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
        shieldSpeed += 1 * Time.deltaTime;
        if (shieldSpeed > shieldSpeedMax)
            shieldSpeed = 10;
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

            shieldView.ShieldMovementHandler.ReleaseShield(hit.point);
            shieldEquippedState = ShieldState.Free;
        }
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
