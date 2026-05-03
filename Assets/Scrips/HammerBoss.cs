using UnityEngine;

public class HammerBoss : MonoBehaviour
{
    public enum State
    {
        Idle,
        MoveToPlayer,
        PlayerNear,
        PlayerFar
    }
    public State currentState;
    private GameObject player;
    private Animator animator;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.MoveToPlayer:
                MoveToPlayerState();
                break;
            case State.PlayerNear:
                PlayerNearState();
                break;
            case State.PlayerFar:
                PlayerFarState();
                break;
        }
    }


    public bool isPlayerInTrigger;
    private void IdleState()
    {
        if(!isPlayerInTrigger)
        {
            animator.SetBool("isWalk",false);
        }
        else
        {
            currentState = State.MoveToPlayer;
        }
    }

    private void FlipWhenAttack(Vector3 positionPlayer)
    {
        Vector3 flipToPlayer = (positionPlayer.x > transform.position.x) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        transform.localScale = flipToPlayer;
    }
    [SerializeField] private float moveSpeed = 4f;
    private void MoveToPlayerState()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position,transform.position);
        
            animator.SetBool("isWalk",true);
            Vector2 vectorDirection = (player.transform.position.x > transform.position.x)? Vector2.right : Vector2.left;
            transform.Translate(vectorDirection * moveSpeed * Time.deltaTime);
            if(vectorDirection == Vector2.right)
            {
                transform.localScale = new Vector3(1,1, 1);
            }
                if(vectorDirection == Vector2.left)
            {
                transform.localScale = new Vector3(-1,1, 1);
            }
        

        if(distanceToPlayer <= 6f && distanceToPlayer >= 4.5f)
        {
            currentState = State.PlayerFar;
        }
        if(distanceToPlayer <= 2f)
        {
            currentState = State.PlayerNear;
        }
        
    }



    private void PlayerFarState()
    {
        animator.SetBool("isSpecialAttack",true );
        FlipWhenAttack(player.transform.position);
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceToPlayer <= 2f)
        {
            currentState = State.PlayerNear;
            animator.SetBool("isSpecialAttack",false );
        }
        if(distanceToPlayer < 4.5f &&  distanceToPlayer > 2f)
        {
            currentState = State.MoveToPlayer;
            
        }
    }
    private void SpinAttack()
    {
        animator.SetBool("isSpinAttack",true);
        
    }

    private void PlayerNearState()
    {
        int random = Random.Range(0, 2);
        switch(random)
        {
            case 0:
                animator.SetBool("isAttack",true);
                break;
            case 1:
                animator.SetBool("isSpinAttack", true);
                break;
        }
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceToPlayer > 2f)
        {
            currentState = State.MoveToPlayer;
            animator.SetBool("isWalk",true ) ;

        }
    }
}
