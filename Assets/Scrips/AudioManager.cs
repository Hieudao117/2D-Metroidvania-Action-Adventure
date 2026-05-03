using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceRun;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioSource audioSourceShotClip;
    [SerializeField] private AudioClip baseAttackClip;
    [SerializeField] private AudioClip takeDamegeClip;
    [SerializeField] private AudioClip spell1Clip;
    [SerializeField] private AudioClip spell1Explosion;
    [SerializeField] private AudioClip spell2Clip;
    [SerializeField] private AudioSource audioSourceStartGame;
    [SerializeField] private AudioClip StartGameClip;

    public static AudioManager Instance;
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
    public void PlayRunCLip()
    {
        if (runClip == null) return;

        
        if (!audioSourceRun.isPlaying)
        {
            audioSourceRun.clip = runClip; 
            audioSourceRun.Play();
        }
    }

    public void StopRunClip()
    {
        audioSourceRun.Stop();
    }

    public void PlayBaseAttackClip()
    {
        audioSourceShotClip.PlayOneShot(baseAttackClip);
    }

    public void PlayTakeDamageClip()
    {
        audioSourceShotClip.PlayOneShot(takeDamegeClip);
    }
    public void PlaySpell1Clip()
    {
        audioSourceShotClip.PlayOneShot(spell1Clip);
    }
    public void PlaySpell1ExplosionClip()
    {
        audioSourceShotClip.PlayOneShot(spell1Explosion);
    }
    public void PlaySpell2Clip()
    {
        audioSourceShotClip.PlayOneShot(spell2Clip);
    }
    public void StopOneShot()
    {
        audioSourceShotClip.Stop();
        audioSourceShotClip.clip = null;
    }

    public void PlayStartGameCLip()
    {
        audioSourceStartGame.clip = StartGameClip;
        audioSourceStartGame.Play();
    }
    public void StopStartGameClip()
    {
        audioSourceStartGame.Stop();
    }
}
