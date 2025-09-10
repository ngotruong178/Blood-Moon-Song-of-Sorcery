using UnityEngine;

public class Player_Movement : MonoBehaviour
{
   
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int maxJump = 2;
    private Player_Combat playerCombat;
    private Animator animator;
    private Rigidbody2D rb;
    public bool isGrounded = false;
    private int jumpCount = 0;
    public bool isJumping ;
    public bool isFalling;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCombat= GetComponent<Player_Combat>();
        
    }
    void Start()
    {

    }


    void Update()
    {
        MoveHandle();
        JumpHandle();
        UpdateAnimation();
    }
    public void MoveHandle()
    {
        float move = Input.GetAxis("Horizontal");
        if (playerCombat != null && playerCombat.isAttacking || Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
            if (move != 0)
                transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }
     }
    public void JumpHandle()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.4f,groundLayer);
        if (isGrounded) {
            jumpCount = 0;
        }
        if (playerCombat != null && playerCombat.isAttacking || Input.GetKey(KeyCode.S)) 
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.K) && jumpCount < maxJump - 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
    }
    public void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded && rb.linearVelocity.y > 0.1f;
        bool isFalling = !isGrounded && rb.linearVelocity.y < -0.1f;
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsFalling", isFalling);
    }
}
