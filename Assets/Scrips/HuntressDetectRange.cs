using UnityEngine;

public class HuntressDetectRange : MonoBehaviour
{
    private Huntress huntress;
    void Start()
    {
        huntress = GetComponentInParent<Huntress>();
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
                huntress.isPlayerInTrigger = true;
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
                huntress.isPlayerInTrigger = false;
            }
        }
    }
}
