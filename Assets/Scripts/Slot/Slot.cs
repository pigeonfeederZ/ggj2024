using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour
{
    public Goods goods;
    public int buyAmount;
    private Slot_Background slotBackground;
    private Slot_Image slotImage;
    public int priceNow;
    public int priceOrigin { get; private set; }
    public int demand;
    public int panic = 0;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI ownedValueText;

    public bool isBreak = false;

    private void Awake()
    {
        slotBackground = transform.GetComponentInChildren<Slot_Background>();
        slotImage = transform.GetComponentInChildren<Slot_Image>();
    }

    public void AddGoods(Goods goods, int amount)
    {
        this.goods = goods;
        this.buyAmount = amount;
        this.demand = Random.Range(50, 100);
        Debug.Log("Demand: " + demand);
        priceOrigin = goods.priceOrigin;
        panic = 0;
        ShowGoods();
        ShowPrice();
    }

    //增加购买数量
    public void AddAmount(int amount)
    {
        this.buyAmount += amount;
    }

    public void ReduceAmount(int amount)
    {
        this.buyAmount -= amount;
    }

    //设置当前价格
    public void SetPriceNow(int priceNow)
    {
        this.priceNow = priceNow;
        ShowPrice();
    }


    private void Start()
    {
        SetPriceNow(priceOrigin);
    }

    //清空Slot
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

    //显示Slot的背景
    public void ShowBackground(float breakPossibility)
    {
        slotBackground.ChangeSprite(breakPossibility);
    }

    //显示价格
    public void ShowPrice()
    {
        priceText.text = priceNow.ToString();
        ownedValueText.text = (buyAmount * priceNow).ToString();
    }

    public void SetClick(bool _canClick)
    {
        slotImage.canClick = _canClick;
    }
}
