using UnityEngine;

public class ArrowInTrap : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] private float damage = 200f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDirection == Vector3.zero)
        {
            return;
        }
        transform.position += moveDirection * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }  
        }
    }
}
