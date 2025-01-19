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
    public CardSlot cardChosenSlot;//选择的卡片槽
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

    public void IncreaseAmount(Slot _slot)
    {
        _slot.buyAmount = (int)(_slot.buyAmount * 1.2f);
    }

    public void BlanketEffect(Slot _slot)
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum < 10)
        {
            _slot.panicModifier = 20;
        }
        else if (randomNum < 30)
        {
            _slot.priceModifier = 5f;
        }
        else if (randomNum < 50)
        {
            Player.instance.AddMoney((int)(_slot.priceNow * _slot.buyAmount * 0.2f));
        }
    }

    public void IncreasePriceAll()
    {
        foreach (Slot slot in GameManager.instance.slotsList)
        {
            slot.SetPriceNow((int)(slot.priceNow * 1.05f));
        }
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
            case 5:
                IncreaseAmount(cardSlotChosen);
                Debug.Log("Case 5");
                break;
            case 6:
                BlanketEffect(cardSlotChosen);
                Debug.Log("Case 6");
                break;
            case 7:
                IncreasePriceAll();
                Debug.Log("Case 7");
                break;
        }


        cardChosen = -1;
        ClearSlotChosen();
        cardChosenSlot = null;

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
        cardCost *= 2;
        cardCostText.text = "$" + cardCost.ToString();
    }

    public void ClearSlotChosen()
    {

        if (cardSlotChosen != null)
        {
            cardSlotChosen.isSelected(false);
            cardSlotChosen = null;
        }

    }

    public void SelectCard()
    {
        if (cardChosenSlot == null)
        {
            return;
        }
        else
        {
            if (cardChosenSlot.card.needToSelectSlot)
            {
                UIManager.instance.CloseShopPanel();
                UIManager.instance.SwitchToUseCardButton();

            }
            else
            {
                takeCardEffect();
                UIManager.instance.CloseShopPanel();
            }
        }
    }

    public void ClickShopButton()
    {
        if (Player.instance.money < cardCost)
        {
            return;
        }
        AddCardCost();
        UIManager.instance.SelectShopPanel();
        distributeCard();
    }
}
