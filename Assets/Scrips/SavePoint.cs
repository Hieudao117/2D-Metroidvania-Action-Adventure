using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public PlayerData pData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (other.CompareTag("Player"))
        {
            // 1. Ghi nhận vị trí lưu
            pData.lastCheckPoint = transform.position;
            pData.lastSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // 2. Hồi máu cho Player
            pData.currentHp = player.maxHp;
            pData.currentMp = player.maxMp;
            if (PlayerUI.Instance != null)
            {
                PlayerUI.Instance.UpdateHp(pData.currentHp, player.maxHp);
                PlayerUI.Instance.UpdateMp(pData.currentMp, player.maxMp);
            }

            // 3. Gọi hệ thống ghi xuống ổ cứng (xem phần 3)
            SaveSystem.SaveToFile(pData);

            Debug.Log("Đã lưu game tại: " + pData.lastSceneName);
        }
    }
}
