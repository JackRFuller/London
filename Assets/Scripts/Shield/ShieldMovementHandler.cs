using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovementHandler : ShieldHandler
{
    private Transform startingPoint;
    private float speed;
    private Vector3 travellingDirection;
    private MovementState movementState = MovementState.Held;
    private enum MovementState
    {
        Free,
        Held,
        Returning,
        Frozen,

    }

    //Return
    [SerializeField]
    private Vector3Lerping lerpingAttributes;

    protected override void Start()
    {
        base.Start();

        GameObject startingPointObj = Instantiate(new GameObject(), this.transform.position, this.transform.rotation,this.transform.parent);
        startingPoint = shieldView.shieldHeldTransform;
        shieldView.PlayerView.PlayerHealthHandler.Respawn.AddListener(SetShieldBackToPlayerUponRespawn);
    }

    public void ReleaseShield(Vector3 targetDirection, float travellingSpeed)
    {
        speed = travellingSpeed;
        travellingDirection = targetDirection - this.transform.position;
        travellingDirection.Normalize();

        this.transform.parent = null;
        this.transform.eulerAngles = Vector3.zero;

        movementState = MovementState.Free;

        Debug.Log("Shield Speed " + speed);
    }

    public void StartReturningProcess()
    {
        if(movementState == MovementState.Free)
        {
            movementState = MovementState.Frozen;
            StartCoroutine(InitReturn());
        }
    }

    private IEnumerator InitReturn()
    {
        shieldView.ShieldRB.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        lerpingAttributes.startPosition = this.transform.position;
        lerpingAttributes.targetPosition = startingPoint.position;

        lerpingAttributes.timeStarted = Time.time;
        movementState = MovementState.Returning;
    }

    private void FixedUpdate()
    {
        
        switch (movementState)
        {
            case MovementState.Free:
                Move();
                break;
            case MovementState.Returning:
                Return();
                break;
            case MovementState.Held:
                SetHeldPosition();
                break;
        }
        
    }

    private void SetHeldPosition()
    {
        transform.position = shieldView.shieldHeldTransform.position;
        transform.rotation = shieldView.shieldHeldTransform.rotation;
    }

    private void Move()
    {
        shieldView.ShieldRB.velocity = (travellingDirection * speed);

        Ray ray = new Ray(transform.position, travellingDirection);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.green,1);

        if(Physics.Raycast(ray,out hit, speed * Time.deltaTime + .1f))
        {
            if(hit.collider.tag != "Player")
            {
                Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                reflectDir.Normalize();
                travellingDirection = reflectDir;
            }
        }
    }

    private void Return()
    {
        float lerpingProgress = lerpingAttributes.GetLerpingProgress(lerpingAttributes.timeStarted, lerpingAttributes.lerpSpeed);
        Vector3 newPosition = Vector3.Lerp(lerpingAttributes.startPosition, startingPoint.position, lerpingAttributes.animationCurve.Evaluate(lerpingProgress));

        this.transform.position = newPosition;

        if(lerpingProgress > 0.9f)
        {
            this.transform.rotation = startingPoint.rotation;
        }

        if(lerpingProgress >= 1.0f)
        {
            shieldView.PlayerView.PlayerShieldHandler.SetShieldStateToHeld();
            movementState = MovementState.Held;
        }
    }

    /// <summary>
    /// Triggered by Respawn event from Player's Health Handler
    /// </summary>
    private void SetShieldBackToPlayerUponRespawn()
    {
        this.transform.position = startingPoint.position;
        this.transform.rotation = startingPoint.rotation;
        shieldView.PlayerView.PlayerShieldHandler.SetShieldStateToHeld();
        movementState = MovementState.Held;
    }

   

    
}
