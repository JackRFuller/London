using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldHandler : PlayerHandler
{
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private Transform shieldSpawnPoint;
    private ShieldView shieldView;

	public void ThrowShield()
    {
        Vector3 rayOrigin = playerView.PlayerCameraHandler.PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        
        RaycastHit hit;

        Vector3 hitPoint = Vector3.zero;

        Debug.DrawRay(rayOrigin, playerView.PlayerCameraHandler.CameraTransform.forward, Color.red, 5);
        if(Physics.Raycast(rayOrigin, playerView.PlayerCameraHandler.CameraTransform.forward, out hit, Mathf.Infinity))
        {
            hitPoint = hit.point;
        }

        GameObject shield = Instantiate(shieldPrefab, shieldSpawnPoint.position, Quaternion.identity);
        shieldView = shield.GetComponent<ShieldView>();

        shieldView.SetDirection(hitPoint);
    }
}
