using UnityEngine;

public class BearTrap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float damage = 50f;
    private Animator animator;


    private Player targetPlayer;
    private float originalSpeed;
    private float originalJumpForce;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                targetPlayer = player;


                animator.SetBool("isActive", true);


                targetPlayer.TakeDamage(damage);


                originalSpeed = targetPlayer.moveSpeedInGround;
                originalJumpForce = targetPlayer.jumpForce;
                targetPlayer.moveSpeedInGround = 0f;
                targetPlayer.jumpForce = 0f;
            }
        }
    }


    public void EndActive()
    {
        animator.SetBool("isActive", false);

        if (targetPlayer != null)
        {

            targetPlayer.moveSpeedInGround = originalSpeed;
            targetPlayer.jumpForce = originalJumpForce;


            targetPlayer = null;
        }
    }
}
