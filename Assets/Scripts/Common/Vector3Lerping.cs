using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector3Lerping
{
    public float lerpSpeed;
    public AnimationCurve animationCurve;

    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Vector3 targetPosition;
    [HideInInspector]
    public float timeStarted;
    [HideInInspector]
    public bool isReturning;

    public float GetLerpingProgress(float timeStarted, float lerpSpeed)
    {
        float percentageComplete = 0;
        float timeSinceStarted = Time.time - timeStarted;
        return percentageComplete = timeSinceStarted / lerpSpeed;
    }
}
