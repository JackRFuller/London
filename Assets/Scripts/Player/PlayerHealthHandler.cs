using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            PlayerView shieldPlayerView = other.GetComponent<ShieldView>().PlayerView;

            //Check the shield isn't mine
            if(shieldPlayerView != playerView)
            {
                if(playerHealthState != PlayerHealthState.Dead)
                {
                    if(playerView.photonView.isMine)
                    {
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
