using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private string[] dialogOptions;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] private GameObject arrow;
    float textSpeed = 0.1f;

    private void Start()
    {
        PointerArrow();
    }

    void PrintTextForCryptid(int cryptid)
    {
        StartCoroutine( PrintDialog(dialogOptions[cryptid]) );
    }

    void PointerArrow()
    {
        arrow.transform.DOMove(new Vector3(arrow.transform.position.x, arrow.transform.position.y + 10, arrow.transform.position.z), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    
    IEnumerator PrintDialog(string dialogToPrint)
    {
        int index = 0;
        while (index < dialogToPrint.Length)
        {
            dialogText.text += dialogToPrint[index];
            index++;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
