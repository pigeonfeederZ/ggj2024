using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComfirmManager : MonoBehaviour
{
    // 单例
    public static ComfirmManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public int amountOwned = -1;
    public Slot slotChosen;
    private int amountNow;

    public void OnClick()
    {
        if (slotChosen == null)
        {
            return;
        }
        else
        {
            amountNow = SliderManager.instance.GetNumber();
            Player.instance.RemoveMoney(slotChosen.priceNow * (amountNow - amountOwned));
            slotChosen.buyAmount = amountNow;
            amountOwned = amountNow;
        }
    }
}
