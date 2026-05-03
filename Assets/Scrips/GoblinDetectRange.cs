using UnityEngine;

public class GoblinDetectRange : MonoBehaviour
{
    private Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
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
                enemy.isPlayerInTrigger = true;
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
                enemy.isPlayerInTrigger = false;
            }
        }
    }
}
