using UnityEngine;
using UnityEngine.UI;


public class FlyEnemy : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Attack,


    }
    public State currentSatae;
    protected GameObject player;
    [SerializeField] protected float range = 6f;
    [SerializeField] protected float moveSpeed = 3f;

    protected float rightLimit;
    protected float leftLimit;
    protected Vector2 startPoint;
    protected bool movingRight = true;

    protected Animator animator;
    [SerializeField] public bool isPlayerInRange;

    [SerializeField] protected float maxHp = 200f;
    protected float currentHp;
    [SerializeField] protected float knockBackForce = 5f;

    protected Rigidbody2D rb;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Start()
    {
        startPoint = transform.position;
        rightLimit = startPoint.x + range;
        leftLimit = startPoint.x - range;
        currentHp = maxHp;
        UpdateHp();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        switch (currentSatae)
        {
            case State.Patrol:
                PatrolState();
                break;
            case State.Attack:
                AttackState();
                break;
        }
    }

    protected void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    protected virtual void FlipWhenAttack(Vector3 positionPlayer)
    {
        Vector3 flipToPlayer = (positionPlayer.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        transform.localScale = flipToPlayer;
    }

    protected virtual void PatrolState()
    {
        animator.SetBool("isAttack", false);
        if (movingRight)
        {
            Move(Vector2.right);
            if (transform.position.x > rightLimit)
            {
                movingRight = false;
            }

        }

        else
        {
            Move(Vector2.left);
            if (transform.position.x < leftLimit)
            {
                movingRight = true;
            }
        }

        if (isPlayerInRange == true)
        {
            currentSatae = State.Attack;
        }
    }

    protected virtual void AttackState()
    {
        animator.SetBool("isAttack", true);
        FlipWhenAttack(player.transform.position);
        if (isPlayerInRange == false)
        {
            currentSatae = State.Patrol;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        UpdateHp();
        currentHp = Mathf.Max(currentHp, 0);

        animator.SetTrigger("Hurt");
        if (currentHp > 0)
        {





            
            animator.SetBool("isDie", false);
        }






        else
        {


            animator.SetBool("isDie", true);
            animator.SetBool("isAttack", false);

            moveSpeed = 0f;
            Die();
        }
    }

    protected  virtual void Die()
    {

        
    }

    [SerializeField] private Image Hp;
    protected void UpdateHp()
    {
        Hp.fillAmount = currentHp / maxHp;
    }



}
