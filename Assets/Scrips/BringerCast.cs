using UnityEngine;

public class BringerCast : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] private float damage = 100f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackRange = new Vector2(2, 2);
    [SerializeField] private LayerMask playerLayer;
    public void Attack()
    {
        Collider2D[] enemyHit = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, playerLayer);
        foreach (Collider2D layer in enemyHit)
        {
            Player player = layer.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }
}
