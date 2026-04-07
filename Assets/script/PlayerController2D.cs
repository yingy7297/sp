using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 15f;

    private Rigidbody2D rb;
    private float targetSpeed;
    private float currentSpeed;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // 角色翻面
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
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
