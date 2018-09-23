using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIHandler : PlayerHandler
{
    [SerializeField]
    private GameObject playerCanvasObj;
    [SerializeField]
    private GameObject playerChargeBarObj;

    [Header("Charge Bar")]
    [SerializeField]
    private Image playerChargeBarImage;

    [Header("Player Messages")]
    [SerializeField]
    private TMP_Text playerMessageText;


    protected override void Start()
    {
        base.Start();

        if(!playerView.photonView.isMine)
        {
            playerCanvasObj.SetActive(false);
        }

        playerMessageText.enabled = false;
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

    public void ShowMessage(string message, float showTime)
    {
        if(photonView.isMine)
        {
            playerMessageText.enabled = true;
            playerMessageText.text = message;

            if (showTime > 0)
            {
                StartCoroutine(HidePlayerMessageAfterElapsedTime(showTime));
            }
        }
    }

    IEnumerator HidePlayerMessageAfterElapsedTime(float messageTime)
    {
        yield return new WaitForSeconds(messageTime);
        playerMessageText.enabled = false;
    }

    
}
