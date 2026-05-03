using UnityEngine;

public class MadDog : MonoBehaviour
{
    public enum State
    {
        Idle,
        Attack
    }
    public State currentState;
    [SerializeField] private float attackSpeed = 2f;
    public bool isPlayerInRange = false;
    private GameObject player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Attack:
                AttackState(); break;
        }
    }
    void Move(Vector2 direction)
    {
        transform.Translate(direction * attackSpeed * Time.deltaTime);
        if(direction == Vector2.right)
        {
            transform.localScale = new Vector3(-1,1, 1);
        }
        if(direction == Vector2.left)
        {
            transform.localScale = new Vector3(1,1, 1);
        }
    }

    void IdleState()
    {
        
        
            animator.SetBool("isAttack",false);
        
        if(isPlayerInRange) 
        {
            currentState = State.Attack;
        }
    }
    void AttackState()
    {
        Vector2 vectorDirectionToPlayer = (player.transform.position.x > transform.position.x)? Vector2.right: Vector2.left;
        animator.SetBool("isAttack",true);
        Move(vectorDirectionToPlayer);
        if (!isPlayerInRange)
        {
            currentState = State.Idle;
        }
    }
    [SerializeField] private float maxHp = 200f;
    private float currentHp;
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        animator.SetTrigger("Hurt");
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp > 0)
        {
            animator.SetBool("isDie", false);

        }
        else
        {
            animator.SetBool("isDie", true);
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 1f);
    }

    [SerializeField] private float damage = 50f;
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


}
