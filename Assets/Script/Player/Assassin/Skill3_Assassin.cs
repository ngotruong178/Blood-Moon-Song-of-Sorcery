using UnityEngine;

public class Skill3_Assassin : MonoBehaviour
{
    
    public float damageSkill3 = 100f;
    public LayerMask enemyLayer;
    public Vector2 size = new Vector2(4, 2);
    public float coolDown;
    public float skillRange=2f;
    private void Awake()
    {
        
    }
    void Start()
    {
        Destroy(gameObject, coolDown);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position,skillRange, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damageSkill3);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, skillRange);
    }

}
