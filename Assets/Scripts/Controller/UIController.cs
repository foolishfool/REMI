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
    public Image PersonExpressionImageLeft;
    public TextMeshProUGUI PersonNameLeft;

    public Image PersonImageRight;
    public Image PersonExpressionImageRight;
    public TextMeshProUGUI PersonNameRight;

    public GameObject DialogueFrame;
    public Transform DialogueFramePos;
    public Transform DialogueFrameInitialPos;

    public Transform DialoguePos;

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

            PersonImageLeft.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID), typeof(Sprite)) as Sprite;
            PersonExpressionImageLeft.sprite = Resources.Load("Sprites/Expression/" + GetPersonnFileName(DialogueID)  + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
            PersonNameLeft.gameObject.SetActive(true);
            PersonImageLeft.gameObject.SetActive(true);
            PersonExpressionImageLeft.gameObject.SetActive(true);
            PersonNameRight.gameObject.SetActive(false);
            PersonImageRight.gameObject.SetActive(false);
            PersonExpressionImageRight.gameObject.SetActive(false);
        }
        else
        {

           PersonImageRight.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID), typeof(Sprite)) as Sprite;
          
            PersonExpressionImageRight.sprite = Resources.Load("Sprites/Expression/" + GetPersonnFileName(DialogueID)  + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
            PersonNameLeft.gameObject.SetActive(false);
            PersonImageLeft.gameObject.SetActive(false);
            PersonExpressionImageLeft.gameObject.SetActive(false);
            PersonNameRight.gameObject.SetActive(true);
            PersonImageRight.gameObject.SetActive(true);
            PersonExpressionImageRight.gameObject.SetActive(true);
        }


    }


    public string GetPersonnFileName(int dialogueID)
    {
        switch (DataHandler.Instance.AllDialogueDatas[dialogueID].PersonID)
        {
            case 1:
                return "ProfessorN";
            case 2:
                return "TOGA";
            case 3:
                return "Atlas";
            case 4:
                return "LUNA";
            case 5:
                return "Tyra";
            case 6:
                return "LUNA";
            case 7:
                return "Zac";
            case 8:
                return "KODA";
            case 9:
                return "Ken";
            case 10:
                return "ProfessorL";
            case 11:
                return "REMI";
            default:
                return "";
        }
    }


 
}
