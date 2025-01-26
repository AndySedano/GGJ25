using UnityEngine;
using UnityEngine.UI;

public class UI_RequiredTool : MonoBehaviour
{
    public Sprite[] toolSprites;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }


    public void ChangeTool(CleaningTool tool)
    {
        image.sprite = toolSprites[(int)tool];
    }
}
