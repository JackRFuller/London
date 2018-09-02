using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldView : MonoBehaviour
{
    Vector3 direction;

    public void SetDirection(Vector3 targetPoint)
    {
        direction = targetPoint - this.transform.position;
        direction.Normalize();
    }

    private void Update()
    {
        transform.Translate(direction * 20 * Time.deltaTime);
    }
}
