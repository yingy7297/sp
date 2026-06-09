using UnityEngine;
using System.Collections;

/// <summary>
/// 處理群組元件 Canvas Group 淡入淡出
/// </summary>
public class FadeSystem
{
    /// <summary>
    /// 淡入淡出
    /// </summary>
    /// <param name="group">畫布群組元件</param>
    /// <param name="fadeIn">是否淡出</param>
    /// <param name="interval">淡入淡出時間</param>
    //static 靜態關鍵字不用掛在物件上即可使用
    //呼叫static方法: FadeSystem.Fade(參數);
    public static IEnumerator Fade(CanvasGroup group, bool fadeIn = true, float interval = 0.03f)
    {
        var increase = fadeIn ? +0.1f : -0.1f;           //判斷是否淡入，決定透明度增減

        for (int i = 0; i < 10; i++)
        {
            group.alpha += increase;                        //透明度增加或減少
            yield return new WaitForSeconds(interval);      //等待指定時間
        }

        group.interactable = fadeIn;                        //淡入後可互動，淡出後不可互動
        group.blocksRaycasts = fadeIn;                      //淡入後阻擋射線，淡出後不阻擋射線
    }
}
