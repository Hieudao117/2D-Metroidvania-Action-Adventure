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

    [SerializeField] private GameObject noticeFalse;
    [SerializeField] private GameObject noticeSuccess;
    public void CallUpgradeHp()
    {
        int result = ResourceManager.instance.UpgradeHp();
        if(result == 0)
        {
            noticeFalse.SetActive(true) ;
        }
        if(result == 1)
        {
            noticeSuccess.SetActive(true) ;
        }
    }

    public void TurnoffNotice()
    {
        noticeFalse.SetActive(false) ;
        noticeSuccess.SetActive(false) ;
    }
    public void CallUpgradeMp()
    {
         int result =ResourceManager.instance.UpgradeMp();
        if (result == 0)
        {
            noticeFalse.SetActive(true);
        }
        if (result == 1)
        {
            noticeSuccess.SetActive(true);
        }
    }
    public void CallUpgradeBaseAttack()
    {
       int result = ResourceManager .instance.UpgradeBaseAttack();
        if (result == 0)
        {
            noticeFalse.SetActive(true);
        }
        if (result == 1)
        {
            noticeSuccess.SetActive(true);
        }
    }
}
