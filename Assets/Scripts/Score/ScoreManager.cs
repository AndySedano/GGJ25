using System;
using UnityEngine;

public class ScoreManager
{
    DateTime startTime;
    TimeSpan timeElapsed;

    public TimeSpan HighScoreTime;
    public TimeSpan LastScoreTime;

    public int HighScoreCount;
    int CryptidsCleaned = 0;
    
    static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }
    }

    public void StartGame()
    {
        startTime = DateTime.Now;
        CryptidsCleaned = 0;
    }
    
    public void CryptidCleaned()
    {
        CryptidsCleaned++;
    }
    
    public int GetCryptidsCleaned()
    {
        return CryptidsCleaned;
    }
    
    public TimeSpan GetBestTime()
    {
        return HighScoreTime;
    }
    
    public int GetBestCount()
    {
        return HighScoreCount;
    }
    
    public void EndGame()
    {
        timeElapsed = DateTime.Now - startTime;
        
        LastScoreTime = timeElapsed;
        // check if time elasped is smaller than high score time
        if (timeElapsed < HighScoreTime && HighScoreTime != TimeSpan.Zero)
        {
            HighScoreTime = timeElapsed;
            HighScoreCount = CryptidsCleaned;
        }
        
        PauseController.instance.isPaused = 0;
        WinScreen.Instance.Win();
    }
}
