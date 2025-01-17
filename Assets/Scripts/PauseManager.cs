using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public GameObject pauseMenuUI; // 暂停菜单的UI界面
    public bool isPaused = false;

    private void Awake()
    {
        // 单例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 检测ESC键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // 暂停游戏
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // 显示暂停菜单
        Time.timeScale = 0f; // 停止游戏时间
        isPaused = true;
    }

    // 继续游戏
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1f; // 恢复游戏时间
        isPaused = false;
    }
}
