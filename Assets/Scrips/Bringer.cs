using Unity.VisualScripting;
using UnityEngine;

public class Bringer : Enemy
{
    [SerializeField] private float yOffset = 0.5f;
    [SerializeField] private GameObject magicPrefab;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 3f);
    }

    protected override void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (direction == Vector2.left)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    protected override void FLipWhenAttack(Vector3 playerPosition)
    {
        Vector3 flip = (playerPosition.x > transform.position.x) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        transform.localScale = flip;
    }
    protected override void ChaseState()
    {
        base.ChaseState();
        if (Vector2.Distance(player.transform.position, transform.position) >= 8f && Vector2.Distance(player.transform.position, transform.position) <= 10f)
        {
            animator.SetBool("isCast",true);
        }
        else
        {
            animator.SetBool("isCast",false);
        }
        
        }
    
    
    public void SummonAbovePlayer()
    {
        if (player != null)
        {
            
            Vector3 spawnPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, 0);

            
            GameObject spell = Instantiate(magicPrefab, spawnPosition, Quaternion.identity);

            
        }
    }
}
