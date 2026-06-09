using UnityEngine;

public class StoryObject : MonoBehaviour
{
    [Header("把共用的 UI 面板拖到這裡")]
    public GameObject storyUI;

    [Header("把這個故事的圖片(Sprite)依序拖進來")]
    public Sprite[] storyPages; // 改成 Sprite 陣列，代表繪本的每一頁

    private void OnMouseDown()
    {
        TriggerStory();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerStory();
        }
    }

    void TriggerStory()
    {
        // 檢查陣列裡至少有一張圖才執行
        if (StoryUIManager.Instance != null && storyUI != null && storyPages.Length > 0)
        {
            StoryUIManager.Instance.ShowUI(storyUI, storyPages);
        }
    }
}