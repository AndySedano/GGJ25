using UnityEngine;

public class Cyrptid : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] sprites;

    void OnMouseOver()
    {
        GameManager.Instance.Cleaning();
    }

    public void OnCleanlinessUpdated(CleaningTool tool, float value)
    {
        sprites[(int)tool].color = new Color(1f, 1f, 1f, 1f- value);
    }
}
