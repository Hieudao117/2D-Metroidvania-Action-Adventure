using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
     public void Start()
    {
        MainMenu();
        AudioManager.Instance.PlayStartGameCLip();
        
    }

    
    void Update()
    {
        
    }
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject introMenu;
    [SerializeField] private GameObject creditMenu;

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        introMenu.SetActive(false);
        creditMenu.SetActive(false);
        Time.timeScale = 0f;
        
    }

    public void PauseMenu()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(true);
        deathMenu.SetActive(false);
        introMenu.SetActive(false);
        creditMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void DeathMenu()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(true);
        introMenu.SetActive(false);
        creditMenu.SetActive(false );
        Time.timeScale = 0f;
    }

    public void IntroMenu()
    {
        introMenu.SetActive(true);
        mainMenu.SetActive(false);
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false );
        creditMenu.SetActive(false );
        Time.timeScale = 1f;
    }

    public void CreditMenu()
    {
        introMenu.SetActive(false);
        mainMenu.SetActive(false);
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
        creditMenu.SetActive(true );
    }

    public void StartIntro()
    {
        mainMenu.SetActive(false);
        introMenu.SetActive(true);
        Time.timeScale = 1f;

        
    }
    public void StartGame()
    {
        
        
        // Tìm Bootstrapper để vào game mới
        Bootstrapper boot = FindFirstObjectByType<Bootstrapper>();
        if (boot != null)
        {
            boot.NewGame();
        }
        Time.timeScale = 1f;
        introMenu.SetActive(false) ;
    }

    public void BackGame()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Reborn()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CountinueGame()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
        AudioManager.Instance.StopStartGameClip();
        Player.isContinuing = true;

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.StartCoroutine(player.RespawnRoutine());
        }
        
    }
}
