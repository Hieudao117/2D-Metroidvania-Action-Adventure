using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Vector3 moveDirection;
    
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
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


        

        if (player != null && direction != Vector3.zero)
        {

            Vector3 targetDir = player.transform.position - transform.position;


            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;


            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(50f);
                Destroy(gameObject);
            }
        }
    }

}
