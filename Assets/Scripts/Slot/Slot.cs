using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

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

    public int amountOfPriceChange;
    private int oldPrice;
    private int addEachTurn;

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
        priceOrigin = goods.priceOrigin;
        priceText.text = priceOrigin.ToString();
        oldPrice = priceOrigin;
        panic = 0;
        amountOfPriceChange = 0;
        isBreak = false;
        ShowGoods();
        SetPriceNow(priceOrigin);
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
        oldPrice = int.Parse(priceText.text);

        if (isBreak == false)
        {
            amountOfPriceChange = priceNow - oldPrice;
            if (amountOfPriceChange < 50)
                addEachTurn = 1;
            else
                addEachTurn = amountOfPriceChange / 50;
        }

        else
            priceText.text = "0";



        ownedValueText.text = (buyAmount * priceNow).ToString();
    }

    private void FixedUpdate()
    {
        if (amountOfPriceChange > 0)
        {
            oldPrice += addEachTurn;
            priceText.text = oldPrice.ToString();
            amountOfPriceChange -= addEachTurn;
        }
        else
            priceText.text = priceNow.ToString();

    }

    public void SetClick(bool _canClick)
    {
        slotImage.canClick = _canClick;
    }
}
