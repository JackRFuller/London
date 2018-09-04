using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldView : MonoBehaviour
{
    [SerializeField]
    private PlayerView playerView;

    private ShieldMovementHandler shieldMovementHandler;


    public ShieldMovementHandler ShieldMovementHandler
    {
        get
        {
            return shieldMovementHandler;
        }
    }

    public PlayerView PlayerView
    {
        get
        {
            return playerView;
        }
    }

    private void Start()
    {
        shieldMovementHandler = GetComponent<ShieldMovementHandler>();
    }
}
