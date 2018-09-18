using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : PlayerHandler
{
    [SerializeField]
    private GameObject playerCanvasObj;
    [SerializeField]
    private GameObject playerChargeBarObj;

    [Header("Charge Bar")]
    [SerializeField]
    private Image playerChargeBarImage;


    protected override void Start()
    {
        base.Start();

        if(!playerView.photonView.isMine)
        {
            playerCanvasObj.SetActive(false);
        }
        
        HidePlayerChargeBar();
    }

    public void ChargeBar(float fillAmount)
    {
        playerChargeBarImage.fillAmount = fillAmount;
        ShowPlayerChargeBar();
    }

    public void ShowPlayerChargeBar()
    {
        if (!playerChargeBarObj.activeInHierarchy)
            playerChargeBarObj.SetActive(true);
    }

    public void HidePlayerChargeBar()
    {
        playerChargeBarObj.SetActive(false);
        playerChargeBarImage.fillAmount = 0;
    }

    
}
