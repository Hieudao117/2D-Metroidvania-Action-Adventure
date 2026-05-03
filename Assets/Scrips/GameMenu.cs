using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public PlayerData playerData;
    public void StartGame()
    {
        GameMenuManager.Instance.StartIntro();
        AudioManager.Instance.StopStartGameClip();
        PlayerPrefs.DeleteAll(); 
        PlayerPrefs.Save();
        playerData.Initialize();
        ResourceManager.instance.UpdateUI();
    }

    public void BackGame()
    {
        GameMenuManager.Instance.BackGame();
    }

    public void Reborn()
    {
        GameMenuManager.Instance.Reborn();
    }

    public void CountinueGame()
    {
        GameMenuManager.Instance.CountinueGame();
    }

    public void MainMenu()
    {
        ///SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMenuManager.Instance.Start();
        AudioManager.Instance.StopOneShot();    
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
