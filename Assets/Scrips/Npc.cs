using UnityEngine;

public class Npc : MonoBehaviour
{
    
    void Start()
    {
       /// GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] private GameObject npcTalk;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npcTalk.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npcTalk.SetActive(false);
        }
    }

    public void CallUpgradeHp()
    {
        ResourceManager.instance.UpgradeHp();
    }
    public void CallUpgradeMp()
    {
         ResourceManager.instance.UpgradeMp();
    }
    public void CallUpgradeBaseAttack()
    {
        ResourceManager .instance.UpgradeBaseAttack();
    }
}
