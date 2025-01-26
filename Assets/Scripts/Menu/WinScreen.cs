using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance;
    [SerializeField] private TextMeshProUGUI LastTime;
    [SerializeField] private TextMeshProUGUI FastestTime;
    [SerializeField] private TextMeshProUGUI CryptidsCleaned;
    [SerializeField] private TextMeshProUGUI BestCryptids;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }
    
    public void Win()
    {
        LastTime.text = ScoreManager.Instance.LastScoreTime.ToString("F2");
        CryptidsCleaned.text = ScoreManager.Instance.GetCryptidsCleaned().ToString();
        
        BestCryptids.text = ScoreManager.Instance.HighScoreCount.ToString();
        FastestTime.text = ScoreManager.Instance.GetBestTime().ToString("F2");
    }
}
