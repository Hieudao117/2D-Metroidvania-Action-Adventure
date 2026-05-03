using UnityEngine;

public class FlyDemonAttackRange : MonoBehaviour
{
    private FlyEnemy flyenemy;
    void Start()
    {

        flyenemy = GetComponentInParent<FlyEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                flyenemy.isPlayerInRange = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                flyenemy.isPlayerInRange = false;
            }
        }
    }
}
