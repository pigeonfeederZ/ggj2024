using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_Image : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private Slot slot;
    public float fadeDuration = 0.5f;
    public bool canClick = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        slot = GetComponentInParent<Slot>();
    }

    public void SetImage(Sprite _image)
    {
        image.sprite = _image;

        Color color = image.color;
        color.a = 0;
        image.color = color;

        StartCoroutine(FadeIn());


    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Color color = image.color;
            color.a = alpha;
            image.color = color;

            yield return null;
        }

        // 确保完全不透明
        Color finalColor = image.color;
        finalColor.a = 1f;
        image.color = finalColor;
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
