using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleSheetsForUnity;

public class UIController : MonoBehaviour
{
    public GameObject IntroductionText;
    public GameObject NocharacterInfo;
    public GameObject ScrollableHallway;
    public Image PersonImageLeft;
    public TextMeshProUGUI PersonNameLeft;
    public Image PersonImageLeftBig;
    public TextMeshProUGUI PersonNameLeftBig;
    public GameObject VideoPlane;

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
    public List<GameObject> WhiteScreens = new List<GameObject>();
    public List<Image> BlackScreens = new List<Image>();
    public RawImage Finishscreen;
    public List<Sprite> BGImgaes = new List<Sprite>();

    public List<string> BGVideoNames= new List<string>();

    private  bool isLeavingScrollableHallway;
    // Start is called before the first frame update
    void Start()
    {
      // GameController.Instance.VideoTextureUpdate("Courtyard");

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

    public void NewDialoguePersonUIUpdateBasedOnBigUI(int DialogueID )
    {
        UpdatePersonUI(DialogueID, GameController.Instance.IsBigUI);
        GameController.Instance.ShowDialogueText = true;
    }

    public void NewDialoguePersonUIUpdateBasedOnOption(int DialogueID)
    {
        UpdatePersonUI(DialogueID, GameController.Instance.IsBigOption);
    }

    public void UpdatePersonUI(int DialogueID, bool BIGUIORBIGOPTION)
    {

        if (GameController.Instance.LeftSizeShow)
        {
            if (GetPersonnFileName(DialogueID) != "YOU")
            {
                if (BIGUIORBIGOPTION)
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



                if (BIGUIORBIGOPTION)
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
                if (BIGUIORBIGOPTION)
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

            //show name
    
                if (BIGUIORBIGOPTION)
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

        if (GetPersonnFileName(DialogueID) == "NO CHARACTER")
        {
            PersonImageLeft.gameObject.SetActive(false);
            PersonImageRight.gameObject.SetActive(false);
            PersonImageLeftBig.gameObject.SetActive(false);
            PersonImageRightBig.gameObject.SetActive(false);

            PersonNameLeft.gameObject.transform.parent.gameObject.SetActive(false);
            PersonNameRight.gameObject.transform.parent.gameObject.SetActive(false);
            PersonNameLeftBig.gameObject.transform.parent.gameObject.SetActive(false);
            PersonNameRightBig.gameObject.transform.parent.gameObject.SetActive(false);
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

    public void LoadNewScene(bool fadeout)
    {
        StartCoroutine(StartLoadNewSceneBehavior(fadeout));
    }

    public void GotoNewScene(int sceneID)
    {
        if (isLeavingScrollableHallway)
        {
            return;
        }
        GameController.Instance.CurrentSceneID = sceneID;
        StartCoroutine(StartLoadNewSceneBehavior(true));
        isLeavingScrollableHallway = true;
    }


    public IEnumerator ShowScrollableHallway()
    {
        BlackScreen.DOColor(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        ScrollableHallway.SetActive(true);
        BlackScreen.DOColor(new Color(Color.black.a, Color.black.g, Color.black.b, 0), 2f);
    }
 

    public IEnumerator StartLoadNewSceneBehavior(bool fadeout)
    {
        if (fadeout)
        {
            BlackScreen.DOColor(Color.black, 2f);
            yield return new WaitForSeconds(2f);
            ScrollableHallway.SetActive(false);
            BG.sprite = BGImgaes[GameController.Instance.CurrentSceneID + 1];
            GameController.Instance.VideoTextureUpdate(BGVideoNames[GameController.Instance.CurrentSceneID + 1]);
            isLeavingScrollableHallway = false;
        }

        else
        {
            ScrollableHallway.SetActive(false);
            BG.sprite = BGImgaes[GameController.Instance.CurrentSceneID + 1];
            GameController.Instance.VideoTextureUpdate(BGVideoNames[GameController.Instance.CurrentSceneID + 1]);
            BlackScreen.DOColor(Color.black, 0f);      
        }
     

       
     //   Debug.Log(BGVideoNames[GameController.Instance.CurrentSceneID + 1]);

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
                return "PROFESSOR N";
            case 2:
                return "T.O.G.A";
            case 3:
                return "TYRA";
            case 4:
                return "ATLAS";
            case 5:
                return "ZAC";
            case 6:
                return "PROFESSOR L";
            case 7:
                return "L.U.N.A";
            case 8:
                return "KEN";
            case 9:
                return "R.E.M.I";
            case 10:
                return "VITA";
            case 11:
                return "KODA";
            case 100:
                return "NO CHARACTER";
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
        yield return new WaitForSeconds(1f);
        if (id != 2)
        {
            StopBlackScreenBehavior(id);
        }
        else
        {
            BlackScreens[id].transform.GetChild(1).gameObject.SetActive(true);
        }
      
         yield break;
    }

    public void FinishGameScreenShow()
    {

        if (DataHandler.Instance.Score >= 14)
        {
            GameController.Instance.EffectVideoTextureUpdate("Game Over - High Tier");
        }  //Medium score
        else if (DataHandler.Instance.Score >= 9 && DataHandler.Instance.Score < 14)
        {
            GameController.Instance.EffectVideoTextureUpdate("Game Over - Moderate Tier");
        }//low score
        else if (DataHandler.Instance.Score <= 8)
        {
            GameController.Instance.EffectVideoTextureUpdate("Game Over - Low Tier");
        }

       GameController.Instance.EffectVideoPlayer.isLooping = true;

        Finishscreen.DOColor(Color.white, 2f).OnComplete(()=> Finishscreen.transform.GetChild(0).gameObject.SetActive(true));


        
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

            else if (DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextDialogueID != 0)
            {
                GameController.Instance.ShowNextDialogue(DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextDialogueID);
            }
            else
            {
                GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
            }
           
        });
            BlackScreens[id].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f);
    }


    public void GameReplay()
    {
        StartCoroutine(ReplayGameBehavior());
    }

    public IEnumerator ReplayGameBehavior()
    {
        BG.GetComponent<Image>().color = Color.black;
        Finishscreen.transform.GetChild(0).gameObject.SetActive(false);
        Finishscreen.DOColor(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        GameController.Instance.VideoTextureUpdate("Start Screen");
        yield return new WaitForSeconds(0.5f);
        Destroy(GameController.Instance.gameObject);
        Destroy(DataHandler.Instance.gameObject);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
