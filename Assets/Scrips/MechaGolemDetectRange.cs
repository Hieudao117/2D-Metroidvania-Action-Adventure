using UnityEngine;

public class MechaGolemDetectRange : MonoBehaviour
{

    private MechaGolem mechaGolem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mechaGolem = GetComponentInParent<MechaGolem>();
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
                mechaGolem.isPlayerInRange = true;

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
                mechaGolem.isPlayerInRange = false;

            }
        }
    }

}
