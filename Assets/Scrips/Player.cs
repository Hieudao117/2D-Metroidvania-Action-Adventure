using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;


public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            // Nếu thấy có hơn 1 đứa, thì đứa "mới" (chính là script này) phải tự biến mất
            Destroy(gameObject);
            return;
        }

        // Nếu đây là đứa duy nhất, hãy giữ nó lại cho các màn sau
        DontDestroyOnLoad(gameObject);
        
    }

    public static bool isContinuing = false; // Biến tĩnh để kiểm soát trạng thái nạp save
    [SerializeField] public float maxHp;
    [SerializeField] public float maxMp;
    void Start()
    {
       
            // BỔ SUNG: Nếu đang bấm nút Tiếp tục game thì thoát ngay, KHÔNG reset dữ liệu nữa!
            if (isContinuing)
            {
                // Cập nhật lại thanh UI Máu/Mana theo đúng dữ liệu JSON vừa load
                PlayerUI.Instance.UpdateHp(pData.currentHp, pData.maxHp);
                PlayerUI.Instance.UpdateMp(pData.currentMp, pData.maxMp);
                return;
            }

            // Logic khởi tạo mặc định chỉ dành cho NEW GAME (Chơi mới hoàn toàn)
            pData.currentHp = pData.maxHp;
            maxHp = pData.maxHp;
            PlayerUI.Instance.UpdateHp(pData.currentHp, pData.maxHp);
            pData.currentMp = pData.maxMp;
            maxMp = pData.maxMp;
            PlayerUI.Instance.UpdateMp(pData.currentMp, pData.maxMp);
        

    }

    [SerializeField] private Vector2 wallJumpForce = new Vector2(10f, 18f); 
    void Update()
    {
       
        if(pData.isActiveHang)
        {

            Hang();

            // Nếu đang bám tường, logic Nhảy sẽ khác một chút (Wall Jump)
            if (isHang && !isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    // Nhảy bật ra khỏi tường
                    float jumpDirection = transform.localScale.x > 0 ? -1 : 1;
                    rb.linearVelocity = new Vector2(jumpDirection * wallJumpForce.x, wallJumpForce.y);

                    // Lật mặt ngay lập tức để nhìn về hướng nhảy
                    transform.localScale = new Vector3(jumpDirection, 1, 1);
                }
            }
        }

        if (Input.GetButtonDown("Attack") && !isHang && isGrounded)
        {
            if(Time.timeScale == 0f)
            {
                return;
            }
            animator.SetTrigger("BaseAttack");
            AudioManager.Instance.PlayBaseAttackClip();
        }
        if (!isGrounded && Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("isJumpAttack");
            ///AudioManager.Instance.PlayBaseAttackClip();
        }
        if (Input.GetMouseButtonDown(1) && !isHang)
        {
            Spell1();
            
        }
        if (Input.GetButtonDown("Spell2") && !isHang && isGrounded)
        {
            Spell2();
        }

        else 
        {
            Move();
            Jump();
            Fall(); 
        }
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            GameMenuManager.Instance.PauseMenu();
        }
        
       
        
        
    }

    [SerializeField] public float moveSpeedInGround = 7f;
    [SerializeField] private float moveSpeedInAir = 4f;
    private bool isGrounded;
    void Move()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }
        float inputMove = Input.GetAxisRaw("Horizontal");
        if(inputMove !=0 && isGrounded)
        {
            AudioManager.Instance.PlayRunCLip();
        }
        else
        {
            AudioManager.Instance.StopRunClip();
        }
        if (isHang && !isGrounded)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return; 
        }
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


    [SerializeField] public float jumpForce = 15f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
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

        bool isFall = rb.linearVelocity.y < -0.4f && !isGrounded;
        animator.SetBool("isFall", isFall);

    }


    

    private bool isHang;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    
    [SerializeField] private float wallSlideSpeed = 2f; // Tốc độ tụt xuống (số càng nhỏ tụt càng chậm)

    public void Hang()
    {
        // 1. Kiểm tra va chạm tường
        isHang = Physics2D.OverlapCircle(wallCheck.position, 0.4f, wallLayer);

        // 2. Điều kiện để bám tường (Giống Hollow Knight):
        // Chạm tường + Không chạm đất + Đang rơi xuống (velocity.y < 0)
        if (isHang && !isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("isHang", true);

            // KHÓA TỐC ĐỘ RƠI: 
            // Ép vận tốc Y luôn là một con số cố định (âm) để nhân vật tụt xuống đều đều
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);

            // Tắt bớt trọng lực để không bị kéo quá mạnh
            rb.gravityScale = 0;
        }
        else
        {
            // 3. Trả lại trạng thái bình thường khi không bám tường
            animator.SetBool("isHang", false);

            rb.gravityScale = 3f;
        }
    }

    private void OnDrawGizmos()
{
   
    if (groundCheck == null) return;

    
    if (isGrounded) 
        Gizmos.color = Color.red;
    else 
        Gizmos.color = Color.green;

    
    Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
}

    
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackRange = new Vector2(2,2);
    [SerializeField] private LayerMask enemyLayer;
    public void BaseAttack()
    {
        Collider2D[] playerHit = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, enemyLayer);
        foreach (Collider2D layer in playerHit)
        {
            Enemy enemy = layer.GetComponent<Enemy>();
            MadDog madDog = layer.GetComponent<MadDog>();
            Huntress huntress = layer.GetComponent<Huntress>();
            Golux golux = layer.GetComponent<Golux>();
            FlyEnemy flyEnemy = layer.GetComponent<FlyEnemy>();
            Dragon dragon = layer.GetComponent<Dragon>();
            MechaGolem mechaGolem = layer.GetComponent<MechaGolem>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(pData.damage);
            }
            if(madDog != null)
            {
                madDog.TakeDamage(pData.damage);
            }
            if(huntress != null)
            {
                huntress.TakeDamage(pData.damage);
            }
            if(golux != null)
            {
                golux.TakeDamage(pData.damage);
            }
            if(flyEnemy != null)
            {
                flyEnemy.TakeDamage(pData.damage);
            }
            if(dragon != null)
            {
                dragon.TakeDamage(pData.damage);
            }
            if (mechaGolem != null)
            {
                mechaGolem.TakeDamae(pData.damage);
            }
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    private void Spell1()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }
        if(pData.currentMp >= 20)
        {
            animator.SetTrigger("SpellAttack1");
            AudioManager.Instance.PlaySpell1Clip();
            pData.currentMp -= 20f;
            pData.currentMp = Mathf.Max(0, pData.currentMp);
            PlayerUI.Instance.UpdateMp(pData.currentMp, maxMp);
            ResourceManager.instance.UpdateUI();

        }
        else
        {
            return;
        }
    }

    private void Spell2()
    {
        if(Time.timeScale == 0f)
        {
            return;
        }
        if (pData.currentMp >= 50)
        {
            animator.SetTrigger("SpellAttack2");
            AudioManager.Instance.PlaySpell2Clip();
            pData.currentMp -= 50f;
            pData.currentMp = Mathf.Max(0, pData.currentMp);
            PlayerUI.Instance.UpdateMp(pData.currentMp, maxMp);
            ResourceManager.instance.UpdateUI();
        }
        else { return; }
    }

    
    
    public PlayerData pData;

    public void TakeDamage(float damage)
    {
        pData.currentHp -= damage;
        pData.currentHp = Mathf.Max(pData.currentHp, 0);
        PlayerUI.Instance.UpdateHp(pData.currentHp, maxHp);
        animator.SetTrigger("Hurt");
        AudioManager.Instance.PlayTakeDamageClip();
        if (pData.currentHp <= 0)
        {
            animator.SetBool("isDie",true);
            
            StartCoroutine(RespawnRoutine());
            GameMenuManager.Instance.DeathMenu();
        }
        else
        {
            animator.SetBool("isDie",false);
        }
        ResourceManager.instance.UpdateUI();
    }





    public IEnumerator RespawnRoutine()
    {
        // 1. Nạp dữ liệu từ file JSON vào pData (Để đảm bảo kể cả tắt game bật lại vẫn đúng)
        SaveSystem.LoadFromFile(pData);

        yield return new WaitForSecondsRealtime(0.1f);

        // 2. Chuyển Scene nếu tọa độ lưu nằm ở màn khác
        string savedScene = pData.lastSceneName;
        if (!string.IsNullOrEmpty(savedScene) && SceneManager.GetActiveScene().name != savedScene)
        {
            SceneManager.LoadScene(savedScene);
            yield return new WaitForEndOfFrame();
        }

        // 3. Đưa Player về vị trí lưu
        transform.position = pData.lastCheckPoint;

        
       

        // 5. Reset trạng thái
        animator.SetBool("isDie", false);
        

        // Đảm bảo thời gian chạy lại (phòng trường hợp bị Scale = 0 từ Menu)
        Time.timeScale = 1f;
        isContinuing = false;
    }

    private void Die()
    {
        Destroy(gameObject, 1f);
    }

    public void Heal(float value)
    {
        pData.currentHp += value;
        pData.currentHp = Mathf.Min(pData.currentHp,maxHp);
        PlayerUI.Instance.UpdateHp(pData.currentHp, maxHp);
    }

    public void RestoreMp(float value)
    {
        pData.currentMp += value;
        pData.currentMp = Mathf.Min(pData.currentMp, maxMp);
        PlayerUI.Instance.UpdateMp(pData.currentMp,maxMp);
    }

    [SerializeField] private GameObject fireBallPlayer;
    [SerializeField] private Transform fireBallPlayerPoint;
    [SerializeField] private float fireBallPlayerSpeed = 10f;
    /*public void SpellAttack1()
    {
        GameObject fbp = Instantiate(fireBallPlayer, fireBallPlayerPoint.position, Quaternion.identity);
        FireBallPlayer fbp1 = fbp.GetComponent<FireBallPlayer>();
        float direction = transform.localScale.x > 0 ? 1 : -1;
        fbp1.SetMovementDirection(new Vector3(direction * fireBallPlayerSpeed, 0, 0));
        fbp1.transform.localScale = new Vector3(direction, 1, 1);
    }*/
    public void SpellAttack1()
    {
        // 1. Lấy vị trí chuột trong thế giới (World Space)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Vì game 2D, chúng ta ép Z về 0 để tránh đạn bay lệch vào/ra khỏi màn hình
        mousePos.z = 0f;

        // 2. Tính toán hướng từ điểm bắn (fireBallPlayerPoint) tới chuột
        Vector3 direction = (mousePos - fireBallPlayerPoint.position).normalized;

        // 3. Tạo hỏa cầu
        GameObject fbp = Instantiate(fireBallPlayer, fireBallPlayerPoint.position, Quaternion.identity);
        FireBallPlayer fbp1 = fbp.GetComponent<FireBallPlayer>();

        // 4. Truyền vận tốc (hướng * tốc độ)
        if (fbp1 != null)
        {
            fbp1.SetMovementDirection(direction * fireBallPlayerSpeed);

            // 5. Xoay hỏa cầu nhìn về phía hướng bay (tùy chọn)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fbp.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    [SerializeField] private GameObject spellAttack2;

    public void EnableSPellAttack2()
    {
        spellAttack2.SetActive(true);
    }
    public void DisableAttack2()
    {
        spellAttack2.SetActive(false);
    }
    public void UpgradeBaseAttack(float value)
    {
        pData.damage += value;
    }

   

    

    public void UpgradeHp(float value)
    {
        pData.maxHp += value;
        maxHp = pData.maxHp;

    }
    public void UpgradeMp(float value)
    {
        pData.maxMp += value;
        maxMp = pData.maxMp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hanghand"))
        {
            pData.isActiveHang = true;
        }
    }

}
