using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI maxMoneyText;
    public TextMeshProUGUI currentMoneyText;


    private void OnEnable()
    {
        maxMoneyText.text = "Max Money: $" + Player.instance.playerMaxMoney.ToString() + " !";
        currentMoneyText.text = "Money Now: $" + Player.instance.money.ToString() + " !";

        AudioManager.instance.PlayMusic(3);
    }
}
