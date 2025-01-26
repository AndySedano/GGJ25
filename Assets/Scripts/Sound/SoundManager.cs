using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip ButtonHover;
    public AudioClip ButtonClick;
    public AudioClip ButtonExit;
    public AudioClip ButtonClickRelease;

    public AudioClip MenuMusic;
    public AudioClip GameMusic;
    public AudioClip PauseMusic;

    public AudioClip PauseMenuEnter;

    public AudioClip CryptidScrub;
    public AudioClip ScrubSwitch;
    public AudioClip ScrubberGrab;
    public AudioClip ScrubberRelease;
    
    public AudioClip BubblePop;
    public AudioClip BubbleSpawn;

    private AudioSource musicSource;
    
    
    [SerializeField, Tooltip("0 for Menu, 1 for game, 2 for pause")] private int musicToPlay = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        musicSource = GetComponent<AudioSource>();
        
        if (!musicSource)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        switch (musicToPlay)
        {
            case 0:
                musicSource.clip = MenuMusic;
                break;
            case 1:
                musicSource.clip = GameMusic;
                break;
            case 2:
                musicSource.clip = PauseMusic;
                break;
        }
        musicSource.loop = true;
        musicSource.Play();

    }
    
}
