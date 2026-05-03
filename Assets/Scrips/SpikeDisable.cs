using UnityEngine;

public class SpikeDisable : MonoBehaviour
{
    [SerializeField] private GameObject spikeCollider;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
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
                animator.SetTrigger("Disable");
                spikeCollider.SetActive(false);
            }
        }
    }
}
