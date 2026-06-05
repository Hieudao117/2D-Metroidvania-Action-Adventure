using UnityEngine;

public class FireBreatherTrap : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isActive",true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isActive",false);
        }
    }

    [SerializeField] private GameObject fireBallTrap;
    [SerializeField] private Transform fireBallTrapPoint;
    public void Shot()
    {
        if (fireBallTrap != null && fireBallTrapPoint != null)
        {
            
            Instantiate(fireBallTrap, fireBallTrapPoint.position, Quaternion.identity);

            
        }
    }
}
