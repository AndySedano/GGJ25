using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private Rect _boundingBox;

    private void Awake()
    {
        SetBoundingBoxRect(transform as RectTransform);
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _boundingBox.Contains(position);
    }

    private void SetBoundingBoxRect(RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        _boundingBox = new Rect(position, size);
    }
}