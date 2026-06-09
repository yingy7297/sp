using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsyncSceneLoader : MonoBehaviour
{
    //單例模式 Singleton Pattern
    //使用時機: 當此腳本只有一個實例物體時，並且其他腳本需要獲得這個腳本時
    //用來存取資料的靜態變數
    private static AsyncSceneLoader _instance;
    //唯讀屬性:讀外部取得此資料窗口
    public static AsyncSceneLoader instance
    {
        get
        {
            if (_instance == null)                                    //如果實體物件不存在
                _instance = FindAnyObjectByType<AsyncSceneLoader>(); //嘗試尋找場景中實例物體
            return _instance;                                        //回傳實體物件
        }
    }
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI loadingText;       //文字百分比
    [SerializeField] private Image loadingBar;                  //圖片進度條
    [SerializeField] private CanvasGroup group;                  //畫布群組

    /// <summary>
    /// 開始非同步載入場景
    /// </summary>
    /// <param name="sceneName">場景名稱.</param>
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));      //啟動協程來載入場景
    }

    /// <summary>
    /// 非同步載入場景協程
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns>IEnumerator for the coroutine</returns>
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return StartCoroutine(FadeSystem.Fade(group));   //淡入載入畫面                                                   //等待一個影格
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);     //開始非同步載入場景  AsyncOperation 非同步資料
        asyncOperation.allowSceneActivation = false;                               //預設為 false，載入完成後不自動切換場景

        while (!asyncOperation.isDone)                                             //當非同步載入未完成時持續執行
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);        // 計算進度 (0 到 0.9，然後在完成時為1)
            if (loadingText != null)                                              // 更新百分比文字
                loadingText.text = $"{Mathf.RoundToInt(progress * 100)}%";
            if (loadingBar != null)                                              // 更新進度條
                loadingBar.fillAmount = progress;
            if (asyncOperation.progress >= 0.9f)                                // 檢查是否載入完成
                asyncOperation.allowSceneActivation = true;                    //當載入完成時，允許場景切換
            yield return null;                                                 // 等待下一幀 null 一個影格的時間
        }
    }
}