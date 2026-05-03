using UnityEngine;
using Unity.Cinemachine; // Đảm bảo dùng thư viện này

public class CameraAutoFollow : MonoBehaviour
{
    private CinemachineCamera vcam;

    void Awake()
    {
        
        vcam = GetComponent<CinemachineCamera>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && vcam != null)
        {
            
            vcam.Target.TrackingTarget = player.transform;

            
            vcam.OnTargetObjectWarped(player.transform, player.transform.position - vcam.transform.position);
        }
    }
}