using UnityEngine;
using UnityEngine.UI;
public class Huntress : MonoBehaviour
{
    private Animator animator;
    private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        lastShootTime = -cooldown;
        currentHp = maxHp;
        UpdateHp();

    }
    [SerializeField] private float cooldown = 5f;
    private float lastShootTime;
    public bool isPlayerInTrigger;
    void Update()
    {
        if(isPlayerInTrigger)
        {
            if(Time.time > lastShootTime + cooldown)
            {
                FLipWhenAttack(player.transform.position);
                animator.SetTrigger("Attack");
                AudioManager.Instance.playShotArrowClip();
                lastShootTime = Time.time;
            }
            
        }
        else
        {
            animator.ResetTrigger("Attack");

        }
    }
    private void FLipWhenAttack(Vector3 playerPosition)
    {

        Vector3 flip = (playerPosition.x > transform.position.x) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        transform.localScale = flip;
    }

    [SerializeField] private float maxHp = 200f;
    private float currentHp;
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        UpdateHp();
        animator.SetTrigger("Hurt");
        currentHp = Mathf.Max(currentHp, 0);
        if(currentHp <= 0)
        {
            animator.SetBool("isDie",true);
            Die();
        }
        else
        {
            animator.SetBool("isDie",false);
        }

    }

    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float speedArrow = 10f;
    public void ShotArrow()
    {
        if (player != null)
        {
            
                Vector3 directionToPlayer = player.transform.position - shotPoint.position;
                directionToPlayer.Normalize();
                GameObject bullet = Instantiate(arrow, shotPoint.position, Quaternion.identity);
                Arrow arrowBullet = bullet.GetComponent<Arrow>();

                arrowBullet.SetMovementDirection(directionToPlayer * speedArrow);
                
            

        }
    }
    private void Die()
    {
        Destroy(gameObject, 1.5f);
    }

    [SerializeField] private Image Hp;
    private void UpdateHp()
    {
        Hp.fillAmount = currentHp/maxHp;
    }


}
