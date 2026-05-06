using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public enum ItemType { Coin, HpFlask }
    public ItemType type;
    public int value = 1; // Số lượng nhận được khi nhặt

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlayCollectItemClip();
            if (type == ItemType.Coin)
            {
                ResourceManager.instance.AddCoin(value);
            }
            else if (type == ItemType.HpFlask)
            {
                ResourceManager.instance.AddPotion(value);
            }

            Destroy(gameObject); // Biến mất sau khi nhặt
        }
    }
}