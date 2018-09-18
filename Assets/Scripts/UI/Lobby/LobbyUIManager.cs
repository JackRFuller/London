using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUIManager : UIHandler
{
    [SerializeField]
    private PlayerLaunchUIHandler playerLaunchMenuHandler;
    public PlayerLaunchUIHandler PlayerLaunchUIHandler
    {
        get
        {
            return playerLaunchMenuHandler;
        }
    }

}
