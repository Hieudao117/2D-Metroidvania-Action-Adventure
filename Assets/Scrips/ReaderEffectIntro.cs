using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // Thêm thư viện này để code ngắn gọn hơn

public class ReaderEffectIntro : MonoBehaviour
{
    [Header("Cấu hình chữ")]
    public TextMeshProUGUI textUI;
    [TextArea(3, 10)] // Cho phép nhập văn bản nhiều dòng trong Inspector
    public string fullText;
    public float delayBetweenChars = 0.01f;

    void Start()
    {
        if (textUI != null)
        {
            StartCoroutine(PlayText());
        }
        else
        {
            Debug.LogError("Bạn chưa kéo IntroText vào ô Text UI trong Inspector!");
        }
    }

    IEnumerator PlayText()
    {
        textUI.text = "";

        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(delayBetweenChars);
        }

        // Đợi thêm 2 giây để người chơi đọc xong
        yield return new WaitForSeconds(2f);

        GameMenuManager.Instance.StartGame();
        
    }
}