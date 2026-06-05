using UnityEngine;
using UnityEngine.EventSystems;

public class AmuletSlot : MonoBehaviour, IDropHandler
{
    public bool isEquipSlot;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem item = dropped.GetComponent<DraggableItem>();

        if (item != null && transform.childCount == 0)
        {
            // BƯỚC 1: Lấy ô cũ TRƯỚC KHI cập nhật parentAfterDrag
            AmuletSlot oldSlot = item.parentAfterDrag.GetComponent<AmuletSlot>();

            // BƯỚC 2: Nếu bùa RỜI KHỎI ô trang bị cũ -> Trừ chỉ số
            if (oldSlot != null && oldSlot.isEquipSlot)
            {
                ChangePlayerStats(item.data, false);
            }

            // BƯỚC 3: Cập nhật "nhà mới" cho bùa
            item.parentAfterDrag = transform;

            // BƯỚC 4: Nếu ô mới này LÀ ô trang bị -> Cộng chỉ số
            if (isEquipSlot)
            {
                ChangePlayerStats(item.data, true);
            }
        }
    }

    void ChangePlayerStats(AmuletData data, bool isAdding)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            Player player = playerObj.GetComponent<Player>();
            float sign = isAdding ? 1 : -1;

            
            player.pData.damage += data.bonusDamage * sign;

            
            player.maxHp += data.bonusHp * sign;
            player.pData.currentHp += data.bonusHp * sign;
            PlayerUI.Instance.UpdateHp(player.pData.currentHp,player.maxHp);
            player.maxMp += data.bonusMp * sign;
            player.pData.currentMp += data.bonusMp * sign;
            PlayerUI.Instance.UpdateMp(player.pData.currentMp,player.maxMp);
            ResourceManager.instance.UpdateUI();


            Debug.Log($"[Amulet] {(isAdding ? "Lắp" : "Tháo")} {data.itemName}. MaxHP hiện tại: {player.pData.maxHp}");
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng có Tag là 'Player'!");
        }
    }
}