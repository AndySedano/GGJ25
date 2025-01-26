using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("AAA");
        // Change the scale of the button to 1.1
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the scale of the button to 1
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);

    }
}
