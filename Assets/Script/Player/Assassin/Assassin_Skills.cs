using System.Collections;
using UnityEngine;

public class Assassin_Skills : MonoBehaviour
{
    //Skill1_Decoy
    [SerializeField] private GameObject decoyPrefab;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private GameObject ultPrefab;
    public LayerMask enemyLayer;
    private GameObject sword;
    private GameObject decoy;
    private GameObject ult;
    public float manaSkill1 = 20f;
    public float manaSkill2 = 30f;
    public float manaSkill3 = 50f;
    public Player_Combat playerCombat;



    [SerializeField] private Transform attackPos;
    [SerializeField] private Transform ultPos;
    private bool isSkill1Actived = false;
    private bool isSkill2Actived = false;
    private bool isSkillUltActived = false;
    private Animator animator;
    private Player_Movement playerMovement;
    private void Awake()
    {
        animator=GetComponent<Animator>();
        playerMovement = GetComponent<Player_Movement>();
        playerCombat = GetComponent<Player_Combat>();
        
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !isSkill1Actived && playerMovement.isGrounded)
        {
            StartCoroutine(Skill1());
        }
        if (Input.GetKeyDown(KeyCode.O) && !isSkill2Actived)
        {
            
            StartCoroutine(Skill2());
        }
        if (Input.GetKeyDown(KeyCode.I) && !isSkillUltActived)
        {

            StartCoroutine(SkillUlt());
        }
    }
    private void FixedUpdate()
    {
       
    }
    public IEnumerator Skill1()
    {
        if (playerCombat.currentMana >= manaSkill1)
        {
            isSkill1Actived = true;
            playerCombat.currentMana = Mathf.Max(playerCombat.currentMana - manaSkill1, 0);
            decoy = Instantiate(decoyPrefab, transform.position, Quaternion.identity);
            decoy.layer = LayerMask.NameToLayer("Player");
            decoy.transform.localScale = gameObject.transform.localScale;
            gameObject.layer = LayerMask.NameToLayer("Decoy");
            playerMovement.moveSpeed += 3.5f;
            yield return new WaitForSeconds(9f);
            Destroy(decoy);
            playerMovement.moveSpeed -= 3.5f;
            gameObject.layer = LayerMask.NameToLayer("Player");
            isSkill1Actived = false;
        }
        else
        {
            Debug.Log("Not enough mana. Please Charge");
            
        }
          
            
    }
    public IEnumerator Skill2()
    {
        if (playerCombat.currentMana >= manaSkill2)
        {
            animator.SetTrigger("Skill2");
            isSkill2Actived = true;
            playerCombat.currentMana = Mathf.Max(playerCombat.currentMana - manaSkill2, 0);
            sword = Instantiate(swordPrefab, attackPos.position, Quaternion.identity);
            Rigidbody2D swordrb = sword.GetComponent<Rigidbody2D>();
            swordrb.linearVelocity = new Vector3(10f * transform.localScale.x, swordrb.linearVelocity.y);
            yield return new WaitForSeconds(2.5f);
            isSkill2Actived = false;
        }
        else
        {
            Debug.Log("Not enough mana. Please Charge");
            
        }
    }
    public IEnumerator SkillUlt()
    {
        if (playerCombat.currentMana >= manaSkill3)
        {
            animator.SetTrigger("Skill2");
            isSkillUltActived = true;
            playerCombat.currentMana = Mathf.Max(playerCombat.currentMana-manaSkill3,0);
            ult = Instantiate(ultPrefab, ultPos.position, Quaternion.identity);
            if (transform.localScale.x < 0)
            {
                Vector3 scale = ult.transform.localScale;
                scale.x = -ult.transform.localScale.x;
                ult.transform.localScale = scale;
            }

            yield return new WaitForSeconds(0.5f);

            isSkillUltActived = false;
        }
        else
        {
            Debug.Log("Not enough mana. Please Charge");
            
        }

    }
    
}
