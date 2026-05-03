using UnityEngine;

public class OldKnight : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack,
        Strengthen,
        Die
    }
    public State currentState;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameObject player;
    private Animator animator;
    public bool isPlayerInTrigger;
    [SerializeField] private float maxHp = 500f;
    private float currentHp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
            case State.Chase:
                ChaseState();
                break;
            case State.Attack: 
                AttackState();
                break;
        }
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction *  moveSpeed * Time.deltaTime);
        if(direction == Vector2.right)
        {
            transform.localScale = new Vector3(1,1, 1);
        }
        if(direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1,1, 1);
        }
    }
    private void IdleState()
    {
        
        if (isPlayerInTrigger)
        {
            currentState = State.Chase;
        }
    }

    private void ChaseState()
    {
        moveSpeed = 10f;
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        Vector2 directiontoPlayer = (player.transform.position.x > transform.position.x)? Vector2.right : Vector2.left;
        Move(directiontoPlayer);
        bool correctDistanceToPlayer = (distanceToPlayer > 0.5f)? true: false;
        if(correctDistanceToPlayer)
        {
            currentState = State.Attack;
            animator.SetBool("isAttack",correctDistanceToPlayer);
        }
        
    }
    private void AttackState()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        bool correctDistanceToPlayer = (distanceToPlayer > 0.5f)? true: false;
        if (!correctDistanceToPlayer)
        {
            animator.SetBool("isAttack", !correctDistanceToPlayer);
            currentState = State.Chase;
        }

    }
    


}
