using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BubblePopper : MonoBehaviour
{
    [SerializeField] private string layerMaskName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnClick ()
    {
        Debug.Log("Clicked");

        LayerMask mask = LayerMask.GetMask(layerMaskName);
        
        // Get the mouse position
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.nearClipPlane; // or a custom depth
        Debug.Log(mousePosition);

        // convert mouse position from screenspace position to world space position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log( mousePosition);
        
        // Get the hit information
        RaycastHit hit;
        Physics.Raycast(mousePosition, Vector3.forward, out hit, Mathf.Infinity, mask);
        // draw the ray
        Debug.DrawRay(mousePosition, Vector3.forward * 1000, Color.blue, 100);
        
        // Remove the bubble from existance
		if (hit.transform)
		{
            Debug.Log("Hit valid");
            Destroy(hit.transform.gameObject);
        }
    }
}
