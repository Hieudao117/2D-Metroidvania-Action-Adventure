using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    [SerializeField] private GameObject lightningTrapCollider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable()
    {
        lightningTrapCollider.SetActive(true);
    }
    public void Disable()
    {
        lightningTrapCollider.SetActive(false);
    }
}
