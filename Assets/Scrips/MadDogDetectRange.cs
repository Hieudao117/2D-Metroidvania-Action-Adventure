using UnityEngine;

public class MadDogDetectRange : MonoBehaviour
{
    private MadDog madDog;
    void Start()
    {
        madDog = GetComponentInParent<MadDog>();
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
                madDog.isPlayerInRange = true;
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
                madDog.isPlayerInRange = false;
            }
        }
    }
}
