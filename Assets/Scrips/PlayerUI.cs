using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    private void Awake()
    {
        
        // Thiết lập Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    [SerializeField] private Image Hp;
    [SerializeField] private Image Mp;
    public void UpdateHp(float currentHp,float maxHP)
    {
        Hp.fillAmount = currentHp/maxHP;
    }

    public void UpdateMp(float currentMp, float maxMP)
    {
        Mp.fillAmount = currentMp / maxMP;
    }
}
