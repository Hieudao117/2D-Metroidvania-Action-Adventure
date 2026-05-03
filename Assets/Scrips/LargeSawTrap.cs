using UnityEngine;

public class LargeSawTrap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPoint = transform.position;
        rightLimit = startPoint.x + range;
        leftLimit  = startPoint.x - range;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    [SerializeField] private float moveSpeed = 4f;
    private float rightLimit;
    private float leftLimit;
    [SerializeField] private float range = 8f;
    private Vector2 startPoint;
    private bool isRight = true;

    void Move()
    {
        if(isRight)
        {
            transform.Translate(Vector2.right *  moveSpeed * Time.deltaTime);
            if(transform.position.x >  rightLimit)
            {
                isRight = false;
            }
            
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if(transform.position.x < leftLimit)
            {
                isRight=true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
           if (player != null)
           {
            player.TakeDamage(100f);
           } 
        }
        
    }
}
