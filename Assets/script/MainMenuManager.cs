using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主選單管理器
/// 包含：開始遊戲、選項、製作團隊與退出按鈕 (已移除繼續遊戲)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("主選單按鈕")]
    [SerializeField] private Button btnNewGame;         //新遊戲按鈕
    [SerializeField] private Button btnOptions;         //選項按鈕
    [SerializeField] private Button btnCredits;         //製作團隊按鈕
    [SerializeField] private Button btnQuit;            //退出按鈕

    [Header("選項與製作團隊介面")]
    [SerializeField] private Button btnBackOptions;     //從選項返回的按鈕
    [SerializeField] private CanvasGroup groupOptions;  //選項畫布群組
    [SerializeField] private Button btnBackCredits;     //從製作團隊返回的按鈕
    [SerializeField] private CanvasGroup groupCredits;  //製作團隊畫布群組

    private void Awake()
    {
        // 控制選項介面的淡入與淡出
        btnOptions.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupOptions, interval: 0.05f)));
        btnBackOptions.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupOptions, false)));

        // 控制製作團隊介面的淡入與淡出
        btnCredits.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupCredits, interval: 0.05f)));
        btnBackCredits.onClick.AddListener(() => StartCoroutine(FadeSystem.Fade(groupCredits, false)));

        // 點擊退出按鈕時關閉應用程式
        btnQuit.onClick.AddListener(() =>
        {
            Debug.Log("<color=#ff3>退出遊戲</color>");   //在主控台顯示訊息
            Application.Quit();
        });

        // 點擊開始遊戲按鈕時，載入遊戲場景
        btnNewGame.onClick.AddListener(() => AsyncSceneLoader.instance.LoadSceneAsync("遊戲場景"));
    }
}