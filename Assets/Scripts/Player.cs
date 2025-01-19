using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // 单例
    public static Player instance { get; private set; }
    public long money; // 玩家拥有的金钱
    public long allMoney; //玩家总资产
    public int aimMoney = 500;
    private long moneyOld;
    private long allMoneyOld;
    public List<Cards> cards = new List<Cards>(); // 玩家拥有的卡牌

    public long playerMaxMoney = 0;


    [SerializeField] private TextMeshProUGUI moneyText;
    public TextMeshProUGUI aimMoneyText;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }


    public void AddMoney(long amount)
    {
        moneyOld = money;
        money += amount;

        AudioManager.instance.PlayVoice(1);
    }

    public void RemoveMoney(long amount)
    {
        moneyOld = money;
        money -= amount;

        AudioManager.instance.PlayVoice(1);
    }

    void Update()
    {
        if (allMoney > playerMaxMoney)
        {
            playerMaxMoney = allMoney;
        }
    }

    private void FixedUpdate()
    {
        long moneyOfSlots = 0;
        foreach (Slot slot in GameManager.instance.slotsList)
        {
            moneyOfSlots += slot.priceTotal;
        }
        allMoney = moneyOfSlots + money;

        ChangeMoney();
        ChangeAllMoney();


    }

    private void ChangeMoney()
    {
        long moneyChangeAmount = money - moneyOld;
        if (moneyChangeAmount == 0)
        {
            return;
        }

        if (Math.Abs(moneyChangeAmount) < 10)
        {
            moneyOld = money;
        }
        else
        {
            long changeThisTurn = (long)(moneyChangeAmount / 5);
            moneyOld += changeThisTurn;
        }

        ShowMoney();
    }

    public void ShowMoney()
    {
        int textLen = moneyOld.ToString().Length;
        moneyText.fontSize = Mathf.Min((int)(300 / textLen * 2), 60);
        moneyText.text = moneyOld.ToString();
    }

    private void ChangeAllMoney()
    {
        long AllMoneyChangeAmount = allMoney - allMoneyOld;
        if (AllMoneyChangeAmount == 0)
        {
            return;
        }

        if (Math.Abs(AllMoneyChangeAmount) < 10)
        {
            allMoneyOld = allMoney;
        }
        else
        {
            long changeThisTurn = (long)(AllMoneyChangeAmount / 5);
            allMoneyOld += changeThisTurn;
        }

        ShowAllMoney();
    }

    public void ShowAllMoney()
    {
        string textCombined = allMoneyOld.ToString() + " / " + aimMoney.ToString();

        int textLen = textCombined.Length;
        aimMoneyText.fontSize = Mathf.Min((int)(300 / textLen * 2), 60);
        aimMoneyText.text = textCombined;
    }

    public void AimUpdate()
    {
        int round = GameManager.instance.round;
        // 更新目标金额
        if (round == 1)
            aimMoney = 5000;
        else if (round == 2)
            aimMoney = 50000;
        Debug.Log("Aim Update");
        ShowAllMoney();
    }

    public void AddCard(Cards card)
    {
        cards.Add(card);
    }

    public void RemoveCard(Cards card)
    {
        cards.Remove(card);
    }


}
