using UnityEngine;

public class Bubble : MonoBehaviour
{
	public Color color;
    [SerializeField]
    private float amplitude = 0.1f;

    [SerializeField] 
    private float speedUp = 0.2f;

    [SerializeField] 
    private float amplitudeSpeed = 1.5f;

    private float tempY;
    private float tempX;
    private Vector3 tempPos;

    void Start()
    {
        amplitude += Random.Range(-0.2f,0.2f);
        speedUp += Random.Range(-0.2f,0.2f);

        tempX = transform.position.x;
        tempY = transform.position.y;
    }

    void Update()
    {
        tempPos.x = tempX + amplitude * Mathf.Sin(amplitudeSpeed * Time.time);
        tempPos.y += speedUp * Time.deltaTime;
        tempPos.z = transform.position.z;
        transform.position = tempPos;
    }
}
