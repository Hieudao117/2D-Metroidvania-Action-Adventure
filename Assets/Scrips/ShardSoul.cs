using UnityEngine;

public class ShardSoul : Enemy
{

    protected override void Start()
    {
        base.Start();
    }
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 1f);
    }
}
