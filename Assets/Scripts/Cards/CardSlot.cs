using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{
    public Cards card;

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
