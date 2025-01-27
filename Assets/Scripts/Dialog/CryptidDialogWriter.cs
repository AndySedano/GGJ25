using UnityEngine;

public class CryptidDialogWriter : MonoBehaviour
{
    [SerializeField] private int id;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DialogManager.instance.PrintTextForCryptid(id);
        Debug.Log("WREW");
    }


}
