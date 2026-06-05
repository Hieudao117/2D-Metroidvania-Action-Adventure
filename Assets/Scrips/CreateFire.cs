using UnityEngine;

public class CreateFire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private GameObject fireBreathStay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBreath"))
        {
            fireBreathStay.SetActive(true);
            Destroy(gameObject, 5f);
        }
    }

    
}
