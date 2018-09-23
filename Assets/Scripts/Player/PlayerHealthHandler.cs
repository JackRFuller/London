using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon;

public class PlayerHealthHandler : PlayerHandler
{
    [HideInInspector]
    public UnityEvent Respawn;

    private PlayerHealthState playerHealthState;
    public PlayerHealthState PlayerState
    {
        get
        {
            return playerHealthState;
        }
    }

    public enum PlayerHealthState
    {
        Alive,
        Dead,
        Respawning,
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Shield"))
        {
            ShieldView shieldView = other.GetComponent<ShieldView>();
           
            //Check the shield isn't mine
            if(shieldView != playerView.PlayerShieldHandler.ShieldView)
            {
                if(playerHealthState != PlayerHealthState.Dead)
                {
                    if(playerView.photonView.isMine)
                    {
                        int shieldOwner = shieldView.PhotonView.ownerId;

                        string playersName = null;

                        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
                        {
                            if (PhotonNetwork.playerList[i].ID == shieldOwner)
                            {
                                playersName = PhotonNetwork.playerList[i].NickName;
                                break;
                            }
                        }

                        GlobalManager.Instance.MatchManager.PhotonView.RPC("PlayerScored", PhotonTargets.All, playersName);
                        playerView.PlayerUIHandler.ShowMessage("Killed By " + playersName, 5.0f);

                        playerHealthState = PlayerHealthState.Dead;
                        playerView.PlayerAnimationHandler.Dead();

                        StartCoroutine("RevivingProcess");
                    }
                }
            }
        }
    }

    private IEnumerator RevivingProcess()
    {
        yield return new WaitForSeconds(3.0f);
        if (Respawn != null)
            Respawn.Invoke();
        
    }

    //Triggered from animation state on Exit
    public void HasRevived()
    {
        playerHealthState = PlayerHealthState.Alive;
    }
}
