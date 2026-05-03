using UnityEngine;

[CreateAssetMenu(fileName = "NewAmulet", menuName = "Inventory/Amulet")]
public class AmuletData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon; 
    [TextArea] public string description;

    [Header("Chỉ số cộng thêm")]
    public float bonusDamage;
    public float bonusHp;
    public float bonusMp;
}