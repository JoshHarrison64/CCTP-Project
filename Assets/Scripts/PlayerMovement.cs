using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 5f;
    public float gravityScale = 1f;
    public float fallingGravityScale = 2f;
    public float inputSmoothTime = 0.12f;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference crouchAction;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Vector2 playerInputDirectionSmoothed;
    private float inputVelocity = 0f;
    private Rigidbody2D rb;

    [Header("Animations")]
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SceneTransition.RestoreReturnPoint(transform);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        Jump();
        if (crouchAction.action.triggered)
        {
            SceneTransition.ReturnToStoredScene();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float horizontalInput = moveAction.action.ReadValue<Vector2>().x;
        // smooth the input so it has some acceleration and deceleration
        playerInputDirectionSmoothed.x = Mathf.SmoothDamp(playerInputDirectionSmoothed.x, horizontalInput, ref inputVelocity, inputSmoothTime);

        Vector3 playerMovementVector = new Vector3(playerInputDirectionSmoothed.x, 0f, playerInputDirectionSmoothed.y) * moveSpeed * Time.deltaTime;
        rb.linearVelocityX = playerMovementVector.x * moveSpeed;

        // animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
    }

    void Jump()
    {
        if (GroundedCheck() && jumpAction.action.triggered)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // set different gravity when falling to make jump feel more fluid
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = fallingGravityScale;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
        // animator.SetBool("IsJumping", true);
    }

    void ChangeDirection()
    {
        if (playerInputDirectionSmoothed.x < -0.1f) // facing left
        {
            transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
        else if (playerInputDirectionSmoothed.x > 0.1f) // facing right
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
    }

    bool GroundedCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)        
        {
            isGrounded = true;
        }
        return isGrounded;
    }

    void OnDrawGizmosSelected() // visualize the ground check radius in editor
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
