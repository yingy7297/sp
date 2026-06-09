using UnityEngine;
using UnityEngine.UI; // 必須引用 UI 命名空間

public class StaticGallery : MonoBehaviour
{
    public Image displayImage;        // 畫面上顯示圖片的那個 Image 元件
    public Sprite[] imageSourceList;  // 存放所有靜態圖片的陣列
    private int currentIndex = 0;     // 目前圖片的索引值
    public Button prevButton;
    public Button nextButton;

    void Start()
    {
        UpdateDisplay(); // 初始顯示第一張
    }

    public void NextPage()
    {
        if (currentIndex < imageSourceList.Length - 1)
        {
            currentIndex++;
            UpdateDisplay();
        }
    }

    public void PreviousPage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        displayImage.sprite = imageSourceList[currentIndex];
        // 第一張時不能按上一頁，最後一張時不能按下一頁
        prevButton.interactable = (currentIndex > 0);
        nextButton.interactable = (currentIndex < imageSourceList.Length - 1);
    }
}
