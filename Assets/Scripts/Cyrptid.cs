using UnityEngine;

public class Cyrptid : MonoBehaviour
{
    public CleaningTool tool = CleaningTool.A;


    void OnMouseOver()
    {
        GameManager.Instance.Cleaning();
    }
}
