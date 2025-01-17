using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 单例
    public static GameManager instance { get; private set; }
    [SerializeField] private List<Slot> slotsList = new List<Slot>();
    [SerializeField] private List<Goods> goodsList = new List<Goods>();

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
            int randomNumber = Random.Range(0, goodsList.Count - 1);
            slot.AddGoods(goodsList[randomNumber], 0);
            goodsList.RemoveAt(randomNumber);
            slot.ShowGoods();
        }
    }

    private void Start()
    {
        InitiateGoods(goodsList);
    }
}
