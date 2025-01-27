using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{


    [SerializeField]
    private Bubble Bubble;

    [SerializeField] private GameObject PointA;
    [SerializeField] private GameObject PointB;

    [SerializeField]
    public float SpawnProbability = 0.2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PauseController.instance.isPaused == 0)
        {
            return;
        }
        // Generate random number between 0 and 1
        float rand = UnityEngine.Random.value;

        // If the random value is less than SpawnProbability, instantiate Bubble
        if (rand < (SpawnProbability/DifficultyController.Instance.GetDifficulty()))
        {
            var spawnPosition = Vector3.Lerp(PointA.transform.position, PointB.transform.position, UnityEngine.Random.value);
            spawnPosition.y = transform.position.y;
            var tool = GameManager.Instance.RandomCleanTool();
            Bubble a = Instantiate(Bubble, spawnPosition, Quaternion.identity);
            a.tool = tool;
            
            // hacky fix but I *really* don't have time to fix it properly
            
            
            a.SetColor(GameManager.Instance.toolColors[(int)tool]);
        }
    }
}
