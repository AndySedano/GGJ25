using Unity.Mathematics;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Bubble;

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
            Instantiate(Bubble, transform.position, Quaternion.identity);
        }
    }
}
