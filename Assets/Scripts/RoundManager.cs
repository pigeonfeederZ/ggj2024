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
    public List<Sprite> backgroundList = new List<Sprite>();
    public GameObject frame;
    public List<Sprite> frameList = new List<Sprite>();
    public GameObject money;
    public List<Sprite> moneyList = new List<Sprite>();
    public GameObject moneyAll;
    public List<Sprite> moneyAllList = new List<Sprite>();
    public GameObject profile;
    public List<Sprite> profileList = new List<Sprite>();
    public GameObject shop;
    public List<Sprite> shopList = new List<Sprite>();
    public GameObject useCard;
    public List<Sprite> useCardList = new List<Sprite>();
    public GameObject nextTurn;
    public List<Sprite> nextTurnList = new List<Sprite>();
    public List<Sprite> priceIcon = new List<Sprite>();

    public void ChangeToRound(int _round)
    {
        // UIManager.instance.SelectInTurnUI();
        GameManager.instance.InitiateGoods(GameManager.instance.goodsList);
        background.GetComponent<Image>().sprite = backgroundList[_round];
        frame.GetComponent<Image>().sprite = frameList[_round];
        money.GetComponent<Image>().sprite = moneyList[_round];
        moneyAll.GetComponent<Image>().sprite = moneyAllList[_round];
        profile.GetComponent<Image>().sprite = profileList[_round];
        shop.GetComponent<Image>().sprite = shopList[_round];
        useCard.GetComponent<Image>().sprite = useCardList[_round];
        nextTurn.GetComponent<Image>().sprite = nextTurnList[_round];
    }
}
