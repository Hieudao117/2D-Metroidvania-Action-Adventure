using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        currentHp = maxHp;
    }
    public bool isPlayerInRange = false;
    private void Update()
    {
        if (isPlayerInRange)
        {
            // 1. Quản lý việc tung chiêu (bay hoặc đứng lại đánh)
            ControlBoss();

            // 2. Quản lý việc di chuyển: Chỉ đuổi theo Player nếu KHÔNG đang bay
            if (!isFlying)
            {
                HandleGroundMovement();
            }
        }
        else
        if(!isFlying) {
        
            moveSpeed = 0f;
            animator.SetBool("isWalk", false);

        }
    }
    public void Control()
    {
        int skill = Random.Range(0, 2);
        switch(skill)
        {
            case 0:
                animator.SetTrigger("BaseAttack");
                break;
            case 1:
                animator.SetTrigger("SpecialAttack");
                break;
            
        }
        
    }
    [SerializeField] private float moveSpeed = 5f;
    public void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        animator.SetBool("isWalk",true);
        if(direction == Vector2.right)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if(direction == Vector2.left)
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }

    [SerializeField] private Transform baseAttackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private float baseAttackDamage = 100f;
    public void BaseAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(baseAttackPoint.position, attackRange, layerPlayer);
        foreach (Collider2D player1 in hitPlayer)
        {
            Player player = player1.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(baseAttackDamage);
            }
        }
    }

    [SerializeField] private Transform shotFirePosition;
    [SerializeField] private GameObject fireBullet;
    [SerializeField] private float fireBulletSpeed;
    public void SpecialAttack()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - shotFirePosition.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(fireBullet, shotFirePosition.position, Quaternion.identity);
            ArmBullet fire = bullet.GetComponent < ArmBullet>();
            fire.SetMovementDirection(directionToPlayer * fireBulletSpeed);

        }
    }

    

    [SerializeField] private float maxHp = 1000f;
    private float currentHp;
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if(currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject,2f);
        GameMenuManager.Instance.CreditMenu();
    }

    [SerializeField] private float coldDownBoss = 2f;
    private float nextSkillTime;
    /*public void ColdTime()
    {
        nextSkillTime = Time.time + coldDownBoss;
        Control();
    }
    private void Boss()
    {

        moveSpeed = 5f;
        Vector2 vectorDistanceToPlayer = (player.transform.position.x > transform.position.x) ? Vector2.right : Vector2.left;
        animator.SetBool("isDie", false);
        Move(vectorDistanceToPlayer);
        if (Time.time >= nextSkillTime)
        {
            ColdTime();
        }


    }*/



    [Header("Fly Patrol Settings")]
    [SerializeField] private float flyHeight = 4f;      
    [SerializeField] private float patrolDistance = 15f; 
    [SerializeField] private float flySpeedMod = 2f;  
    private bool isFlying = false;                     

    public void StartFlyCycle()
    {
        if (!isFlying)
        {
            StartCoroutine(FlyPatrolRoutine());
        }
    }

    private IEnumerator FlyPatrolRoutine()
    {
        isFlying = true;
        animator.SetBool("isFly", true);
        animator.SetBool("isWalk", false); 

        Vector3 startPos = transform.position;
        float targetY = startPos.y + flyHeight;

       
        while (transform.position.y != targetY)
        {
            float newY = Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        
        int laps = 0;
        float leftPoint = startPos.x - patrolDistance;
        float rightPoint = startPos.x + patrolDistance;

        
        bool movingRight = (transform.localScale.x < 0);

        while (laps < 4)
        {
            float targetX = movingRight ? rightPoint : leftPoint;
            
            transform.localScale = new Vector3(movingRight ? -1 : 1, 1, 1);

            while (transform.position.x != targetX)
            {
                float newX = Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * flySpeedMod * Time.deltaTime);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                yield return null;
            }

            movingRight = !movingRight;
            laps++;
        }

        
        while (transform.position.y != startPos.y)
        {
            float newY = Mathf.MoveTowards(transform.position.y, startPos.y, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        
        isFlying = false;
        animator.SetBool("isFly", false);
    }

    [SerializeField] private float coolTimeState = 3f;
    private float nextStateTime;

    private void ControlState()
    {
        if (isFlying) return;
        int state = Random.Range(0, 3);
        switch (state)
        {
            case 0:
                
                Control();
                break;
                case 1: Control(); break;
            case 2:
                StartFlyCycle();
                break;
        }
    }
    private void ColdTimeState()
    {
        nextStateTime = Time.time + coolTimeState;
        ControlState();
    }
    private void ControlBoss()
    {
        if(Time.time >= nextStateTime)
        {
            ColdTimeState();
        }
    }
    private void HandleGroundMovement()
    {
        moveSpeed = 5f;
        
        Vector2 direction = (player.transform.position.x > transform.position.x) ? Vector2.right : Vector2.left;

        animator.SetBool("isDie", false);
        Move(direction);
    }

    
}
