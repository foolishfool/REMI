using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Image PersonImageLeft;
    public TextMeshProUGUI PersonNameLeft;

    public Image PersonImageRight;
    public TextMeshProUGUI PersonNameRight;

    public GameObject DialogueFrame;
    public Transform DialogueFramePos;
    public Transform DialogueFrameInitialPos;

    public Transform DialoguePos;
    public Image WhiteScreen;

    // Start is called before the first frame update
    void Start()
    {
   

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void NewDialoguePersonUIUpdate(int DialogueID)
    {




        if (GameController.Instance.LeftSizeShow)
        {

            PersonImageLeft.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
            PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(true);
            PersonImageLeft.gameObject.SetActive(true);
            PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
            PersonImageRight.gameObject.SetActive(false);
        }
        else
        {

            PersonImageRight.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
            PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
            PersonImageLeft.gameObject.SetActive(false);
            PersonNameRight.gameObject.transform.parent.gameObject.SetActive(true);
            PersonImageRight.gameObject.SetActive(true);
        }


    }


    public void HidePersonUI()
    {
        PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
        PersonImageLeft.gameObject.SetActive(false);
        PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
        PersonImageRight.gameObject.SetActive(false);
    }

    public string GetPersonnFileName(int dialogueID)
    {
        switch (DataHandler.Instance.AllDialogueDatas[dialogueID].PersonID)
        {
            case 1:
                return "PROFESSORN";
            case 2:
                return "TOGA";
            case 3:
                return "ATLAS";
            case 4:
                return "LUNA";
            case 5:
                return "TYRA";
            case 6:
                return "LUNA";
            case 7:
                return "ZAC";
            case 8:
                return "KODA";
            case 9:
                return "KEN";
            case 10:
                return "PROFESSORL";
            case 11:
                return "REMI";
            default:
                return "";
        }
    }


 
}
