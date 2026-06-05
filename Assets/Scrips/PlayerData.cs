using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    public float currentHp;
    public float maxHp;
    public float currentMp;
    public float maxMp;
    public float damage;
    public int coins ;
    public int HpFlask ;
    public bool isActiveHang;

    [Header("Save Point")]
    public Vector3 lastCheckPoint; // Tọa độ điểm Save cuối cùng
    public string lastSceneName;   // Tên màn chơi (Map) cuối cùng

    // Hàm reset khi bắt đầu chơi game mới (New Game)
    public void Initialize()
    {
        maxHp = 500f;
        maxMp = 200f;
        damage = 100f;
        coins = 0;
        HpFlask = 0;
        isActiveHang = false;
        

        lastCheckPoint = new Vector3(0, 0, 0);
    }
}
