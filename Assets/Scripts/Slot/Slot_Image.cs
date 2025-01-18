using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_Image : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private Slot slot;
    public bool canClick = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        slot = GetComponentInParent<Slot>();
    }

    public void SetImage(Sprite _image)
    {
        image.sprite = _image;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardManager.instance.cardChosen != -1)
        {
            if (CardManager.instance.cardSlotChosen != slot && CardManager.instance.cardSlotChosen != null)
            {
                CardManager.instance.cardSlotChosen.isSelected(false);
                CardManager.instance.cardSlotChosen = slot;
                slot.isSelected(true);
                return;
            }
            else if (CardManager.instance.cardSlotChosen == null)
            {
                CardManager.instance.cardSlotChosen = slot;
                slot.isSelected(true);
                return;
            }
            else
            {
                CardManager.instance.ClearSlotChosen();
                return;
            }
        }

        if (slot.goods == null || !canClick)
        {
            return;
        }
        else
        {
            SliderManager.instance.ChangeSlider(slot.buyAmount, (int)(Player.instance.money / (long)slot.priceNow) + slot.buyAmount);
            ComfirmManager.instance.amountOwned = slot.buyAmount;
            ComfirmManager.instance.slotChosen = slot;
            SliderManager.instance.buyingPanel.SetActive(true);
        }

    }
}
