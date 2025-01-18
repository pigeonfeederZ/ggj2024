using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    //单例
    public static SliderManager instance { get; private set; }

    public Slider slider;

    [SerializeField] private TextMeshProUGUI maxNumberText;
    [SerializeField] private TextMeshProUGUI leastNumberText;
    [SerializeField] private TextMeshProUGUI currentNumberText;
    public GameObject buyingPanel;


    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }


    //得到Slider的值
    public int GetNumber()
    {
        return Mathf.RoundToInt(slider.value);
    }

    public void ChangeSlider(int currentNumber, int maxNumber)
    {
        slider.maxValue = maxNumber;
        slider.value = currentNumber;
    }

    void Update()
    {
        if (ComfirmManager.instance.slotChosen == null)
        {
            slider.interactable = false;
            buyingPanel.SetActive(false);
        }
        else
        {
            slider.interactable = true;
            currentNumberText.text = Mathf.RoundToInt(slider.value).ToString();
            leastNumberText.text = ((int)slider.minValue).ToString();
            maxNumberText.text = ((int)slider.maxValue).ToString();
        }
    }

    public void CloseBuyingPanel()
    {
        buyingPanel.SetActive(false);
    }
}
