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

    [SerializeField]
    private NetworkManager networkManager;
    [SerializeField]
    private UIManager uiManager;

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

	private void Awake()
	{
        instance = this;

        if(instance != this)
        {
            Destroy(gameObject);
        }
	}
	
}
