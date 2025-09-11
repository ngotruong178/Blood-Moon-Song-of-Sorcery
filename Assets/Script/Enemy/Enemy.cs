using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 200;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackDamage = 30f;
    private Animator animator;
    private float currentHealth;
    
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Attack();
    }
    public void Move()
    {
        
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
    public void Attack()
    {
        if (Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer))
        {
            animator.SetTrigger("Attack");
        }
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
