using UnityEngine;
using UnityEngine.EventSystems;

public class SwordAir : MonoBehaviour
{
    private Vector3 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDirection == Vector3.zero)
        {
            return;
        }
        transform.position += moveDirection * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}
