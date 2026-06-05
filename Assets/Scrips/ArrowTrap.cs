using UnityEngine;

public class ArrowTrap : MonoBehaviour
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

    [SerializeField] private GameObject arrowInTrap;
    [SerializeField] private Transform arrowInTrapPoint;
    [SerializeField] private float arrowInTrapSpeed = 20f;
    public void ShotArrow()
    {
        GameObject arrow = Instantiate(arrowInTrap, arrowInTrapPoint.position, Quaternion.identity);
        ArrowInTrap arrow1 = arrow.GetComponent<ArrowInTrap>();
        Vector3 direction = new Vector3(-1, 0, 0);
        arrow1.SetMovementDirection(direction * arrowInTrapSpeed);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Active");
        }
    }
}
