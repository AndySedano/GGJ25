using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public CleaningTool tool;

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

    private float amplitude;
    private float speed;
    private float frequency;
    private SpriteRenderer SR;
    private Animator Anim;
    private AudioSource AS;

    private AudioSource source;
    void Awake()
    {
        Anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        AS = GetComponent<AudioSource>();
    }

    void Start()
    {
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        speed = Random.Range(minSpeed, maxSpeed);
        frequency = Random.Range(minFrequency, maxFrequency);
        
        source = GetComponent<AudioSource>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        source.clip = SoundManager.instance.BubbleSpawn;
        
        source.Play();
    }

    public void SetColor(Color c)
    {
        SR.material.color = c;
    }

    public void Explode()
    {
        Anim.SetInteger("rand", UnityEngine.Random.Range(0, 4));
        Debug.Log(Anim.GetInteger("rand"));
        Anim.SetTrigger("Explode");
        AS.Play();
        GameManager.Instance.AddToolEnergy(tool);
        Destroy(gameObject.GetComponent<SphereCollider>());
        StartCoroutine(DestroyWithDelay(0.2f, gameObject));
    }

    private IEnumerator DestroyWithDelay(float delayInSeconds, GameObject go)
    {
        yield return new WaitForSeconds(delayInSeconds);
        Destroy(go);
    }

    void Update()
    {
        float x = Mathf.Sin(frequency * Time.time) * amplitude; // calculate the sine value for X
        float y = speed + Time.deltaTime;

        transform.Translate(new Vector3(x, y, 0) * (PauseController.instance.isPaused)); // translate the object with the calculated values

        if (transform.position.y > 150)
        {
            Destroy(gameObject);
        }
    }
}
