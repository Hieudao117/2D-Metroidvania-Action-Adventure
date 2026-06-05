using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance; // Singleton để dễ gọi từ mọi nơi

    [Header("UI References")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI potionText;
    public TextMeshProUGUI currenthpText;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI currentMpText;
    public TextMeshProUGUI maxMpText;

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

    
    public int UpgradeBaseAttack()
    {
        if(playerData.coins >= 5)
        {
            playerData.coins -= 5;
            FindAnyObjectByType<Player>().UpgradeBaseAttack(50f);
            UpdateUI();
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int UpgradeHp()
    {
        if(playerData.coins >= 2)
        {
            playerData.coins -= 2;
            FindAnyObjectByType<Player>().UpgradeHp(50f);
            UpdateUI();
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public int UpgradeMp()
    {
        if (playerData.coins >= 5)
        {
            playerData.coins -= 5;
            FindAnyObjectByType<Player>().UpgradeMp(50f);
            UpdateUI();
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void UpdateUI()
    {
        coinText.text = "x " + playerData.coins.ToString();
        potionText.text = "x " + playerData.HpFlask.ToString();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            Player player = playerObj.GetComponent<Player>();
            currenthpText.text = playerData.currentHp.ToString();
            maxHpText.text = player.maxHp.ToString();
            currentMpText.text = playerData.currentMp.ToString();
            maxMpText.text = player.maxMp.ToString();
        }
    }
}