using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private GameObject laserBulletCollider;
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Active()
    {
        laserBulletCollider.SetActive(true);
    }

    public void UnActive()
    {
        laserBulletCollider.SetActive(false);   
    }
}
