using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.28f;
    private Player_Movement playerMovement;
    private bool isDashing;
    private float dashTime;
    [SerializeField] private float dashCoolDown = 0.75f;
    private float lastDash = -999f;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isAttacking;
    private bool canCombo;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<Player_Movement>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Defense();
        StartDash();
    }
    private void FixedUpdate()
    {
        Dash();
    }
    public void Attack() 
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isAttacking && !Input.GetKey(KeyCode.S))
            {
                animator.SetTrigger("Attack1");
                isAttacking = true;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            else if(canCombo)
            {
                animator.SetTrigger("Attack2");
                canCombo = false;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }
    } 
    public void SetAttacking()
    {
        isAttacking = false;
        canCombo = false;
    }
    public void SetCombo()
    {
        canCombo = true;
    }
    public void Defense()
    {
        if (Input.GetKey(KeyCode.S))
            animator.SetBool("Defense", true);
        else
            animator.SetBool("Defense", false);
    }
    public void Dash()
    {

        if (isDashing)
        {
            rb.MovePosition(rb.position + (Vector2)(transform.localScale.x * transform.right * Time.fixedDeltaTime * dashSpeed));
        }
        dashTime += Time.fixedDeltaTime;
        if (dashTime >= dashDuration)
        {
            isDashing = false;
            animator.SetBool("IsDashing", isDashing);
        }
    }
    public void StartDash()
    {
        if (Input.GetKeyDown(KeyCode.L) && playerMovement.isGrounded && !isAttacking && Time.time>=lastDash + dashCoolDown)
        {
            isDashing = true;
            dashTime = 0f;
            lastDash = Time.time;
            animator.SetTrigger("Dash");
            animator.SetBool("IsDashing", isDashing);
        }
    }


}
