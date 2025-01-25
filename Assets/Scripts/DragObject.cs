using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private DragManager _manager = null;
    private Canvas parentCanvas;
    private Vector2 startPos;

    private Vector2 _centerPoint;
    private Vector2 _worldCenterPoint => transform.TransformPoint(_centerPoint);

    [SerializeField]
    UnityEvent WhileCleaning;

    private void Awake()
    {
        _manager = GetComponentInParent<DragManager>();
        _centerPoint = (transform as RectTransform).rect.center;
        parentCanvas = GetComponentInParent<Canvas>();
        startPos = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos;
        Vector3 adjustedPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_manager.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out movePos);
        adjustedPos = parentCanvas.transform.TransformPoint(movePos);
        if (_manager.IsWithinBounds(adjustedPos))
        {
            transform.position = adjustedPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        (transform as RectTransform).DOAnchorPos(startPos, 0.1f).SetEase(Ease.OutBack);
    }
}