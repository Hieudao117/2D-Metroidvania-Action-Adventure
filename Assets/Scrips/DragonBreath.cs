using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] private GameObject fireBreath;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Move();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            fireBreath.SetActive(true);
            AudioManager.Instance.PlayDragonBreathClip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fireBreath.SetActive(false);
            AudioManager.Instance.StopPlayDragonBreathClip();
            Destroy(gameObject);
        }
    }

    [SerializeField] private float moveSpeed = 4f;
    private void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); 

    }

}
