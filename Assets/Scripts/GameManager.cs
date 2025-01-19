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
    public List<Goods> goodsListCopy = new List<Goods>();

    public int round = 0;


    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void InitiateWholeGame()
    {
        goodsList.Clear();
        goodsListCopy.ForEach(goods => goodsList.Add(goods));

        round = 0;
        InitiateWithoutGoodsList();

        Player.instance.aimMoney = 500;
        Player.instance.money = 100;
        Player.instance.allMoney = 100;
        Player.instance.ShowMoney();
        Player.instance.ShowAllMoney();

        CardManager.instance.cardChosen = -1;
        CardManager.instance.cardChosenSlot = null;
        CardManager.instance.cardSlotChosen = null;
        CardManager.instance.cardCost = 10;
        CardManager.instance.cardCostText.text = "$10";

        ComfirmManager.instance.InitiateConfirm();

        AudioManager.instance.PlayMusic(0);

        RoundManager.instance.ChangeToRound(0);



    }

    public void InitiateWithoutGoodsList()
    {
        InitiateGoods(goodsList);
    }

    // 为每个Slot添加的物品
    public void InitiateGoods(List<Goods> goodsList)
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(slotsList[i].isBreak);
            Debug.Log(slotsList[i].goods);
            if (slotsList[i].isBreak || slotsList[i].goods == null)
            {
                slotsList[i].AddGoods(goodsList[0], 0);
                goodsList.RemoveAt(0);
            }
        }
    }


    //过回合后增加商品价格
    public void NextTurn()
    {
        long moneyOfSlots = 0;
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
                slot.buyAmount = 0;
                slot.ownedAmountText.text = "";
                Debug.Log("Bubble Break");

                //如果是上一round的商品破裂，则补充新的
                //配合动画放在新函数里：SpawnNewGoods

            }
            //否则价格增加
            else
            {
                slot.SetPriceNow(slot.priceNow + CalculatePriceAdd(slot));
                CalculatePanic(slot);
            }

            moneyOfSlots += slot.priceTotal;

            ComfirmManager.instance.InitiateConfirm();
            slot.panicModifier = 0;
            slot.priceModifier = 1;
        }
        // 计算总资产
        Player.instance.allMoney = moneyOfSlots + Player.instance.money;
        // Debug.Log(Player.instance.allMoney);

        CheckGameEnd();
        ComfirmManager.instance.InitiateConfirm();

        UIManager.instance.SwithToShopButton();



    }

    //判断Bubble是否破裂
    public void IsBubbleBreak(Slot _slot)
    {

        int breakProb = URandom.Range(0, 101);
        Debug.Log(name + breakProb);
        if (breakProb > _slot.panic)
        {
            _slot.isBreak = false;
        }
        else
        {
            _slot.isBreak = true;
            _slot.amountOfPriceChange = 0;
            AudioManager.instance.PlayVoice(0);
        }
    }

    public int CalculatePriceAdd(Slot _slot)
    {
        return Math.Abs(Mathf.RoundToInt(GetGaussDistributeRandom(0.2 + Math.Log10((_slot.buyAmount + 1) * _slot.priceOrigin / 10 / Math.Pow(10, _slot.goods.round)), 0.3) * _slot.priceOrigin * _slot.priceModifier));
    }

    public void CalculatePanic(Slot _slot)
    {

        _slot.panic = (int)GetGaussDistributeRandom((_slot.priceNow - _slot.priceOrigin) * 0.5 / Math.Pow(10, _slot.goods.round), 5) - _slot.panicModifier;

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

        //播放一段过场动画
        UIManager.instance.ShowSettleDownUI();

        //播放音乐
        AudioManager.instance.PlayMusic(round);

        Player.instance.AimUpdate();
        RoundManager.instance.ChangeToRound(round);
    }



    public void CheckGameEnd()
    {
        if (AllSlotBreak())
        {
            UIManager.instance.SelectGameOverUI();
        }
        else if (Player.instance.allMoney >= Player.instance.aimMoney)
        {
            EnterNextRound();
        }

        foreach (Slot slot in slotsList)
        {
            if (!slot.isBreak)
            {
                if (slot.buyAmount != 0)
                {
                    return;
                }
                else if (Player.instance.money >= slot.priceNow)
                {
                    return;
                }
            }
        }

        UIManager.instance.SelectGameOverUI();
    }


}
