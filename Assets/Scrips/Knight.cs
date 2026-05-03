using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private float moveSpeedInGround = 10f;
    [SerializeField] private float moveSpeedInAir = 3f;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject swordAir;
    [SerializeField] private Transform swordAirPoint;
    [SerializeField] private float swordAirSpeed = 10f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 attackRange = new Vector2(1f, 0.5f);
    [SerializeField] private float damage = 50f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 15f;
    private bool isSlide = false;


    private bool isGrounded;


    private void Awake()
    {
        // Tìm tất cả các Object có Tag là "Player" trong Scene hiện tại
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            // Nếu thấy có hơn 1 đứa, thì đứa "mới" (chính là script này) phải tự biến mất
            Destroy(gameObject);
            return;
        }

        // Nếu đây là đứa duy nhất, hãy giữ nó lại cho các màn sau
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }


    /*void Update()
    {
        
        
        Jump();
        Fall();
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        if(Input.GetMouseButtonDown(1) && isGrounded)
        {
            animator.SetTrigger("SpecialAttack");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isSlide)
        {
            isSlide = true;
            animator.SetBool("isSlide", true); // Chạy Animation lướt
                                         // Không code di chuyển ở đây, hãy để Animation Event kích hoạt
        }

        if (!isSlide)
        {
            Move(); // Chỉ cho phép di chuyển khi không đang lướt
        }





        if (isHanging)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                // Nhảy lên từ gờ
                isHanging = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetBool("isHanging", false);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // Buông tay rơi xuống
                isHanging = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                animator.SetBool("isHanging", false);
            }
        }
        else
        {
            // Chỉ chạy Move/Jump khi KHÔNG bám gờ
            Move();
            Jump();
        }
    }*/

    void Update()
    {
        // Nếu đang lướt thì không làm gì cả
        if (isSlide) return;

        if (isHanging)
        {
            // Khóa hoàn toàn các hành động khác, chỉ nghe lệnh leo lên hoặc buông gờ
            HandleLedgeActions();
        }
        else
        {
            // CHỈ chạy các hàm này khi KHÔNG bám gờ
            Jump();
            Fall();
            Move();

            // Các lệnh tấn công cũng nên để ở đây
            if (Input.GetMouseButtonDown(0)) animator.SetTrigger("Attack");

            if (Input.GetMouseButtonDown(1) && isGrounded) animator.SetTrigger("SpecialAttack");

            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                isSlide = true;
                animator.SetBool("isSlide", true);
            }
        }
    }

    // Tách riêng logic bám gờ cho sạch code
    void HandleLedgeActions()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            isHanging = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isHanging", false);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            isHanging = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("isHanging", false);
        }
    }

    public void StartDash()
    {
        
        float direction = transform.localScale.x > 0 ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0); // Đẩy nhân vật đi
        rb.gravityScale = 0; // Tắt trọng lực để lướt không bị rớt (nếu cần)
    }

    // Hàm này sẽ được gọi ở CUỐI Animation lướt
    public void EndDash()
    {
        isSlide = false;
        rb.linearVelocity = Vector2.zero; // Dừng lại ngay lập tức
        rb.gravityScale = 4; // Bật lại trọng lực (chỉnh số này theo ý bạn)
        animator.SetBool("isSlide",false);
    }



    void Move()
    {
        float inputMove = Input.GetAxisRaw("Horizontal");
        float currentSpeed = isGrounded ? moveSpeedInGround : moveSpeedInAir;
        rb.linearVelocity = new Vector2(inputMove * currentSpeed, rb.linearVelocity.y);
        if (inputMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (inputMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);

        bool isRun = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJump = !isGrounded;
        animator.SetBool("isRun", isRun);
        animator.SetBool("isJump", isJump);

    }

    void Fall()
    {
        
        bool isFall = rb.linearVelocity.y < -0.1f;
        animator.SetBool("isFall",isFall);
        
        if (Input.GetMouseButtonDown(1) && !isGrounded)
        {
            animator.SetBool("isFallAttack", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -15);
            
        }

        if (isGrounded)
        {
            animator.SetBool("isFallAttack",false);
            
            
        }

    }

    public void BaseAttack()
    {
        Collider2D[] playerHit = Physics2D.OverlapBoxAll(attackPoint.position,attackRange,0f,enemyLayer);
        foreach(Collider2D layer in playerHit)
        {
            Enemy enemy = layer.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }
    public void ActiveNoSpeed()
    {
        moveSpeedInGround = 0f;
    }
    public void UnactiveNoSpeed()
    {
        moveSpeedInGround = 10f;
    }

    public void AttackBySwordAir()
    {
        GameObject sa = Instantiate(swordAir,swordAirPoint.position, Quaternion.identity);
        SwordAir sa1 = sa.GetComponent<SwordAir>();
        float direction = transform.localScale.x>0 ? 1 : -1;
        sa1.SetMovementDirection(new Vector3(direction * swordAirSpeed, 0 ,0));
        sa.transform.localScale = new Vector3(direction, 1, 1);
    }



    [Header("Ledge Settings")]
    public bool isHanging = false;
    public float xOffset = 0.5f; // Khoảng cách ngang từ tâm đến tay
    public float yOffset = 0.8f; // Khoảng cách dọc từ tâm đến tay
    private Vector2 anchorPos;   // Lưu vị trí cái gờ vừa chạm

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ bám khi chạm vào Tag "Ledge", đang rơi xuống (velocity.y < 0) và chưa bám
        if (collision.CompareTag("Ledge") && !isGrounded && rb.linearVelocity.y < 0 && !isHanging)
        {
            SnapToLedge(collision.transform.position);
        }
    }

    void SnapToLedge(Vector3 ledgePosition)
    {
        isHanging = true;
        anchorPos = ledgePosition;

        // 1. Đóng băng vật lý để không bị rơi hoặc rung lắc
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;

        // 2. Tính toán hướng mặt (1 là phải, -1 là trái)
        float faceDir = transform.localScale.x > 0 ? 1 : -1;

        // 3. ÉP TỌA ĐỘ (Đây là phần quan trọng nhất)
        // Player phải đứng lùi lại một khoảng xOffset và thấp xuống một khoảng yOffset so với cái gờ
        transform.position = new Vector3(ledgePosition.x - (xOffset * faceDir), ledgePosition.y - yOffset, 0);

        // 4. Chạy Animation bám
        animator.SetBool("isHanging", true);
    }


}



