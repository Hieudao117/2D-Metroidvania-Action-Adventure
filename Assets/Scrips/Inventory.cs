using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject amuletInventory;
    [SerializeField] private GameObject amuletPrefab; // THÊM DÒNG NÀY: Kéo prefab Amulet_Item vào đây

    private bool isInventoryOpen = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Gọi Load khi game vừa khởi động
            LoadInventory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Test nhanh nút Save bằng phím K (Tùy chọn)
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        if (amuletInventory != null)
        {
            amuletInventory.SetActive(isInventoryOpen);
            Time.timeScale = isInventoryOpen ? 0f : 1f;
        }
    }

    public void SaveInventory()
    {
        string dataToSave = "";
        AmuletSlot[] allSlots = FindObjectsByType<AmuletSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var slot in allSlots)
        {
            if (slot.transform.childCount > 0)
            {
                DraggableItem item = slot.GetComponentInChildren<DraggableItem>();
                if (item != null && item.data != null)
                {
                    // Lưu tên của ScriptableObject
                    dataToSave += item.data.name + ",";
                }
            }
        }
        PlayerPrefs.SetString("SavedAmulets", dataToSave);
        PlayerPrefs.Save();
        Debug.Log("Đã lưu túi đồ: " + dataToSave);
    }

    public void LoadInventory()
    {
        string savedData = PlayerPrefs.GetString("SavedAmulets", "");
        if (string.IsNullOrEmpty(savedData)) return;

        // Loại bỏ các phần tử rỗng khi tách chuỗi
        string[] names = savedData.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        AmuletSlot[] allSlots = FindObjectsByType<AmuletSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        int currentSlot = 0;
        foreach (string itemName in names)
        {
            string trimmedName = itemName.Trim();
            if (string.IsNullOrEmpty(trimmedName)) continue;

            // ĐƯỜNG DẪN CHÍNH XÁC THEO HÌNH BẠN GỬI:
            AmuletData data = Resources.Load<AmuletData>("AmuletsData/" + trimmedName);

            if (data != null)
            {
                while (currentSlot < allSlots.Length)
                {
                    // Chỉ đặt vào ô thường, không đặt vào ô đang trang bị
                    if (!allSlots[currentSlot].isEquipSlot && allSlots[currentSlot].transform.childCount == 0)
                    {
                        GameObject newAmulet = Instantiate(amuletPrefab, allSlots[currentSlot].transform);
                        newAmulet.transform.localPosition = Vector3.zero;
                        newAmulet.transform.localScale = Vector3.one;
                        newAmulet.GetComponent<DraggableItem>().data = data;

                        Debug.Log("Đã hồi phục bùa: " + trimmedName);
                        break;
                    }
                    currentSlot++;
                }
            }
            else
            {
                Debug.LogError("Không tìm thấy file: " + trimmedName + " trong Resources/AmuletsData/");
            }
        }
    }
}