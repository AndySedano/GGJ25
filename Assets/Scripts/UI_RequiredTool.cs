using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_RequiredTool : MonoBehaviour
{
    public Sprite[] toolSprites;
    Image image;
    
    AudioSource source;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
    
        source = GetComponent<AudioSource>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        
        source.clip = SoundManager.instance.ScrubSwitch;
    }


    public void ChangeTool(CleaningTool tool)
    {
        image.sprite = toolSprites[(int)tool];
        source.Play();
    }
}
