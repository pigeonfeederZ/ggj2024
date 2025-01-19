using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using System;

public class Slot : MonoBehaviour
{
    public Goods goods;
    public int buyAmount;
    private Slot_Background slotBackground;
    private Slot_Image slotImage;
    public int priceNow;
    public long priceTotal;
    public int priceOrigin;
    public int panic = 0;

    public GameObject priceBackground;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI ownedAmountText;

    public int amountOfPriceChange;
    private int oldPrice;
    private int addEachTurn;

    public bool isBreak = false;

    public int panicModifier = 0;
    public float priceModifier = 1;

    public Animator animator;

    private void Awake()
    {
        slotBackground = transform.GetComponentInChildren<Slot_Background>();
        slotImage = transform.GetComponentInChildren<Slot_Image>();
        animator = GetComponentInChildren<Animator>();
    }

    public void AddGoods(Goods goods, int amount)
    {
        this.goods = goods;
        this.buyAmount = amount;
        priceOrigin = (int)UnityEngine.Random.Range((int)(10 * Math.Pow(10, goods.round)), (int)(50 * Math.Pow(10, goods.round)));
        priceText.text = priceOrigin.ToString();
        oldPrice = priceOrigin;
        priceNow = priceOrigin;
        panic = -10;
        animator.SetInteger("PanicTrigger", panic);
        amountOfPriceChange = 0;
        isBreak = false;
        animator.SetBool("isBreak", false);
        SetClick(true);

        ShowGoods();
        SetPriceNow(priceOrigin);
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
        oldPrice = int.Parse(priceText.text.Substring(1, priceText.text.Length - 1));

        if (isBreak == false)
        {
            amountOfPriceChange = priceNow - oldPrice;
            if (amountOfPriceChange < 50)
                addEachTurn = 1;
            else
                addEachTurn = amountOfPriceChange / 50;
        }

        else
            priceText.text = "$0";


        priceTotal = (long)buyAmount * (long)priceNow;
        ownedAmountText.text = buyAmount.ToString();
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
            priceText.text = "$" + priceNow.ToString();

    }

    public void SetClick(bool _canClick)
    {
        slotImage.canClick = _canClick;
    }

    public void isSelected(bool _isSelected)
    {
        //改变颜色
        if (_isSelected)
            priceBackground.GetComponent<Image>().color = new Color(0.1784865f, 0.9716981f, 0.06875221f, 1f);
        else
            priceBackground.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
