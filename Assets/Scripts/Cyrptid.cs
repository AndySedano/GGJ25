using System;
using MoreMountains.Feel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cyrptid : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] sprites;


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
    }
}
