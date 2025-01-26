using UnityEngine;

public class Quit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Stop the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
        // Quit the game
        Application.Quit();
        
    }
}
