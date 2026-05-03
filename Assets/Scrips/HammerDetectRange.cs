using UnityEngine;

public class HammerDetectRange : MonoBehaviour
{
    private HammerBoss hammerBoss;
    void Start()
    {
        hammerBoss = GetComponentInParent<HammerBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                hammerBoss.isPlayerInTrigger = true;
            }
        }
    }
}
