using UnityEngine;

public class CellingTRap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] private GameObject CellingTrapCollision;

    public void EnableCollision()
    {
        CellingTrapCollision.SetActive(true);
    }
    public void DisableCollision()
    {
        CellingTrapCollision.SetActive(false);
    }
}
