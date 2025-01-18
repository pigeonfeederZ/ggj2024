using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject background;
    public GameObject frame;

    public List<Sprite> backgroundList = new List<Sprite>();
    public List<Sprite> frameList = new List<Sprite>();

    public void ChangeToRound(int _round)
    {
        UIManager.instance.SelectInTurnUI();
        GameManager.instance.InitiateGoods(GameManager.instance.goodsList);
        background.GetComponent<Image>().sprite = backgroundList[_round];
        frame.GetComponent<Image>().sprite = frameList[_round];
    }
}
