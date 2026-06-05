using UnityEngine;

public class FireBreathStay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                player.TakeDamage(50f);
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            if(enemy!= null)
            {

                enemy.TakeDamage(50f);
            }
        }
    }
}
