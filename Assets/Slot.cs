using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Goods goods;
    public int buyAmount;
    private Slot_Background slotBackground;
    private Slot_Image slotImage;

    private void Awake()
    {
        slotBackground = transform.GetComponentInChildren<Slot_Background>();
        slotImage = transform.GetComponentInChildren<Slot_Image>();
        ShowGoods();
    }

    public void AddGoods(Goods goods, int amount)
    {
        this.goods = goods;
        this.buyAmount = amount;
    }

    public void AddAmount(int amount)
    {
        this.buyAmount += amount;
    }

    public void ReduceAmount(int amount)
    {
        this.buyAmount -= amount;
    }

    public void Clear()
    {
        goods = null;
        buyAmount = 0;
    }

    //将商品的图标显示在Slot上
    public void ShowGoods()
    {
        slotImage.SetImage(goods.icon);
    }


    public void ShowBackground(float breakPossibility)
    {

        slotBackground.ChangeSprite(breakPossibility);
    }
}
