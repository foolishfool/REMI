using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int CurrentDialogueID;
  
    public Dialogue CurrentDialogue;
    public GameObject DialoguePrefab;
    public GameObject DialoguePrefabBig;
    public bool LeftSizeShow = true;
    public bool ShowOptions;
    public bool ShowEffect;
    public bool OptionClicked;
    [HideInInspector]
    public int CurrentQuestionID;
    [HideInInspector]
    public GameObject CurrentEffect;
    public int NewDialogueID;
    static GameController instance;
    public UIController UiController;
    public List<GameObject> Questions;
    public List<GameObject> Effects;
    [HideInInspector]
    public int CurrentSceneID;
    [HideInInspector]
    public bool IsBigUI;
    [HideInInspector]
    public bool IsBigOption;
    [HideInInspector]
    public bool ShowDialogueText;
    public VideoPlayer StartVideoPlayer;
    public VideoPlayer EffectVideoPlayer;
    public GameObject FrontImage;
    public GameObject GifImage;
    public RenderTexture videoRenderTexture;
    public bool IsSkipClicked;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameController)) as GameController;
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameController");
                    instance = obj.AddComponent<GameController>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        StartVideoPlayer.url = Path.Combine(Application.streamingAssetsPath, "Start Screen.mp4");
        StartVideoPlayer.playOnAwake = true;
        StartVideoPlayer.isLooping = true;
        StartVideoPlayer.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        ReArrangeBgResource();
        StartVideoPlayer.Play();

        if (GameObject.Find("UIController"))
        {
            UiController = GameObject.Find("UIController").GetComponent<UIController>();
        }
        
    }

    public void VideoTextureUpdate(string videoName)
    {

        if (videoName == "")
        {
            //uiController.VideoPlane.SetActive(false);
            UiController.BG.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        else
        {

           // StartVideoPlayer.targetMaterialRenderer  = GameObject.Find("UIController").GetComponent<UIController>().VideoPlane.GetComponent<Renderer>();
            StartVideoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName + ".mp4");
            StartVideoPlayer.isLooping = true;
             StartVideoPlayer.Play();
            if (UiController)
                UiController.BG.transform.GetChild(0).gameObject.SetActive(true);
           // uiController.VideoPlane.SetActive(true);
        }
    }


    public void EffectVideoTextureUpdate(string videoName)
    {
        EffectVideoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName + ".mp4");
        EffectVideoPlayer.isLooping = false;
        StartVideoPlayer.Pause();
        EffectVideoPlayer.Play();

    }
    // Update is called once per frame
    void Update()
    {
       // if (uiController)
       // {
       //     if (CurrentDialogue)
       //     {
       //         uiController.SkipButton.gameObject.SetActive(true);
       //     }
       //     else uiController.SkipButton.gameObject.SetActive(false);
       // }

    }


    public void PlayVideo()
    {
   
        ReArrangeBgResource();
        FrontImage.SetActive(false);
    }
    public void ShowNextDialogue(int ID)
    {
        if (!DataHandler.Instance.AllDialogueDatas.ContainsKey(ID))
        {
            return;
        }

        UiController = GameObject.Find("UIController").GetComponent<UIController>();
        //reset
        ShowOptions = false;
        OptionClicked = false;
        ShowEffect = false;
        CurrentDialogueID = ID;
        IsBigUI = false;
        IsBigOption = false;
        ShowDialogueText = false;
        GameObject newDialogue;

        if (DataHandler.Instance.AllDialogueDatas[ID].IsBigOption == 1)
            IsBigOption = true;


        if (DataHandler.Instance.AllDialogueDatas[ID].IsBigUI == 1)

             IsBigUI = true;
             

        newDialogue = Instantiate(DialoguePrefab, UiController.DialoguePos.position, Quaternion.identity);
        newDialogue.transform.parent = UiController.transform;
        newDialogue.transform.localScale = Vector3.one;
        if (CurrentDialogue)
        {
            Destroy(CurrentDialogue.gameObject);
        }
  

        CurrentDialogue = newDialogue.GetComponent<Dialogue>();
        if (DataHandler.Instance.CurrentPersonID != DataHandler.Instance.AllDialogueDatas[ID].PersonID && DataHandler.Instance.CurrentPersonID != 0)
        {
            UiController.HidePersonUI();
            if (IsBigUI)
            {
                UiController.DialogueFrameBig.transform.position = UiController.DialogueFrameInitialPos.position;
                UiController.DialogueFrame.transform.position = UiController.DialogueFrameInitialPos.position;
                UiController.DialogueFrame.SetActive(false);
                UiController.DialogueFrameBig.SetActive(true);
              
            }
            else {
                UiController.DialogueFrame.transform.DOMoveY(UiController.DialogueFrameInitialPos.position.y, 0f);
                UiController.DialogueFrame.SetActive(false);
                UiController.DialogueFrame.SetActive(true);

            }
          
        
            LeftSizeShow = !LeftSizeShow;
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;

            if (IsBigUI)
            {
                UiController.DialogueFrameBig.transform.DOMoveY(UiController.DialogueFramePosBig.position.y, 0.5f).OnComplete(() => UiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
            }
            else
            UiController.DialogueFrame.transform.DOMoveY(UiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => UiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == 0)
        {
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;

            if (IsBigUI)
            {
                UiController.DialogueFrameBig.transform.DOMoveY(UiController.DialogueFramePosBig.position.y, 0.5f).OnComplete(() => UiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
            }
            else
            UiController.DialogueFrame.transform.DOMoveY(UiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => UiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == DataHandler.Instance.AllDialogueDatas[ID].PersonID)
        {
            //only update expression
            if (IsBigUI)
            {
                UiController.DialogueFrameBig.transform.DOMoveY(UiController.DialogueFramePosBig.position.y, 0f);
                UiController.DialogueFrameBig.SetActive(true);
                UiController.DialogueFrame.SetActive(false);
            }
            else
            {
                UiController.DialogueFrame.transform.DOMoveY(UiController.DialogueFramePos.position.y, 0f);
                UiController.DialogueFrame.SetActive(true);
                UiController.DialogueFrameBig.SetActive(false);
            }
        
            UiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID);
        }

        newDialogue.GetComponent<Dialogue>().ReadDialogueData(ID);
        newDialogue.GetComponent<Dialogue>().InitializeUI();
    }


    public void ShowNextQuestion()
    {
        GameObject newQuestion = Instantiate(Questions[CurrentQuestionID], GameObject.Find(Questions[CurrentQuestionID].name + "Pos").transform.position,Quaternion.identity);
        newQuestion.transform.SetParent(UiController.transform);
        newQuestion.transform.localScale = Vector3.one;
        UiController.HidePersonUI();

    }

    public void ReArrangeBgResource()
    {
       // GifImage.GetComponent<AnimatedGifPlayer>().enabled = false;

        GifImage.GetComponent<RawImage>().texture = videoRenderTexture;
        StartVideoPlayer.Play();
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


    public void BeginDialogue()
    {
        StartCoroutine(BeginDialogueBehavior());
    }



    //called at intro info button

    public IEnumerator BeginDialogueBehavior()
    {
        UiController = GameObject.Find("UIController").GetComponent<UIController>();
         UiController.BlackScreen.DOColor(Color.black, 2f);
        //uiController.NocharacterInfo.SetActive(false);
          yield return new WaitForSeconds(2f);
         UiController.IntroductionText.SetActive(false);
         VideoTextureUpdate("Command Deck");
         yield return new WaitForSeconds(1f);
          UiController.BlackScreen.DOColor(new Color(Color.black.a, Color.black.g, Color.black.b, 0), 2f);
          yield return new WaitForSeconds(2f);
         //ShowNoCharacterText();
       // StartCoroutine(uiController.ShowScrollableHallway());
        //yield break;
        ShowNextDialogue(1);
    }

    public void ShowNoCharacterText()
    {
        UiController.NocharacterInfo.SetActive(true);
        UiController.NocharacterInfo.GetComponentInChildren<Button>().onClick.AddListener(() =>

        {
            ShowNextDialogue(1);
            UiController.NocharacterInfo.SetActive(false);
        } );
    }


    void OnLevelWasLoaded(int level)
    { 
        if (level == 1)
          VideoTextureUpdate("Courtyard");
    }

    public void SkipDialogue()
    {
        if (IsSkipClicked)
        {
         
            return;
        }
        IsSkipClicked = true;
        int nextSkiptoId = DataHandler.Instance.AllUnskipDialogueIDs[0];
        CurrentDialogue.ReadDialogueData(nextSkiptoId);
        CurrentDialogueID = nextSkiptoId;
        CurrentDialogue.NextButtonClick();
        VideoTextureUpdate(UiController.BGVideoNames[DataHandler.Instance.AllDialogueDatas[nextSkiptoId].SceneID]);
        //Debug.Log(uiController.BGVideoNames[DataHandler.Instance.AllDialogueDatas[nextSkiptoId].SceneID]);
        DataHandler.Instance.AllUnskipDialogueIDs.Remove(nextSkiptoId);
        UiController.SkipButton.transform.localScale = Vector3.zero;
    }

    public void UpgradeButtonOpen()
    {
        UiController.UpgradeButton.enabled = true;
        UiController.UpgradeButton.GetComponent<Image>().sprite = UiController.UpgradeButtonSprite;  
    }

    public void GetBackHallway()
    {
        StartCoroutine(UiController.ShowScrollableHallway());
    }
}
