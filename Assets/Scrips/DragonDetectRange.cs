using UnityEngine;

public class DragonDetectRange : MonoBehaviour
{
    private Dragon dragon;
    void Start()
    {
        dragon = GetComponentInParent<Dragon>();
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
            if(player != null)
            {
                dragon.isPlayerInRange = true;
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
                dragon.isPlayerInRange = false;
            }
        }
    }
}
