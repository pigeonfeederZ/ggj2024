using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    //单例
    public static SliderManager instance { get; private set; }

    private Slider slider;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    //得到Slider的值
    public int GetNumber()
    {
        return (int)slider.value;
    }

    public void ChangeSlider(int currentNumber, int maxNumber)
    {
        slider.maxValue = maxNumber;
        slider.value = currentNumber;
    }
}
