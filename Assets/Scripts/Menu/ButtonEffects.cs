using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the scale of the button to 1.1
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
        source.clip = SoundManager.instance.ButtonHover;
        source.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the scale of the button to 1
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        source.clip = SoundManager.instance.ButtonExit;
        source.Play();
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f);
        source.clip = SoundManager.instance.ButtonClick;
        source.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
        if (SoundManager.instance.ButtonClickRelease)
        {
            source.clip = SoundManager.instance.ButtonClickRelease;
            source.Play();
        }
    }
}
