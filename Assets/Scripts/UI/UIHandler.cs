using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    protected GlobalManager globalManager;

    protected virtual void Start()
    {
        globalManager = GlobalManager.Instance;
    }
}
	
