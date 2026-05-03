using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private GameObject spearTrap;

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
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                player.TakeDamage(100f);
            }
        }
    }

    public void Active()
    {
        spearTrap.SetActive(true);
    }

    public void UnActive()
    {
        spearTrap.SetActive(false);
    }
}
