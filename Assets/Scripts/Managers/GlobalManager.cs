using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlobalManager : MonoBehaviour 
{
	private static GlobalManager instance;
	public static GlobalManager Instance 
	{
		get
		{
			return instance;
		}
	}

    
    private NetworkManager networkManager;    
    private UIManager uiManager;
    private MatchManager matchManager;

    public NetworkManager NetworkManager
    {
        get
        {
            return networkManager;
        }
    }
    public UIManager UIManager
    {
        get
        {
            return uiManager;
        }
    }
    public MatchManager MatchManager
    {
        get
        {
            return matchManager;
        }
    }

	private void Awake()
	{
        instance = this;

        if(instance != this)
        {
            Destroy(gameObject);
        }
	}

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        uiManager = GetComponent<UIManager>();
        matchManager = GetComponent<MatchManager>();
    }

}
