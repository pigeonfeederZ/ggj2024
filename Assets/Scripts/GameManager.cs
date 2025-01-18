using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using Random = System.Random;
using URandom = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 单例
    public static GameManager instance { get; private set; }
    public List<Slot> slotsList = new List<Slot>();
    public List<Goods> goodsList = new List<Goods>();

    public int aimMoney = 1000;
    public TextMeshProUGUI aimMoneyText;

    public int round = 0;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    // 为每个Slot添加的物品
    public void InitiateGoods(List<Goods> goodsList)
    {
        for (int i = 0; i < 6; i++)
        {
            slotsList[i].AddGoods(goodsList[i], 0);
        }
    }

    private void Start()
    {
        aimMoneyText.text = aimMoney.ToString();
        InitiateGoods(goodsList);

    }

    //过回合后增加商品价格
    public void NextTurn()
    {
        foreach (Slot slot in slotsList)
        {
            if (slot.isBreak)
            {
                continue;
            }


            //先判定Bubble是否破裂
            IsBubbleBreak(slot);
            if (slot.isBreak)
            {
                //若破裂则播放动画
                slot.animator.SetBool("isBreak", true);
                slot.SetClick(false);
                slot.SetPriceNow(0);
                Debug.Log("Bubble Break");
            }
            //否则价格增加
            else
            {
                slot.SetPriceNow(slot.priceNow + CalculatePriceAdd(slot));
                CalculatePanic(slot);
            }

            ComfirmManager.instance.InitiateConfirm();
        }

        if (AllSlotBreak())
        {
            CheckGameEnd();
        }
    }

    //判断Bubble是否破裂
    public void IsBubbleBreak(Slot _slot)
    {

        int breakProb = URandom.Range(0, 101);
        if (breakProb > _slot.panic)
        {
            _slot.isBreak = false;
        }
        else
        {
            _slot.isBreak = true;
            _slot.amountOfPriceChange = 0;
        }
    }

    public int CalculatePriceAdd(Slot _slot)
    {
        return Mathf.RoundToInt(Math.Abs(GetGaussDistributeRandom(0.2 + Math.Log10(_slot.buyAmount + 1), 0.5) * _slot.priceNow));
    }

    public void CalculatePanic(Slot _slot)
    {
        _slot.panic = (int)GetGaussDistributeRandom(80 / 3.14 * Math.Atan(_slot.priceNow - _slot.demand), 5);

        Debug.Log(_slot.name + " " + _slot.panic);
        _slot.animator.SetInteger("PanicTrigger", _slot.panic);
    }

    private float GetGaussDistributeRandom(double miu, double sigma2)   // 均值 方差
    {
        Random ran = new Random();
        double r1 = ran.NextDouble();
        double r2 = ran.NextDouble();
        double r = Math.Sqrt((-2) * Math.Log(r2)) * Math.Sin(2 * Math.PI * r1);
        double result = miu + sigma2 * r;
        Console.WriteLine(result);
        return (float)result;
    }

    public bool AllSlotBreak()
    {
        foreach (Slot slot in slotsList)
        {
            if (!slot.isBreak)
            {
                return false;
            }
        }
        Debug.Log("All Slot Break");
        return true;
    }

    public void EnterNextRound()
    {
        if (round == 3)
            return;
        round++;
        AimUpdate();
        RoundManager.instance.ChangeToRound(round);
    }

    public void AimUpdate()
    {
        // 更新目标金额
        Debug.Log("Aim Update");
        aimMoneyText.text = aimMoney.ToString();
    }

    public void CheckGameEnd()
    {
        if (Player.instance.money < aimMoney)
        {
            UIManager.instance.SelectGameOverUI();
        }
        else
        {
            UIManager.instance.SelectSettleDownUI();
        }
    }
}
