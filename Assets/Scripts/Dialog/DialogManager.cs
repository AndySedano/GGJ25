using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    
    [SerializeField] private string[] dialogOptions;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] private GameObject arrow;
    float textSpeed = 0.1f;

    private bool writingDialog = false;
    private int currentCryptid;
    private void Start()
    {
        instance = this;
        PointerArrow();
        
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void PrintTextForCryptid(int cryptid)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        StartCoroutine( PrintDialog(dialogOptions[cryptid]) );
        currentCryptid = cryptid;

        StartCoroutine(DelayedPuase());
    }

    IEnumerator DelayedPuase()
    {
        yield return new WaitForSeconds(0.25f);
        PauseController.instance.isPaused = 0;

    }

    void PointerArrow()
    {
        arrow.transform.DOMove(new Vector3(arrow.transform.position.x, arrow.transform.position.y + 5, arrow.transform.position.z), 0.25f).SetLoops(-1, LoopType.Yoyo);
    }
    
    IEnumerator PrintDialog(string dialogToPrint)
    {
        int index = 0;
        writingDialog = true;
        dialogText.text = "";
        while (index < dialogToPrint.Length && writingDialog)
        {
            dialogText.text += dialogToPrint[index];
            index++;
            yield return new WaitForSeconds(textSpeed);
        }

        writingDialog = false;
    }

    void DoSubmit()
    {
        if (writingDialog)
        {
            writingDialog = false;
            dialogText.text = dialogOptions[currentCryptid];
            return;
        }
        
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        PauseController.instance.isPaused = 1;
    }
}
