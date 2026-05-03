using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Vector3 moveDirection;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
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
            if (player != null)
            {
                player.TakeDamage(50f);

                animator.SetTrigger("Explosion");
                moveDirection = Vector3.zero;
                Destroy(gameObject,1f);
            }
        }
    }
}
