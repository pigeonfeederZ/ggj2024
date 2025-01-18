using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using Random = System.Random;
using URandom = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // 单例
    public static GameManager instance { get; private set; }
    public List<Slot> slotsList = new List<Slot>();
    public List<Goods> goodsList = new List<Goods>();

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
        foreach (Slot slot in slotsList)
        {
            int randomNumber = URandom.Range(0, goodsList.Count - 1);
            slot.AddGoods(goodsList[randomNumber], 0);
            goodsList.RemoveAt(randomNumber);
        }
    }

    private void Start()
    {
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
                slot.SetClick(false);
                slot.SetPriceNow(0);
                Debug.Log("Bubble Break");
            }
            //否则价格增加
            else
            {
                slot.SetPriceNow(slot.priceNow + CalculatePriceAdd(slot));
            }

            ComfirmManager.instance.InitiateConfirm();
        }

        if (AllSlotBreak())
        {
            UIManager.instance.SelectSettleDownUI();
        }
    }

    //判断Bubble是否破裂
    public void IsBubbleBreak(Slot _slot)
    {
        CalculatePanic(_slot);
        int breakProb = URandom.Range(0, 101);
        if (breakProb > _slot.panic)
            _slot.isBreak = false;
        else
        {
            _slot.isBreak = true;
        }
    }

    public int CalculatePriceAdd(Slot _slot)
    {
        int priceEq = _slot.demand;
        float max_price_growth = (float)0.2; // 最大价格增速
        float min_price_growth = (float)0.05; // 最小价格增速
        return (int)(_slot.priceNow * Math.Max((-max_price_growth / Math.Pow(priceEq, 2) * Math.Pow(_slot.priceNow, 2) + max_price_growth), min_price_growth)) + 1;
    }

    public void CalculatePanic(Slot _slot)
    {
        _slot.panic += (int)GetGaussDistributeRandom(3 * Math.Pow(Math.E, _slot.priceNow - _slot.demand), 0.5);
    }

    private double GetGaussDistributeRandom(double miu, double sigma2)   // 均值 方差
    {
        Random ran = new Random();
        double r1 = ran.NextDouble();
        double r2 = ran.NextDouble();
        double r = Math.Sqrt((-2) * Math.Log(r2)) * Math.Sin(2 * Math.PI * r1);
        double result = miu + sigma2 * r;
        Console.WriteLine(result);
        return result;
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
        InitiateGoods(goodsList);
        UIManager.instance.SelectInTurnUI();
    }
}
