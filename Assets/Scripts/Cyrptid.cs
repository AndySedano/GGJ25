using System;
using System.Collections;
using MoreMountains.Feel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cyrptid : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] sprites;

    private AudioSource source;

    bool cleaningSoundPlaying = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        source.clip = SoundManager.instance.CryptidScrub;
    }

    void OnMouseEnter()
    {
        GameManager.Instance.IsMouseOverCryptid = true;
    }

    void OnMouseExit()
    {
        GameManager.Instance.IsMouseOverCryptid = false;
    }

    void OnMouseOver()
    {
        GameManager.Instance.Cleaning();
    }

    public void OnCleanlinessUpdated(CleaningTool tool, float value)
    {
        sprites[(int)tool].color = new Color(1f, 1f, 1f, 1f - value);
        
        if (!cleaningSoundPlaying)
        {
            StartCoroutine(PlayScrub());
        }
    }

    IEnumerator PlayScrub()
    {
        cleaningSoundPlaying = true;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        cleaningSoundPlaying = false;
    }
}
