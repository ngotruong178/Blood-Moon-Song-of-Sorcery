using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public bool isAttacking;
    private bool canCombo;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    public void Attack() 
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isAttacking)
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
        
  
}
