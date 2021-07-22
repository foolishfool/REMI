using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using FirebaseWebGL.Examples.Database;

public class UIController : MonoBehaviour
{
    public GameObject IntroductionText;
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
    public GameObject FinishGameUI;
    public TextMeshProUGUI Score;
    public Image BG;
    public TextMeshProUGUI UserCode;

    public List<GameObject> Crystals;

    public Transform DialoguePos;
    public List<Image> WhiteScreens = new List<Image>();
    public List<Image> BlackScreens = new List<Image>();
    public List<Sprite> BGImgaes = new List<Sprite>();

    public List<string> BGVideoNames= new List<string>();
    // Start is called before the first frame update
    void Start()
    {
       GameController.Instance.VideoTextureUpdate("Courtyard");

        Invoke("ShowIntroductionText",10f);
      
    }

    void ShowIntroductionText()
    {
        IntroductionText.SetActive(true); 
        IntroductionText.GetComponentInChildren<Button>().onClick.AddListener(() => GameController.Instance.BeginDialogue());
        GameController.Instance.StartVideoPlayer.isLooping = false;
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
                    PersonImageLeft.gameObject.SetActive(false);

                }
                else
                {
                    PersonImageLeft.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
                    PersonImageLeft.gameObject.SetActive(true);
                    PersonImageLeftBig.gameObject.SetActive(false);
                }

            }

            if (GameController.Instance.IsBigUI)
            {
                PersonNameLeftBig.text = GetPersonnFileName(DialogueID);
                PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageRightBig.gameObject.SetActive(false);


                PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
                PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageRight.gameObject.SetActive(false);

            }

            else
            {

                PersonNameLeft.text = GetPersonnFileName(DialogueID);
                PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageRight.gameObject.SetActive(false);

                PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageRightBig.gameObject.SetActive(false);
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
                    PersonImageRight.gameObject.SetActive(false);
                }
                else
                {
                    PersonImageRight.sprite = Resources.Load("Sprites/Person/" + GetPersonnFileName(DialogueID) + "_" + DataHandler.Instance.AllDialogueDatas[DialogueID].Expression, typeof(Sprite)) as Sprite;
                    PersonImageRight.gameObject.SetActive(true);
                    PersonImageRightBig.gameObject.SetActive(false);
                }


            }

            if (GameController.Instance.IsBigUI)
            {
                PersonNameRightBig.text = GetPersonnFileName(DialogueID);
                PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageLeftBig.gameObject.SetActive(false);


                PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
                PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageLeft.gameObject.SetActive(false);
            }
            else
            {
                PersonNameRight.text = GetPersonnFileName(DialogueID);
                PersonNameRight.gameObject.transform.parent.gameObject.SetActive(true);
                PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageLeft.gameObject.SetActive(false);


                PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(false);
                PersonImageLeftBig.gameObject.SetActive(false);

            }


        }

        GameController.Instance.ShowDialogueText = true;
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
        GameController.Instance.VideoTextureUpdate(BGVideoNames[GameController.Instance.CurrentSceneID + 1]);

        GameController.Instance.CurrentSceneID++;
        yield return new WaitForSeconds(1f);
        BlackScreen.DOColor(new Color(Color.black.a, Color.black.g, Color.black.b, 0), 2f);
        yield return new WaitForSeconds(2f);


        if (DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextDialogueID != 0)
        {
            GameController.Instance.ShowNextDialogue(DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextDialogueID);
        }
        else if (DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextMultipleDialogue != " ")
        {
            //show next dialogue based on previous choosen 
            GameController.Instance.CurrentDialogue.ChooseNextDialogueBasedOnPreChoosen();
        }
        else
        {
            GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
        }
           
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
            case 99:
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
                return "PROFESSORL";
            case 7:
                return "LUNA";
            case 8:
                return "KEN";
            case 9:
                return "REMI";
            case 10:
                return "VITA";
            case 11:
                return "KODA";
            default:
                return "";
        }
    }

    public void BlackScreenScreenShow(int id)
    {
        StartCoroutine(BlackScreenBehavior(id));
    }
    public IEnumerator BlackScreenBehavior(int id)
    {
        BlackScreens[id].DOColor(Color.black, 2f);
        yield return new WaitForSeconds(1f);
        BlackScreens[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(Color.white, 1f);     
        BlackScreens[id].transform.GetChild(1).gameObject.SetActive(true);
        yield break;
    }


    public void StopBlackScreenBehavior(int id)
    {
        BlackScreens[id].transform.GetChild(1).gameObject.SetActive(false);
        BlackScreens[id].DOColor(new Color(Color.black.a, Color.black.g, Color.black.b, 0), 1f).OnComplete(() =>
        {
            if (DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextMultipleDialogue != " ")
            {
                //show next dialogue based on previous choosen 
                GameController.Instance.CurrentDialogue.ChooseNextDialogueBasedOnPreChoosen();
            }
            else 
            GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
        });
            BlackScreens[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f);
    }


    public void GameReplay()
    {
        SceneManager.LoadScene(0);
    }
}
