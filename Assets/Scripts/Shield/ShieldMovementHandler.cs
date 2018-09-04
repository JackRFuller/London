using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovementHandler : ShieldHandler
{
    

    private Transform startingPoint;
    private Vector3 travellingDirection;
    private MovementState movementState = MovementState.Frozen;
    private enum MovementState
    {
        Free,
        Frozen,
        Returning,
    }

    //Return
    [SerializeField]
    private Vector3Lerping lerpingAttributes;

    protected override void Start()
    {
        base.Start();

        GameObject startingPointObj = Instantiate(new GameObject(), this.transform.position, this.transform.rotation,this.transform.parent);
        startingPoint = startingPointObj.transform;
    }

    public void ReleaseShield(Vector3 targetDirection)
    {
        travellingDirection = targetDirection - this.transform.position;
        travellingDirection.Normalize();

        this.transform.parent = null;
        this.transform.eulerAngles = Vector3.zero;

        movementState = MovementState.Free;
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
        yield return new WaitForSeconds(0.1f);
        lerpingAttributes.startPosition = this.transform.position;
        lerpingAttributes.targetPosition = startingPoint.position;

        lerpingAttributes.timeStarted = Time.time;
        movementState = MovementState.Returning;
    }

    private void Update()
    {
        switch(movementState)
        {
            case MovementState.Free:
                Move();
                break;
            case MovementState.Returning:
                Return();
                break;
        }
    }

    private void Move()
    {
        transform.Translate(travellingDirection * 20 * Time.deltaTime);

        Ray ray = new Ray(transform.position, travellingDirection);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.green,1);

        if(Physics.Raycast(ray,out hit, 20 * Time.deltaTime + .1f))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            reflectDir.Normalize();
            travellingDirection = reflectDir;
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
            movementState = MovementState.Frozen;
            this.transform.parent = startingPoint.parent;
            this.transform.position = startingPoint.position;
            this.transform.rotation = startingPoint.rotation;
            shieldView.PlayerView.PlayerShieldHandler.SetShieldStateToHeld();
        }
    }

   

    
}
