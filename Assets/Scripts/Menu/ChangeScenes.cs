using UnityEngine;

public class ChangeScenes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SwitchScene (string sceneName)
    {
        // Load the scene with the name SceneName
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
