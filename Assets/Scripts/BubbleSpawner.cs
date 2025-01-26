using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Android;

public class BubbleSpawner : MonoBehaviour
{
    
    
    [SerializeField]
    private GameObject Bubble;

    [SerializeField] private GameObject PointA;
    [SerializeField] private GameObject PointB;

    [SerializeField]
    public float SpawnProbability = 0.2f;
    
    [SerializeField]
    // This could be different based on your game logic
    public float MainBubbleChance = 0.9f;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        // Generate random number between 0 and 1
        float rand = UnityEngine.Random.value;
        
        // If the random value is less than SpawnProbability, instantiate Bubble
        if (rand < SpawnProbability)
        {
            GameObject a = Instantiate(Bubble, Vector3.Lerp(PointA.transform.position, PointB.transform.position, UnityEngine.Random.value), Quaternion.identity);
            Color newColor = GameManager.Instance.toolColors[UnityEngine.Random.Range(0, GameManager.Instance.toolColors.Count)];
            a.GetComponent<SpriteRenderer>().color = newColor;
        }
    }
}
