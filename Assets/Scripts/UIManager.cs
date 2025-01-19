using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 单例
    public static UIManager instance { get; private set; }
    public GameObject bgInSettleDown;
    public GameObject panelInSettleDown;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public GameObject inTrunUI;
    public GameObject settleDownUI;
    public GameObject gameOverUI;
    public GameObject ShopPanel;
    public GameObject MainMenulUI;

    public GameObject ShopButton;
    public GameObject UseCardButton;
    public GameObject NextTurnButton;

    public float moveDuration = 2f;

    public void SelectMainMenulUI()
    {
        gameOverUI.gameObject.SetActive(false);
        inTrunUI.gameObject.SetActive(false);
        settleDownUI.gameObject.SetActive(false);
        MainMenulUI.gameObject.SetActive(true);
    }

    public void SelectInTurnUI()
    {
        gameOverUI.gameObject.SetActive(false);
        inTrunUI.gameObject.SetActive(true);
        MainMenulUI.gameObject.SetActive(false);
    }

    public void SelectGameOverUI()
    {
        gameOverUI.gameObject.SetActive(true);
        inTrunUI.gameObject.SetActive(false);
        settleDownUI.gameObject.SetActive(false);
        MainMenulUI.gameObject.SetActive(false);
    }

    public void SelectShopPanel()
    {
        ShopPanel.gameObject.SetActive(true);
        Player.instance.RemoveMoney(CardManager.instance.cardCost);
    }

    public void CloseShopPanel()
    {
        ShopPanel.gameObject.SetActive(false);
    }

    public void SwitchToUseCardButton()
    {
        ShopButton.gameObject.SetActive(false);
        NextTurnButton.gameObject.SetActive(false);
        UseCardButton.gameObject.SetActive(true);
    }

    public void SwithToShopButton()
    {
        ShopButton.gameObject.SetActive(true);
        UseCardButton.gameObject.SetActive(false);
        NextTurnButton.gameObject.SetActive(true);
        CardManager.instance.cardChosen = -1;
        CardManager.instance.ClearSlotChosen();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowSettleDownUI()
    {
        settleDownUI.gameObject.SetActive(true);

        Image panelImage = panelInSettleDown.GetComponent<Image>();

        Color color = panelImage.color;
        color.a = 0f;
        panelImage.color = color;

        Vector3 startPosition = bgInSettleDown.transform.position;
        startPosition.y = 400f + 540f; // 设定起始 y 轴位置为 400
        bgInSettleDown.transform.position = startPosition;


        StartCoroutine(MoveAndFadeInCoroutine());
    }

    private IEnumerator MoveAndFadeInCoroutine()
    {
        float elapsedTime = 0f;



        // 初始背景颜色设置为较暗


        // 图片初始位置

        // 执行图片移动和背景渐变
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            // 计算当前透明度和位置
            float t = elapsedTime / moveDuration;

            // 图片从 y=400 移动到 y=0
            bgInSettleDown.transform.position = new Vector3(bgInSettleDown.transform.position.x, Mathf.Lerp(400 + 540f, 540f, t), bgInSettleDown.transform.position.z);

            // 背景颜色渐变
            float alpha = Mathf.Lerp(0f, 0.5f, t);
            Color color = panelInSettleDown.GetComponent<Image>().color;
            color.a = alpha;
            panelInSettleDown.GetComponent<Image>().color = color;

            yield return null; // 等待下一帧
        }

        // 确保最终位置和背景颜色
        bgInSettleDown.transform.position = new Vector3(bgInSettleDown.transform.position.x, 540f, bgInSettleDown.transform.position.z);
        Color finalColor = panelInSettleDown.GetComponent<Image>().color;
        finalColor.a = 0.5f;
        panelInSettleDown.GetComponent<Image>().color = finalColor;

        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveAndFadeOutCoroutine());
    }

    private IEnumerator MoveAndFadeOutCoroutine()
    {
        float elapsedTime = 0f;

        // 执行图片移动和背景渐变
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            // 计算当前透明度和位置
            float t = elapsedTime / moveDuration;

            // 图片从 y=0 移动到 y=400
            bgInSettleDown.transform.position = new Vector3(bgInSettleDown.transform.position.x, Mathf.Lerp(540f, 940f, t), bgInSettleDown.transform.position.z);

            // 背景颜色渐变
            float alpha = Mathf.Lerp(0.5f, 0f, t);
            Color color = panelInSettleDown.GetComponent<Image>().color;
            color.a = alpha;
            panelInSettleDown.GetComponent<Image>().color = color;

            yield return null; // 等待下一帧
        }

        // 确保最终位置和背景颜色
        bgInSettleDown.transform.position = new Vector3(bgInSettleDown.transform.position.x, 940f, bgInSettleDown.transform.position.z);
        Color finalColor = panelInSettleDown.GetComponent<Image>().color;
        finalColor.a = 0f;
        panelInSettleDown.GetComponent<Image>().color = finalColor;

        settleDownUI.gameObject.SetActive(false);
    }
}