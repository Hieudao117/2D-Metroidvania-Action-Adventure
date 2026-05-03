using UnityEngine;

public class Samurai : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 0.5f);
    }
}
