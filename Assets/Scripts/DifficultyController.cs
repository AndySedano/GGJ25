using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    // Functions as a multiplier
    [SerializeField] private int difficulty = 1;
    int maxDifficulty = 4;
    
    [SerializeField] List<Cyrptid> UnSpawnedCryptids;
    List<Cyrptid> SpawnedCryptids = new List<Cyrptid>();
    Cyrptid currentCryptid;
    
    public static DifficultyController Instance;

    void Start()
    {
        Instance = this;

        maxDifficulty = UnSpawnedCryptids.Count;
        
        for (int x = 0; x < UnSpawnedCryptids.Count; x++)
        {
            UnSpawnedCryptids[x].gameObject.SetActive(false);
        }
        
        SpawnCryptid();
    }

    IEnumerator OnCryptidCleaned()
    {
        #if UNITY_EDITOR
        Debug.Log("Start Waiting");
        #endif
        
        yield return new WaitUntil(() => GameManager.Instance.TotalCleanliness() >= 1);
        GameManager.Instance.CleanNewCryptid();
        
        #if UNITY_EDITOR
        Debug.Log("end Waiting");
        #endif
        IncrementDifficulty();
    }
    
    public void SetMaxDifficulty(int newMax)
    {
        maxDifficulty = newMax;
    }
    
    public int GetDifficulty()
    {
        return difficulty;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void IncrementDifficulty()
    {
        if (difficulty < maxDifficulty)
        {
            difficulty++;
            SpawnCryptid();

            return;
        }
        
        ScoreManager.Instance.EndGame();
    }
    
    
    public void SpawnCryptid()
    {
        if (currentCryptid)
        {
            currentCryptid.gameObject.SetActive(false);
            #if UNITY_EDITOR
            Debug.Log("Cryptid Cleaned");
            #endif
        }
        
        int rand = Random.Range(0, UnSpawnedCryptids.Count);
        currentCryptid = UnSpawnedCryptids[rand];
        currentCryptid.gameObject.SetActive(true);
        
        UnSpawnedCryptids.RemoveAt(rand);
        SpawnedCryptids.Add(currentCryptid);
        StartCoroutine(OnCryptidCleaned());
    }
}
