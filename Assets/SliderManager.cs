using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    //单例
    public static SliderManager instance { get; private set; }

    private Slider slider;

    [SerializeField] private TextMeshProUGUI maxNumberText;
    [SerializeField] private TextMeshProUGUI leastNumberText;
    [SerializeField] private TextMeshProUGUI currentNumberText;
    [SerializeField] GameObject numberText;


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

    void Update()
    {
        if (ComfirmManager.instance.slotChosen == null)
        {
            slider.interactable = false;
            numberText.SetActive(false);
        }
        else
        {
            slider.interactable = true;
            numberText.SetActive(true);
            currentNumberText.text = ((int)slider.value).ToString();
            leastNumberText.text = ((int)slider.minValue).ToString();
            maxNumberText.text = ((int)slider.maxValue).ToString();
        }
    }
}
