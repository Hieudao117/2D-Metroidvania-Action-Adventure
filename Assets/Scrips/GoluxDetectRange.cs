using UnityEngine;

public class GoluxDetectRange : MonoBehaviour
{
    private Golux golux;
    void Start()
    {
        golux = GetComponentInParent<Golux>();
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
                golux.isPlayerInTrigger = true;
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
                golux.isPlayerInTrigger = false;
            }
        }
    }
}
