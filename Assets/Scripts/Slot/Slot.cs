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
    public long priceTotal;
    public int priceOrigin { get; private set; }
    public int demand;
    public int panic = 0;

    public GameObject priceBackground;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI ownedValueText;

    public int amountOfPriceChange;
    private int oldPrice;
    private int addEachTurn;

    public bool isBreak = false;

    public int panicModifier = 0;

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
        priceOrigin = goods.priceOrigin;
        priceText.text = priceOrigin.ToString();
        oldPrice = priceOrigin;
        panic = 0;
        animator.SetInteger("PanicTrigger", panic);
        amountOfPriceChange = 0;
        isBreak = false;
        animator.SetBool("isBreak", false);
        SetClick(true);

        //设置不同阶段商品的demand
        if (goods.round == 0)
            this.demand = Random.Range(50, 100);
        else if (goods.round == 1)
            this.demand = Random.Range(300, 500);
        else if (goods.round == 2)
            this.demand = Random.Range(1000, 2000);

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


        priceTotal = (long)buyAmount * (long)priceNow;
        ownedValueText.text = priceTotal.ToString();
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

    public void isSelected(bool _isSelected)
    {
        //改变颜色
        if (_isSelected)
            priceBackground.GetComponent<Image>().color = new Color(0.1784865f, 0.9716981f, 0.06875221f, 1f);
        else
            priceBackground.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
