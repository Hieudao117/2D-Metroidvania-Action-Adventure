using UnityEngine;

public class AmuletPickup : MonoBehaviour
{
    public string uniqueID; // Đặt tên riêng cho mỗi bùa, ví dụ: "Bua_Rung_1"
    public AmuletData data; // File ScriptableObject của bùa này
    public GameObject amuletPrefab; // Prefab "Amulet_Item" ở Phần 2

    private void Start()
    {
        if (PlayerPrefs.HasKey(uniqueID))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(uniqueID, 1);
            PlayerPrefs.Save();
            
            AmuletSlot[] allSlots = Object.FindObjectsByType<AmuletSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (AmuletSlot slot in allSlots)
            {
                if (!slot.isEquipSlot && slot.transform.childCount == 0)
                {
                    GameObject newAmulet = Instantiate(amuletPrefab, slot.transform);

                    
                    newAmulet.transform.localPosition = Vector3.zero;
                    newAmulet.transform.localScale = Vector3.one; 

                    newAmulet.GetComponent<DraggableItem>().data = this.data;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}