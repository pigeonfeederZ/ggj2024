using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Cards card;
    public Image imageDisplay;
    public TextMeshProUGUI cardDiscription;

    public bool isSelected = false;

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
        if (CardManager.instance.cardChosenSlot != this)
        {

            //取消上一个选中的卡片
            if (CardManager.instance.cardChosenSlot != null)
            {
                CardManager.instance.cardChosenSlot.IsSelected(false);
            }

            CardManager.instance.cardChosen = card.cardID;
            CardManager.instance.cardChosenSlot = this;

            //选中特效
            IsSelected(true);
        }
        else
        {
            CardManager.instance.cardChosen = -1;
            CardManager.instance.cardChosenSlot = null;
            //取消选中特效
            IsSelected(false);
        }
    }
    public void IsSelected(bool _isSelected)
    {
        //改变颜色
        if (_isSelected)
            GetComponent<Image>().color = new Color(0.1784865f, 0.9716981f, 0.06875221f, 1f);
        else
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}

