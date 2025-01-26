using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private DragManager _manager = null;
    private Canvas parentCanvas;
    private Vector2 startPos;

    [SerializeField]
    CleaningTool tool = CleaningTool.BRUSH;

    [SerializeField]
    Image percentageFilledImage;

    private AudioSource source;

    public void OnToolEnergyUpdated(CleaningTool tool, float value)
    {
        if (this.tool == tool)
        {
            percentageFilledImage.fillAmount = 1 - value;
        }
    }

    private void Awake()
    {
        _manager = GetComponentInParent<DragManager>();
        parentCanvas = GetComponentInParent<Canvas>();
        startPos = transform.localPosition;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PauseController.instance.isPaused > 0)
        {
            return;
        }
        
        GameManager.Instance.activeTool = tool;
        source.clip = SoundManager.instance.ScrubSwitch;
        // source.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if (PauseController.instance.isPaused > 0)
        {
            return;
        }
        Vector2 movePos;
        Vector3 adjustedPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_manager.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out movePos);
        adjustedPos = parentCanvas.transform.TransformPoint(movePos);
        if (_manager.IsWithinBounds(adjustedPos))
        {
            transform.position = adjustedPos;
        }

        if (GameManager.Instance.IsMouseOverCryptid)
        {
            GameManager.Instance.timeSinceLastClean += eventData.delta.magnitude * Time.deltaTime;
        }
    }

    private void ReturnTool()
    {
        (transform as RectTransform).DOAnchorPos(startPos, 0.1f).SetEase(Ease.OutBack);
        GameManager.Instance.DeactivateTool();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnTool();
        source.clip = SoundManager.instance.ScrubSwitch;
        // source.Play();
    }
}