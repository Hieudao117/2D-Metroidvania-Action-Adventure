using UnityEngine;

public class FireBallTrap : MonoBehaviour
{
    [Header("Giai đoạn 1: Bay chéo lên")]
    [SerializeField] private float upwardSpeedX = 8f;   
    [SerializeField] private float upwardSpeedY = 12f; 

    [Header("Giai đoạn 2: Rơi thẳng xuống")]
    [SerializeField] private float fallSpeed = 20f;   
    [SerializeField] private float damage = 40f;

    private Transform playerTransform;
    private bool isFalling = false;
    private float initialDirectionX = 1f; 
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;

            
            initialDirectionX = playerTransform.position.x > transform.position.x ? 1f : -1f;
        }

        Destroy(gameObject, 6f); 
    }

    void Update()
    {
        if (!isFalling)
        {
           
            Vector3 moveVector = new Vector3(upwardSpeedX * initialDirectionX, upwardSpeedY, 0f);
            transform.position += moveVector * Time.deltaTime;

            
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            
            if (playerTransform != null)
            {
                
                if ((initialDirectionX > 0 && transform.position.x >= playerTransform.position.x) ||
                    (initialDirectionX < 0 && transform.position.x <= playerTransform.position.x))
                {
                    StartFalling();
                }
            }
        }
        else
        {
            // --- GIAI ĐOẠN 2: Rơi thẳng đứng từ điểm đó xuống ---
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    private void StartFalling()
    {
        isFalling = true;

        
        if (playerTransform != null)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        }

        
        transform.rotation = Quaternion.Euler(0, 0, 270f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (fallSpeed == 0f) return;

        if (collision.CompareTag("Player") && isFalling)
        {
            fallSpeed = 0f; 
            animator.SetTrigger("Explosion");
            AudioManager.Instance.PlayExplosionClip();

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        if (collision.CompareTag("Ground") && isFalling)
        {
            fallSpeed = 0f;
            animator.SetTrigger("Explosion");
            AudioManager.Instance.PlayExplosionClip();
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}