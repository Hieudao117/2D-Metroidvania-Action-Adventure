using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance; // Singleton để dễ gọi từ mọi nơi

    [Header("UI References")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI potionText;

    public PlayerData playerData;
    

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsePotion();
        }
    }

    public void AddCoin(int amount)
    {
        playerData.coins += amount;
        UpdateUI();
    }

    public void AddPotion(int amount)
    {
        playerData.HpFlask += amount;
        UpdateUI();
    }

    public void UsePotion()
    { 
        if (playerData.HpFlask > 0)
        {
            playerData.HpFlask--;
            
            FindAnyObjectByType<Player>().Heal(100f);
            UpdateUI();
            
        }
    }
    public void UpgradeBaseAttack()
    {
        if(playerData.coins >= 10)
        {
            playerData.coins -= 10;
            FindAnyObjectByType<Player>().UpgradeBaseAttack(50f);
            UpdateUI();
        }
    }

    public void UpgradeHp()
    {
        if(playerData.coins >= 20)
        {
            playerData.coins -= 20;
            FindAnyObjectByType<Player>().UpgradeHp(50f);
            UpdateUI();
        }
    }
    public void UpgradeMp()
    {
        if (playerData.coins >= 20)
        {
            playerData.coins -= 20;
            FindAnyObjectByType<Player>().UpgradeMp(50f);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        coinText.text = "x " + playerData.coins.ToString();
        potionText.text = "x " + playerData.HpFlask.ToString();
    }
}