using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 单例
    public static Player instance { get; private set; }
    public int money; // 玩家拥有的金钱
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

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        moneyText.text = money.ToString();
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
