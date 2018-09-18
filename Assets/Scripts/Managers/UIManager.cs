﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager
{
    [SerializeField]
    private LobbyUIManager lobbyUIManager;
    public LobbyUIManager LobbyUIManager
    {
        get
        {
            return lobbyUIManager;
        }
    }

}
