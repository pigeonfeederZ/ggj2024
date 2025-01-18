using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 单例
    public static UIManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public GameObject inTrunUI;
    public GameObject settleDownUI;

    public void SelectInTurnUI()
    {
        inTrunUI.gameObject.SetActive(true);
        settleDownUI.gameObject.SetActive(false);
    }

    public void SelectSettleDownUI()
    {
        inTrunUI.gameObject.SetActive(false);
        settleDownUI.gameObject.SetActive(true);
    }
}
