using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 200;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackDamage = 30f;
    public float attackCooldown =1.5f;
    private float lastAttack;
    public bool isRunning;
    public bool isRight=true;
    public float moveRange = 2.5f;
    public float moveSpeed = 3f;
    public Vector3 startPos;
    public Rigidbody2D rb;
    private Animator animator;
    public float currentHealth;

    private void Awake()
    {
        isRight = true;
    }
    void Start()
    {
        lastAttack = Time.time;
        startPos = transform.position;
        currentHealth = maxHealth;
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
        
    }
    public void Move()
    {
        if (Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer))
        {
            isRunning = false;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            if (Time.time >= lastAttack+attackCooldown) 
            {
                animator.SetTrigger("Attack");
                lastAttack = Time.time;
            }
                
        }
        else
        {
            
            if (isRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                if (transform.position.x >= startPos.x + moveRange)
                {
                    isRight = false;
                    Flip();
                }

            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                if (transform.position.x <= startPos.x - moveRange)
                {
                    isRight = true;
                    Flip();
                }
            }
            isRunning = true;
            
        }
        
        animator.SetBool("IsRunning", isRunning);
    }
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void TakeDamage(float dame)
    {
        currentHealth -= dame;
        currentHealth=Mathf.Max(currentHealth, 0);
        animator.SetTrigger("Hurt");
        if (currentHealth == 0)
            animator.SetTrigger("Die");
    }
    public void UpDateAnimation() 
    { 
        
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void SetDamage()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayer);
        foreach (Collider2D player in players)
        {
            player.GetComponent<Player_Combat>().TakeDamage(attackDamage);
        }
    }
}
