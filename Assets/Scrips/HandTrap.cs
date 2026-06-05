using UnityEngine;

public class HandTrap : MonoBehaviour
{
    private float rightLimit;
    private float leftLimit;
    [SerializeField] private float moveRange = 6f;
    private Vector2 startPoint;
    void Start()
    {
        startPoint = transform.position;
        rightLimit = startPoint.x + moveRange;
        leftLimit = startPoint.x - moveRange;

    }

    
    void Update()
    {
        Move();
    }
    [SerializeField] private float moveSpeed;
    private bool isRight = true;
    private void Move()
    {
        if(isRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if(transform.position.x > rightLimit)
            {
                isRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x < leftLimit)
            {
                isRight = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                player.TakeDamage(50f);
            }
        }
    }


}
