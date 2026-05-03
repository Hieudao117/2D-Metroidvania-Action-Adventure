using UnityEngine;

public class ItemFollow : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;

    void Start()
    {
        // Tìm Player trong Scene
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        if (player != null)
        {
            // Luôn bay về phía Player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        // Khi chạm vào Player thì biến mất
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                Destroy(gameObject);
                player.RestoreMp(20f);
            }
        }
    }
}