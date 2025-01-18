using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Cards card;
    public Image imageDisplay;
    public TextMeshProUGUI cardDiscription;

    public void Awake()
    {
        cardDiscription = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetCard(Cards _card)
    {
        card = _card;
        ShowCard();
    }

    public void ShowCard()
    {
        imageDisplay.sprite = card.icon;
        cardDiscription.text = card.description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardManager.instance.cardChosen != card.cardID)
        {
            //取消上一个选中的卡片
            CardManager.instance.cardChosen = card.cardID;
            //选中特效
        }
        else
        {
            CardManager.instance.cardChosen = -1;
            //取消选中特效
        }
    }
}
