using UnityEngine;
using UnityEngine.EventSystems;

public class SpellAttack2 : MonoBehaviour
{
    private float damage = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        MadDog madDog = collision.GetComponent<MadDog>();
        Huntress huntress = collision.GetComponent<Huntress>();
        Golux golux = collision.GetComponent<Golux>();
        FlyEnemy flyEnemy = collision.GetComponent<FlyEnemy>();
        Dragon dragon = collision.GetComponent<Dragon>();
        if (collision.CompareTag("Enemy"))

        {
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (madDog != null)
            {
                madDog.TakeDamage(damage);
            }
            if (huntress != null)
            {
                huntress.TakeDamage(damage);
            }
            if (golux != null)
            {
                golux.TakeDamage(damage);
            }
            if (flyEnemy != null)
            {
                flyEnemy.TakeDamage(damage);
            }
            if(dragon != null)
            {
                dragon.TakeDamage(damage);
            }
            
        }
    }
}
