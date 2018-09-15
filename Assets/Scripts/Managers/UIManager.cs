using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager
{
    [SerializeField]
    private LobbyUIHandler lobbyHandler;

    public LobbyUIHandler LobbyUIHandler
    {
        get
        {
            return lobbyHandler;
        }
    }

}
