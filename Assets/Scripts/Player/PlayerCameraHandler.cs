using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHandler : PlayerHandler
{
    private Camera playerCamera;
    private Transform cameraTransform { get { return playerCamera.transform; } }
    [SerializeField]
    private Transform targetTransform;

    private float yaw;
    private float pitch;

    [SerializeField]
    private float mouseSensitivity = 10;
    [SerializeField]
    private float distanceFromTarget = 2;
    [SerializeField]
    private Vector2 pitchMinMax = new Vector2(-40, 85);
    [SerializeField]
    private float rotationSmoothTime = .12f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    public Transform CameraTransform
    {
        get
        {
            return cameraTransform;
        }        
    }
    public Camera PlayerCamera
    {
        get
        {
            return playerCamera;
        }
    }

    protected override void Start()
    {
        base.Start();

        //Setup Camera
        GameObject playerCameraObj = new GameObject();
        playerCameraObj.AddComponent<Camera>();
        playerCamera = playerCameraObj.GetComponent<Camera>();
        if (!playerView.photonView.isMine)
            playerCameraObj.GetComponent<Camera>().enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    public void MoveCamera(float _yaw, float _pitch)
    {
        yaw += _yaw * mouseSensitivity;
        pitch += _pitch * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);        
        cameraTransform.eulerAngles = currentRotation;

        //Offset
        cameraTransform.position = targetTransform.position - cameraTransform.forward * distanceFromTarget;
    }
}
