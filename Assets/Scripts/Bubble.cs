using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Color color;

    [SerializeField]
    private float minAmplitude = 0.1f;
    [SerializeField]
    private float maxAmplitude = 0.5f;

    [SerializeField]
    private float minSpeed = 0.2f;
    [SerializeField]
    private float maxSpeed = 0.2f;

    [SerializeField]
    private float minFrequency = 1.5f;
    [SerializeField]
    private float maxFrequency = 1.5f;

    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float frequency;

    void Start()
    {
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        speed = Random.Range(minSpeed, maxSpeed);
        frequency = Random.Range(minFrequency, maxFrequency);
    }

    void Update()
    {
        float x = Mathf.Sin(frequency * Time.time) * amplitude; // calculate the sine value for X
        float y = speed + Time.deltaTime;

        transform.Translate(new Vector3(x, y, 0)); // translate the object with the calculated values
    }
}
