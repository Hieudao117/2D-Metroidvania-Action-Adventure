using UnityEngine;

public class FlyDemon : FlyEnemy
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform attackPoint;
    [SerializeField] public float speedFireBall = 15f;
    [SerializeField] private float cooldown = 2f;
    private float lastShootTime;

    protected override void Start()
    {
        base.Start();
        lastShootTime = -cooldown;
    }



    protected override void AttackState()
    {
        base.AttackState();

        if (player != null)
        {
            if (Time.time > lastShootTime + cooldown)
            {
                Vector3 directionToPlayer = player.transform.position - attackPoint.position;
                directionToPlayer.Normalize();
                GameObject bullet = Instantiate(fireBall, attackPoint.position, Quaternion.identity);
                FireBall fireBallBullet = bullet.GetComponent<FireBall>();
                fireBallBullet.SetMovementDirection(directionToPlayer * speedFireBall);
                lastShootTime = Time.time;
            }

        }
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 1f);
    }
}

