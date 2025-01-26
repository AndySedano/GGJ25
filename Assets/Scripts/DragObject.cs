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
    CleaningTool tool = CleaningTool.A;

    [SerializeField]
    Image percentageFilledImage;

    public void OnToolEnergyUpdated(CleaningTool tool, float value)
    {
        if(this.tool == tool)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Instance.activeTool = tool;
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
        GameManager.Instance.activeTool = null;
    }
}