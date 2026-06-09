using UnityEngine;
using UnityEngine.SceneManagement; // 必須引用此命名空間

public class SceneChanger : MonoBehaviour
{
    // [載入遊戲場景] 

    public void StartGame()
    {
        // 請確保引號內的名稱與你們的場景名稱完全一致
        SceneManager.LoadScene("遊戲場景");
    }

    // [回到主畫面]

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("遊戲主選單");
    }

    // [重新開始關卡] 

    public void RetryGame()
    {
        // SceneManager.GetActiveScene().name 會自動取得目前場景的名稱
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // [離開遊戲]

    public void QuitGame()
    {
        Debug.Log("遊戲已關閉"); // 在編輯器測試時只會顯示 Log
        Application.Quit();     // 只有在打包出來的 exe/apk 才會真正關閉
    }
}
