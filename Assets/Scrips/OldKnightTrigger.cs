using UnityEngine;

public class OldKnightTrigger : MonoBehaviour
{
    private OldKnight oldKnight;
    void Start()
    {
        oldKnight = GetComponentInParent<OldKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Knight knight = collision.GetComponent<Knight>();
        if (collision.CompareTag("Player")){
            if(knight != null)
            {
                oldKnight.isPlayerInTrigger = true;
            }
        }
        
    }

}
