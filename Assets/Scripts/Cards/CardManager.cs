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

    #region 卡片效果
    public void decreaseCardEffect(Slot _slot)
    {
        _slot.panicModifier = 10;
    }

    public void increasePrice(Slot _slot)
    {
        _slot.priceModifier = 5f;
    }

    public void gainMoneyNow(Slot _slot)
    {
        Player.instance.AddMoney((int)(_slot.priceNow * _slot.buyAmount * 0.2f));
    }

    public void decreasePrice(Slot _slot)
    {
        _slot.SetPriceNow((int)(_slot.priceNow * 0.8f));
        _slot.panicModifier = 20;
    }

    # endregion

    public void takeCardEffect()
    {
        Debug.Log("Take Card Effect");
        switch (cardChosen)
        {
            case 1:
                Debug.Log("Case 1");
                decreaseCardEffect(cardSlotChosen);
                break;
            case 2:
                Debug.Log("Case 2");
                increasePrice(cardSlotChosen);
                break;
            case 3:
                Debug.Log("Case 3");
                gainMoneyNow(cardSlotChosen);
                break;
            case 4:
                decreasePrice(cardSlotChosen);
                Debug.Log("Case 4");
                break;
        }


        //cardChosen = -1;
        ClearSlotChosen();

        foreach (CardSlot cardSlot in cardSlots)
        {
            cardSlot.IsSelected(false);
        }
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
