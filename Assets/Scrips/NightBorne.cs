using UnityEngine;

public class NightBorne : Enemy
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform explosionPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 1f);
    }
    public void Explosion()
    {
        Instantiate(explosion, explosionPoint.position,Quaternion.identity);
    }
}
