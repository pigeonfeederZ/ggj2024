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
    private long moneyChangeAmount;
    private long allMoneyChangeAmount;
    private long moneyOld;
    private long allMoneyOld;
    public List<Cards> cards = new List<Cards>(); // 玩家拥有的卡牌


    [SerializeField] private TextMeshProUGUI moneyText;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    void Start()
    {
        moneyText.text = money.ToString();
    }

    public void AddMoney(long amount)
    {
        moneyOld = money;
        money += amount;
    }

    public void RemoveMoney(long amount)
    {
        moneyOld = money;
        money -= amount;
    }

    private void FixedUpdate()
    {
        ChangeMoney();
    }

    private void ChangeMoney()
    {
        moneyChangeAmount = money - moneyOld;
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

    private void ShowMoney()
    {
        int textLen = moneyOld.ToString().Length;
        if (textLen * 30 > 270)
        {
            moneyText.fontSize = (int)(300 / textLen * 2);
        }

        moneyText.text = moneyOld.ToString();
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
