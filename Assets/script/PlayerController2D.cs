using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 15f;

    [Header("造型設定 (放入左右兩面的圖片)")]
    public SpriteRenderer spriteRenderer; // 負責顯示圖片的組件
    public Sprite rightFacingSprite;      // 向右走時的造型
    public Sprite leftFacingSprite;       // 向左走時的造型

    private Rigidbody2D rb;
    private float targetSpeed;
    private float currentSpeed;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 如果沒有手動拖入 SpriteRenderer，程式會自動抓取同一物件上的
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // 鍵盤（Unity 測試用）
        moveInput = Input.GetAxisRaw("Horizontal");

        // 手機觸控（左右半邊控制）
        if (Input.touchCount > 0)
        {
            moveInput = 0;

            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    moveInput = -1;
                }
                else
                {
                    moveInput = 1;
                }
            }
        }

        targetSpeed = moveInput * moveSpeed;

        // --- 【修改重點：根據移動方向替換造型】 ---
        if (moveInput > 0)
        {
            // 往右走，換成右邊的造型，並確保沒有被鏡像翻轉
            spriteRenderer.sprite = rightFacingSprite;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            // 往左走，換成左邊的造型，並確保沒有被鏡像翻轉
            spriteRenderer.sprite = leftFacingSprite;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        // 平滑加減速
        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }
}
