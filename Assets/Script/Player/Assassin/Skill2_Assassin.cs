using UnityEngine;

public class Skill2_Assassin : MonoBehaviour
{
    
    public float damageSkill2 = 30f;
    private void Awake()
    {
        
    }
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageSkill2);
            }
        }
    }

}
