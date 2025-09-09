using System.Collections;
using UnityEngine;

public class Assassin_Skills : MonoBehaviour
{
    //Skill1_Decoy
    [SerializeField] private GameObject decoyPrefab;
    [SerializeField] private GameObject swordPrefab;
    private GameObject sword;
    private GameObject decoy;
    [SerializeField] private Transform attackPos;
    private bool isSkill1Actived = false;
    private bool isSkill2Actived = false;
    private Animator animator;
    private Player_Movement player_Movement;
    private void Awake()
    {
        animator=GetComponent<Animator>();
        player_Movement = GetComponent<Player_Movement>();
  
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !isSkill1Actived)
        {
            StartCoroutine(Skill1());
        }
        if (Input.GetKeyDown(KeyCode.O) && !isSkill2Actived)
        {
            StartCoroutine(Skill2());
        }
    }
    private void FixedUpdate()
    {
       
    }
    public IEnumerator Skill1()
    {
        isSkill1Actived = true;
        decoy = Instantiate(decoyPrefab, transform.position, Quaternion.identity);
        decoy.layer = LayerMask.NameToLayer("Player");
        decoy.transform.localScale = gameObject.transform.localScale; 
        gameObject.layer = LayerMask.NameToLayer("Decoy");
        player_Movement.moveSpeed += 3.5f;
        yield return new WaitForSeconds(9f);
        Destroy(decoy);
        player_Movement.moveSpeed -= 3.5f;
        gameObject.layer = LayerMask.NameToLayer("Player");
        isSkill1Actived = false;
    }
    public IEnumerator Skill2()
    {
        isSkill2Actived = true;
        sword = Instantiate(swordPrefab, attackPos.position, Quaternion.identity);
        Rigidbody2D swordrb = sword.GetComponent<Rigidbody2D>();
        swordrb.linearVelocity = new Vector3( 10f* transform.localScale.x,swordrb.linearVelocity.y);
        yield return new WaitForSeconds(2.5f);
        Destroy(sword);
        isSkill2Actived= false;
    }
}
