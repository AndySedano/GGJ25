using UnityEngine;

public class PauseController : MonoBehaviour
{
    public int isPaused = 1;
    public static PauseController instance;
    [SerializeField] GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = 1;
        instance = this;
    }

    public void OnPause()
    {
        if (isPaused == 0)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }


    public void Resume()
    {
        isPaused = 1;
        
        // this doesn't break the button ( Setting is as inactive does )
        pauseMenu.SetActive(false);
        SoundManager.instance.musicToPlay = 1;
        SoundManager.instance.Play();
    }


    public void Pause()
    {
        isPaused = 0;
        pauseMenu.SetActive(true);
        SoundManager.instance.musicToPlay = 2;
        SoundManager.instance.Play();
    }
}
