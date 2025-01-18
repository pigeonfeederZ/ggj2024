using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // 单例
    public static CardManager instance { get; private set; }
    public List<Cards> cards = new List<Cards>(); // 玩家拥有的卡牌

    public List<CardSlot> cardSlots = new List<CardSlot>(); // 卡槽

    //选择的卡片
    public int cardChosen = -1;
    public Slot cardSlotChosen;

    //每张卡片的花费
    public int cardCost = 10;
    public TextMeshProUGUI cardCostText;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }


    public void decreaseCardEffect(Slot _slot)
    {
        _slot.panicModifier = 10;
    }

    public void takeCardEffect()
    {
        if (cardChosen == 1)
        {
            decreaseCardEffect(cardSlotChosen);
        }

        cardChosen = -1;
        ClearSlotChosen();
    }

    public void distributeCard()
    {
        foreach (CardSlot cardSlot in cardSlots)
        {
            cardSlot.SetCard(cards[Random.Range(0, cards.Count)]);
        }
    }

    public void AddCardCost()
    {
        cardCost += 10;
        cardCostText.text = cardCost.ToString();
    }

    public void ClearSlotChosen()
    {

        if (cardSlotChosen != null)
        {
            cardSlotChosen.isSelected(false);
            cardSlotChosen = null;
        }

    }
}
