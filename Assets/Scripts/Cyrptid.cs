using System;
using System.Collections;
using DG.Tweening;
using MoreMountains.Feel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cyrptid : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] sprites;

    [SerializeField]
    Sprite blink;

    [SerializeField]
    Sprite happy;

    Sprite intialSprite;

    public float cleaness = 1f;


    private SpriteRenderer spriteRend;
    private AudioSource source;

    bool cleaningSoundPlaying = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        spriteRend = GetComponent<SpriteRenderer>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        source.clip = SoundManager.instance.CryptidScrub;
        intialSprite = spriteRend.sprite;
    }

    void OnMouseEnter()
    {
        GameManager.Instance.IsMouseOverCryptid = true;
        if (GameManager.Instance.activeTool.HasValue)
        {
            spriteRend.sprite = blink;
        }
    }

    void OnMouseExit()
    {
        GameManager.Instance.IsMouseOverCryptid = false;
        spriteRend.sprite = intialSprite;
    }

    void OnMouseOver()
    {
        if (GameManager.Instance.activeTool.HasValue)
        {
            GameManager.Instance.Cleaning();
        }
    }
    
    public void OnCryptidCleaned()
    {
        spriteRend.sprite = happy;
        float distance = transform.position.y + 10f;
        transform.DOMoveY(distance, 1).SetEase(Ease.InOutElastic).SetLoops(2);
    }

    public void OnCleanlinessUpdated(CleaningTool tool, float value)
    {
        sprites[(int)tool].color = new Color(1f, 1f, 1f, 1f - value);
        
        // if (!cleaningSoundPlaying)
        // {
        //     StartCoroutine(PlayScrub());
        // }
    }

    IEnumerator PlayScrub()
    {
        cleaningSoundPlaying = true;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        cleaningSoundPlaying = false;
    }
}
