using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 【新增】用來偵測有沒有點到 UI

public class StoryUIManager : MonoBehaviour
{
    public static StoryUIManager Instance;

    private GameObject currentUI;
    private bool isShowing = false;
    private float openTime;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;

    public Image displayImage;
    private Sprite[] currentPages;
    private int currentPageIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    public void ShowUI(GameObject ui, Sprite[] pages)
    {
        if (isShowing) return;

        currentUI = ui;
        currentPages = pages;
        currentPageIndex = 0;

        currentUI.SetActive(true);
        isShowing = true;

        openTime = Time.unscaledTime;
        Time.timeScale = 0f;

        ShowPageAtIndex(currentPageIndex);
    }

    void ShowPageAtIndex(int index)
    {
        if (displayImage != null && currentPages != null)
        {
            displayImage.sprite = currentPages[index];
        }
    }

    public void NextPage()
    {
        if (currentPages != null && currentPageIndex < currentPages.Length - 1)
        {
            currentPageIndex++;
            ShowPageAtIndex(currentPageIndex);
        }
        else if (currentPageIndex == currentPages.Length - 1)
        {
            HideUI();
        }
    }

    public void PreviousPage()
    {
        if (currentPages != null && currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPageAtIndex(currentPageIndex);
        }
    }

    void Update()
    {
        if (!isShowing) return;
        if (Time.unscaledTime - openTime < 0.5f) return;

        // 【關鍵修復】如果游標或手指正在點擊「UI 介面」，就不要觸發全螢幕點擊！
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 如果是手機版，確認那根手指是不是點在 UI 上
            if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;
            // 如果是電腦版滑鼠
            else if (Input.touchCount == 0)
                return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.tapCount == 2) HideUI();
                else if (touch.tapCount == 1) NextPage();
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.unscaledTime - lastClickTime < doubleClickThreshold)
            {
                HideUI();
            }
            else
            {
                lastClickTime = Time.unscaledTime;
                NextPage();
            }
        }
    }

    public void HideUI()
    {
        currentUI.SetActive(false);
        isShowing = false;
        Time.timeScale = 1f;
    }
}