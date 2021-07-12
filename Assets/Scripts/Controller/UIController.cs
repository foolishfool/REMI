﻿using System.Collections;
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
    public Image PersonImageLeftBig;
    public TextMeshProUGUI PersonNameLeftBig;

    public Image PersonImageRight;
    public TextMeshProUGUI PersonNameRight;
    public Image PersonImageRightBig;
    public TextMeshProUGUI PersonNameRightBig;

    public GameObject DialogueFrame;
    public GameObject DialogueFrameBig;
    public Transform DialogueFramePos;
    public Transform DialogueFramePosBig;
    public Transform DialogueFrameInitialPos;
    public Image BlackScreen;
    public Image BG;

    public List<GameObject> Crystals;

    public Transform DialoguePos;
    public List<Image> WhiteScreens = new List<Image>();
    public List<Sprite> BGImgaes = new List<Sprite>();


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
            if (GetPersonnFileName(DialogueID) != "YOU")
            {
                if (GameController.Instance.IsBigUI)
                {
                    PersonImageLeftBig.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
                    PersonImageLeftBig.gameObject.SetActive(true);

                }
                else
                {
                    PersonImageLeft.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
                    PersonImageLeft.gameObject.SetActive(true);
                }

            }

            if (GameController.Instance.IsBigUI)
            {
                PersonNameLeftBig.text = GetPersonnFileName(DialogueID);
                PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageRightBig.gameObject.SetActive(false);

            }

            else
            {

                PersonNameLeft.text = GetPersonnFileName(DialogueID);
                PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageRight.gameObject.SetActive(false);
            }

        }
        else
        {
            if (GetPersonnFileName(DialogueID) != "YOU")
            {
                if (GameController.Instance.IsBigUI)
                {

                    PersonImageRightBig.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
                    PersonImageRightBig.gameObject.SetActive(true);
                }
                else
                {
                    PersonImageRight.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
                    PersonImageRight.gameObject.SetActive(true);
                }

  
            }

            if (GameController.Instance.IsBigUI)
            {
                PersonNameRightBig.text = GetPersonnFileName(DialogueID);
                PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageLeftBig.gameObject.SetActive(false);
            }
            else
            {
                PersonNameRight.text = GetPersonnFileName(DialogueID);
                PersonNameRight.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageLeft.gameObject.SetActive(false);
            }


        }


    }


    public void HidePersonUI()
    {
        PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
        PersonImageLeft.gameObject.SetActive(false);
        PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
        PersonImageRight.gameObject.SetActive(false);
        DialogueFrame.SetActive(false);


        PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(false);
        PersonImageLeftBig.gameObject.SetActive(false);
        PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(false);
        PersonImageRightBig.gameObject.SetActive(false);
        DialogueFrameBig.SetActive(false);

    }


    public void ShowCrystals()
    {
        for (int i = 0; i < Crystals.Count; i++)
        {
            Crystals[i].SetActive(true);
        }
    }

    public void LoadNewScene()
    {
        StartCoroutine(StartLoadNewSceneBehavior());
    }

    public IEnumerator StartLoadNewSceneBehavior()
    {
        BlackScreen.DOColor(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        BG.sprite = BGImgaes[GameController.Instance.CurrentSceneID + 1];
        BlackScreen.DOColor(new Color(Color.black.a, Color.black.g, Color.black.b, 0), 2f);
        yield return new WaitForSeconds(2f);
        GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID +1);
        yield break;

   
    }

    public void HideCrystals()
    {
        for (int i = 0; i < Crystals.Count; i++)
        {
            Crystals[i].SetActive(false);
        }
    }

    public string GetPersonnFileName(int dialogueID)
    {
        switch (DataHandler.Instance.AllDialogueDatas[dialogueID].PersonID)
        {
            case 0:
                return "YOU";
            case 1:
                return "PROFESSORN";
            case 2:
                return "TOGA";
            case 3:
                return "TYRA";
            case 4:
                return "ATLAS";
            case 5:
                return "ZAC";
            case 6:
                return "LUNA";
            case 7:
                return "VITA";
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
