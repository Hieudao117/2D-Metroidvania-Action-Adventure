using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public  enum State
    {
        Patrol,
        Chase,
        Attack,
        Back
    }
    public State currentState;
    
    
    protected float rightLimit;
    protected float leftLimit;
    protected Vector2 startPoint;
    protected Animator animator;
    protected bool isRight = true;


    /// <summary>
    protected GameObject player;
    /// </summary>
    [Header("Data System")]
    [SerializeField] protected EnemyData enemyData; 
    protected float patrolrange = 5f;
    protected float moveSpeed;
    protected float maxHp;
    protected float currentHp;
    protected float damage;
    protected float chaseRange;
    protected float stopDistanceToPlayer;
    public bool isPlayerInTrigger;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected virtual void Start()
    {
        if (enemyData != null)
        {
            moveSpeed = enemyData.moveSpeed;
            maxHp = enemyData.maxHp;
            damage = enemyData.damage;
            chaseRange = enemyData.chaseRange;
            stopDistanceToPlayer = enemyData.stopDistanceToPlayer;
            
        }
        startPoint = transform.position;
        rightLimit = startPoint.x + patrolrange;
        leftLimit = startPoint.x - patrolrange;
        currentHp = maxHp;
        UpdateHpBar();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch(currentState)
        {
            case State.Patrol:
                PatrolState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Attack: 
                AttackState();
                break;
            case State.Back:
                BackState();
                break;
        }
    }

    protected virtual void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if(direction == Vector2.right)
        {
            transform.localScale = new Vector3(1,1, 1);
        }
        if(direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1,1, 1);
        }
    }

    protected void PatrolState()
    {
        animator.SetBool("isAttack",false);
        if (isRight)
        {
            Move(Vector2.right);
            if(transform.position.x > rightLimit)
            {
                isRight = false;
            }
        }
        else
        {
            Move(Vector2.left);
            if(transform.position.x < leftLimit)
            {
                isRight=true;
            }
        }

        if (isPlayerInTrigger == true)
        {
            currentState = State.Chase;
        }
    }

    protected virtual void ChaseState()
    {
        animator.SetBool("isAttack",false );
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        float distanceToStartPoint = Vector2.Distance(transform.position, startPoint);
        Vector2 vectorDistanceToPlayer = (player.transform.position.x > transform.position.x)? Vector2.right : Vector2.left;
        Move(vectorDistanceToPlayer);
        if(distanceToStartPoint > chaseRange)
        {
            currentState = State.Back;
        }
        if(distanceToPlayer <= stopDistanceToPlayer)
        {
            currentState = State.Attack;
        }
        if(isPlayerInTrigger == false)
        {
            currentState = State.Patrol;
        }

    }
    protected virtual void FLipWhenAttack(Vector3 playerPosition)
    {
        
        Vector3 flip = (playerPosition.x > transform.position.x)? new Vector3(1,1,1) : new Vector3(-1,1,1);
        transform.localScale = flip;
    }

    protected void AttackState()
    {
        FLipWhenAttack(player.transform.position);
        animator.SetBool("isAttack", true);
        float distanceToPlayer = Vector2.Distance(player.transform.position,transform.position);
        if(distanceToPlayer > stopDistanceToPlayer )
        {
            currentState = State.Chase;
        }
        
        
        
    }
    protected void BackState()
    {
        float vectorDistanceToStartPoint = startPoint.x - transform.position.x;
        float distanceToStartPoint = Mathf.Abs(vectorDistanceToStartPoint);
        if(distanceToStartPoint <= 0.2f)
        {
            currentState = State.Patrol;
        }
        else
        {
            Vector2 vectorBack = (startPoint.x > transform.position.x) ? Vector2.right : Vector2.left;
            Move(vectorBack);
        }


    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        animator.SetTrigger("Hurt");
        UpdateHpBar();
        currentHp = Mathf.Max(currentHp, 0);
        if(currentHp > 0)
        {
            animator.SetBool("isDie",false);
            
        }
        else
        {
            animator.SetBool("isDie", true);
            Die();
        }
    }
    
    protected virtual void Die()
    {
        moveSpeed = 0f;
    }

    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackRange = new Vector2(2, 2);
    [SerializeField] private LayerMask playerLayer;
    public void Attack()
    {
        Collider2D[] enemyHit = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, playerLayer);
        foreach (Collider2D layer in enemyHit)
        {
            Player player = layer.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    [SerializeField] private Image Hp;
    private void UpdateHpBar()
    {
        if(Hp != null)
        {
            Hp.fillAmount = currentHp/maxHp;
        }
    }
    [SerializeField] private GameObject iTem;
    [SerializeField] private Transform iTemDropPoint;
    protected virtual void DropItem()
    {
        Instantiate(iTem, iTemDropPoint.position,Quaternion.identity);
    }

    [SerializeField] private GameObject souls;
    [SerializeField] private Transform soulsDropPoint;

    public void DropSouls()
    {
        Instantiate(souls,soulsDropPoint.position,Quaternion.identity);
    }

}
