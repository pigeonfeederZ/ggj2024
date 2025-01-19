using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnim : MonoBehaviour
{
    public void SpawnNewGoods()
    {
        Slot slot = transform.parent.GetComponent<Slot>();
        if (slot.goods.round < GameManager.instance.round)
        {
            slot.AddGoods(GameManager.instance.goodsList[0], 0);
            GameManager.instance.goodsList.RemoveAt(0);
        }
    }
}
