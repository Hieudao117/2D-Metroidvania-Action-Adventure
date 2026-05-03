using UnityEngine;

public class Golux : MonoBehaviour
{
    public enum State
    {
        Idle,
        MoveToPlayer,
        BaseAttack,
        SPecialAttack,
        Heal
    }
    public State currentState;
    private Animator animator;
    private GameObject player;
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
            case State.MoveToPlayer:
                MoveToPlayerState();
                break;
            case State.BaseAttack:
                BaseAttackState(); break;
            case State.SPecialAttack:
                SpecialAttackState(); break;
            case State.Heal:
                HealState(); break;
        }
    }

    public bool isPlayerInTrigger;
    private void IdleState()
    {
        moveSpeed = 0f;
        animator.SetBool("IsWalk",false);
        if(isPlayerInTrigger )
        {
            currentState = State.MoveToPlayer;
            moveSpeed = 4f;
            animator.SetBool("IsWalk",true);
        }
    }

    [SerializeField] private float moveSpeed = 4f;

    private void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if(direction == Vector2.right)
        {
            transform.localScale = new Vector3(1,1,1);
        }

        else if(direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
    [SerializeField] private float stopDistanceToBaseAttack = 2f;
    [SerializeField] private float specialAttackCooldown = 5f;
    private float nextSpecialAttackTime = 0f;
    private void MoveToPlayerState()
    {
        Vector2 vectorDirection = (player.transform.position.x > transform.position.x)? Vector2.right : Vector2.left;
        
        Move(vectorDirection);
        if(!isPlayerInTrigger)
        {
            currentState = State.Idle;
        }
        
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceToPlayer <= stopDistanceToBaseAttack)
        {
            currentState = State.BaseAttack;
        }

        if(distanceToPlayer > stopDistanceToBaseAttack && Time.time >= nextSpecialAttackTime )
        {
            currentState = State.SPecialAttack;
        }
        if (currentHp <= 100f && Time.time > nextHealTime) 
        {
            nextHealTime = Time.time + healCooldown;
            currentState = State.Heal;
            
            animator.SetBool("isHeal", true);
        }
        


    }

    private void BaseAttackState()
    {
        animator.SetBool("isBaseAttack",true);
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceToPlayer >= stopDistanceToBaseAttack)
        {
            animator.SetBool("isBaseAttack",false);
            currentState = State.MoveToPlayer;

        }
    }

    private void SpecialAttackState()
    {
        // Chỉ kích hoạt khi Animation chưa chạy
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            animator.SetBool("isSpecialAttack", true);

            // Thiết lập thời gian cho lần dùng tiếp theo
            nextSpecialAttackTime = Time.time + specialAttackCooldown;
        }

        // Sau khi kết thúc Animation, bạn cần một cơ chế để quay lại State khác
        // Cách tốt nhất là dùng Animation Event gọi hàm kết thúc dưới đây
    }

    // Hàm này bạn đặt vào Animation Event ở cuối Clip Special Attack
    public void FinishSpecialAttack()
    {
        animator.SetBool("isSpecialAttack", false);
        currentState = State.MoveToPlayer; // Hoặc MoveToPlayer
    }

    [SerializeField] private GameObject goluxSkill;
    public void SpecialAttackPerform() 
    {
        // Lấy tọa độ của Player
        float targetxPosition = player.transform.position.x;
        Vector3 dispearance = new Vector3(targetxPosition, -27f, 0);

        
        GameObject spike = Instantiate(goluxSkill, dispearance, Quaternion.identity);

        
        
    }
    [SerializeField] private float healCooldown = 20f; 
    private float nextHealTime = 0f; 
    public void HealState()
    {
        currentHp += 200f;
        currentHp = Mathf.Min(maxHp, currentHp);
        
        animator.SetBool("isHeal", false);
        currentState = State.MoveToPlayer;

    }

    [SerializeField] private float maxHp = 300f;
    private float currentHp;
    public void TakeDamage(float damage)
    {
        currentHp-= damage;
        animator.SetTrigger("Hurt");
        currentHp = Mathf.Max(currentHp, 0);
        if(currentHp <=0)
        {
            Die();
        }
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
