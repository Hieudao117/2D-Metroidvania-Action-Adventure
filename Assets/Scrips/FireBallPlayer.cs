using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FireBallPlayer : MonoBehaviour
{
    
    private Vector3 moveDirection;
    [SerializeField] private float damage = 100f;
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
        Enemy enemy = collision.GetComponent<Enemy>();
        MadDog madDog = collision.GetComponent<MadDog>();
        Huntress huntress = collision.GetComponent<Huntress>();
        Golux golux = collision.GetComponent<Golux>();
        FlyEnemy flyEnemy = collision.GetComponent<FlyEnemy>();
        Dragon dragon = collision.GetComponent<Dragon>();
        MechaGolem mechaGolem = collision.GetComponent<MechaGolem>();
        
        if (collision.CompareTag("Enemy"))
            
        {
            animator.SetTrigger("Explosion");
            AudioManager.Instance.PlaySpell1ExplosionClip();
            moveDirection = Vector3.zero;
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if(madDog != null)
            {
                madDog.TakeDamage(damage);
            }
            if(huntress != null)
            {
                huntress.TakeDamage(damage);
            }
            if(golux != null)
            {
                golux.TakeDamage(damage);
            }
            if(flyEnemy != null)
            {
                flyEnemy.TakeDamage(damage);
            }
            if(dragon != null)
            {
                dragon.TakeDamage(damage);
            }
            if (mechaGolem != null)
            {
                mechaGolem.TakeDamae(damage);
            }

            Destroy(gameObject, 1f);
        }
    }
}


