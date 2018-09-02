using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour {

    protected ShieldView shieldView;

    protected virtual void Start()
    {
        shieldView = GetComponent<ShieldView>();
    }
}
