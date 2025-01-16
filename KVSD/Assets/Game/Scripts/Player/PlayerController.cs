using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float airMoveSpeed = 1f;
    public float jumpForce = 10f;
    public float reboundForce = 5f;
    public float reboundJumpForce = 4f;

    public KeyCode jumpKey = KeyCode.Space;

    [Header("Jump Trigger Controller")]
    public JumpTriggerController jumpTriggerController;
    public JumpTriggerController LeftTrigger;
    public JumpTriggerController RightTrigger;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        HandleJump();
    }

    private void Move()
    {
        if (jumpTriggerController.isTouchingBlock)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(rb.velocity.x / 1.1f + moveInput * moveSpeed, rb.velocity.y);
        }
        else {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(rb.velocity.x / 1.005f + moveInput * airMoveSpeed, rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && (jumpTriggerController.isTouchingBlock || jumpTriggerController.isTouchingSlipperyBlock))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        } else
        {
            if (Input.GetKeyDown(jumpKey) && LeftTrigger.isTouchingBlock)
            {
                rb.velocity = new Vector2(reboundForce, reboundJumpForce);
            } else if (Input.GetKeyDown(jumpKey) && RightTrigger.isTouchingBlock)
            {
                rb.velocity = new Vector2(-reboundForce, reboundJumpForce);
            }
        }
    }
}
