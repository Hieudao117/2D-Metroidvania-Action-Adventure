using UnityEngine;

public class Golem : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 1.5f);
    }
}
