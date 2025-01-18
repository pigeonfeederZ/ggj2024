using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 单例
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public GameObject inTrunUI;
    public GameObject settleDownUI;
    public GameObject gameOverUI;
    public GameObject ShopPanel;

    public GameObject ShopButton;
    public GameObject UseCardButton;
    public GameObject NextTurnButton;

    public void SelectInTurnUI()
    {
        gameOverUI.gameObject.SetActive(false);
        inTrunUI.gameObject.SetActive(true);
        settleDownUI.gameObject.SetActive(false);
    }

    public void SelectGameOverUI()
    {
        gameOverUI.gameObject.SetActive(true);
        inTrunUI.gameObject.SetActive(false);
        settleDownUI.gameObject.SetActive(false);
    }

    public void SelectShopPanel()
    {
        ShopPanel.gameObject.SetActive(true);
        Player.instance.RemoveMoney(CardManager.instance.cardCost);
    }

    public void CloseShopPanel()
    {
        ShopPanel.gameObject.SetActive(false);
    }

    public void SwitchToUseCardButton()
    {
        ShopButton.gameObject.SetActive(false);
        NextTurnButton.gameObject.SetActive(false);
        UseCardButton.gameObject.SetActive(true);
    }

    public void SwithToShopButton()
    {
        ShopButton.gameObject.SetActive(true);
        UseCardButton.gameObject.SetActive(false);
        NextTurnButton.gameObject.SetActive(true);
        CardManager.instance.cardChosen = -1;
        CardManager.instance.ClearSlotChosen();
    }
}
