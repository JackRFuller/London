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

	private void Awake()
	{
        instance = this;

        if(instance != this)
        {
            Destroy(gameObject);
        }
	}
	
}
