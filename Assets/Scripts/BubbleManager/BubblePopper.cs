using UnityEngine;
using UnityEngine.InputSystem;

public class BubblePopper : MonoBehaviour
{
    [SerializeField] private string layerMaskName;

    private AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        if (SoundManager.instance)
            source.clip = SoundManager.instance.BubblePop;
    }

    void OnClick()
    {
        // Debug.Log("Clicked");

        LayerMask mask = LayerMask.GetMask(layerMaskName);

        // Get the mouse position in screen space
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Create a ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Debug the ray
        Debug.DrawRay(ray.origin, ray.direction * 250, Color.blue, 2f);

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hit, 250, mask))
        {
            // Debug.Log("Hit valid: " + hit.transform.name);
            // Destroy the object hit
            Bubble b;
            hit.transform.gameObject.TryGetComponent<Bubble>(out b);

            if(b != null)
            {
                b.Explode();
            }

            // Destroy(hit.transform.gameObject);
            
            // Replenish energy
            // Color bColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
            
			source.volume *= 35f;
            source.Play();
            
        }
    }
}
