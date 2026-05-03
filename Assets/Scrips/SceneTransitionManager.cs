using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine; // Sử dụng thư viện mới cho Cinemachine 3.0

public class SceneTransitionManager : MonoBehaviour
{
    // Singleton: Giúp truy cập Manager từ bất cứ đâu (ví dụ: Gate)
    public static SceneTransitionManager Instance;

    [Header("Cấu hình UI")]
    public Animator fadeAnimator; // Kéo FadeImage từ Canvas vào đây

    private void Awake()
    {
        // Kiểm tra Singleton để đảm bảo chỉ có 1 Manager duy nhất
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ Manager xuyên suốt các màn chơi
        }
        else
        {
            Destroy(gameObject); // Xóa bản sao nếu lỡ tạo thêm ở Scene khác
        }
    }

    // Hàm gọi để bắt đầu quá trình chuyển cảnh
    public void Transition(string sceneName, string spawnPointName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName, spawnPointName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName, string spawnPointName)
    {
        // 1. Hiệu ứng tối màn hình
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("StartFade");
        }

        // Đợi Animation mờ đen chạy xong (khoảng 0.6s)
        yield return new WaitForSeconds(1f);

        // 2. Tải Scene mới ngầm (Async)
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        // Đợi cho đến khi Scene tải xong hoàn toàn
        while (!op.isDone)
        {
            yield return null;
        }

        // Đợi thêm một nhịp để đảm bảo các Object ở Map mới đã khởi tạo (Awake/Start)
        yield return new WaitForEndOfFrame();

        // 3. Tìm Player (từ Scene Init sang) và Điểm Spawn ở Map mới
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawnPoint = GameObject.Find(spawnPointName);

        if (player != null && spawnPoint != null)
        {
            // Đưa Player tới đúng tọa độ của SpawnPoint
            player.transform.position = spawnPoint.transform.position;

            // 4. Cập nhật Camera mới (Cinemachine 3.0 dùng CinemachineCamera)
            CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();

            if (vcam != null)
            {
                // Gán Player làm mục tiêu theo dõi cho Camera
                vcam.Follow = player.transform;

                // Thông báo cho Cinemachine biết Player vừa "dịch chuyển tức thời" 
                // để Camera nhảy thẳng tới đó, tránh hiện tượng trượt hình (pan)
                vcam.OnTargetObjectWarped(player.transform, Vector3.zero);
            }
        }

        // 5. Làm màn hình sáng lại
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("EndFade");
        }
    }
}