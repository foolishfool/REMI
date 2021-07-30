using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;
using OldMoatGames;

public class GameController : MonoBehaviour
{
    public int CurrentDialogueID;
    [HideInInspector]
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
    private UIController uiController;
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
        StartVideoPlayer.Play();

        if (GameObject.Find("UIController"))
        {
            uiController = GameObject.Find("UIController").GetComponent<UIController>();
        }
        
    }

    public void VideoTextureUpdate(string videoName)
    {

        if (videoName == "")
        {
            //uiController.VideoPlane.SetActive(false);
            uiController.BG.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        else
        {

           // StartVideoPlayer.targetMaterialRenderer  = GameObject.Find("UIController").GetComponent<UIController>().VideoPlane.GetComponent<Renderer>();
            StartVideoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName + ".mp4");
            StartVideoPlayer.isLooping = true;
             StartVideoPlayer.Play();
            if (uiController)
                uiController.BG.transform.GetChild(0).gameObject.SetActive(true);
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

        uiController = GameObject.Find("UIController").GetComponent<UIController>();
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
             

         newDialogue = Instantiate(DialoguePrefab, uiController.DialoguePos.position, Quaternion.identity);
        newDialogue.transform.parent = uiController.transform;
        newDialogue.transform.localScale = Vector3.one;
        if (CurrentDialogue)
        {
            Destroy(CurrentDialogue.gameObject);
        }
  

        CurrentDialogue = newDialogue.GetComponent<Dialogue>();
        if (DataHandler.Instance.CurrentPersonID != DataHandler.Instance.AllDialogueDatas[ID].PersonID && DataHandler.Instance.CurrentPersonID != 0)
        {
            uiController.HidePersonUI();
            if (IsBigUI)
            {
                uiController.DialogueFrameBig.transform.position = uiController.DialogueFrameInitialPos.position;
                uiController.DialogueFrame.transform.position = uiController.DialogueFrameInitialPos.position;
                uiController.DialogueFrame.SetActive(false);
                uiController.DialogueFrameBig.SetActive(true);
              
            }
            else {
                uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFrameInitialPos.position.y, 0f);
                uiController.DialogueFrame.SetActive(false);
                uiController.DialogueFrame.SetActive(true);

            }
          
        
            LeftSizeShow = !LeftSizeShow;
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;

            if (IsBigUI)
            {
                uiController.DialogueFrameBig.transform.DOMoveY(uiController.DialogueFramePosBig.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
            }
            else
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == 0)
        {
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;

            if (IsBigUI)
            {
                uiController.DialogueFrameBig.transform.DOMoveY(uiController.DialogueFramePosBig.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
            }
            else
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == DataHandler.Instance.AllDialogueDatas[ID].PersonID)
        {
            //only update expression
            if (IsBigUI)
            {
                uiController.DialogueFrameBig.transform.DOMoveY(uiController.DialogueFramePosBig.position.y, 0f);
                uiController.DialogueFrameBig.SetActive(true);
                uiController.DialogueFrame.SetActive(false);
            }
            else
            {
                uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0f);
                uiController.DialogueFrame.SetActive(true);
                uiController.DialogueFrameBig.SetActive(false);
            }
        
            uiController.NewDialoguePersonUIUpdateBasedOnBigUI(ID);
        }

        newDialogue.GetComponent<Dialogue>().ReadDialogueData(ID);
        newDialogue.GetComponent<Dialogue>().InitializeUI();
    }


    public void ShowNextQuestion()
    {
        GameObject newQuestion = Instantiate(Questions[CurrentQuestionID], GameObject.Find(Questions[CurrentQuestionID].name + "Pos").transform.position,Quaternion.identity);
        newQuestion.transform.SetParent(uiController.transform);
        newQuestion.transform.localScale = Vector3.one;
        uiController.HidePersonUI();

    }

    public void ReArrangeBgResource()
    {
        GifImage.GetComponent<AnimatedGifPlayer>().enabled = false;

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
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        uiController.BlackScreen.DOColor(Color.black, 2f);

        yield return new WaitForSeconds(2f);
        uiController.IntroductionText.SetActive(false);
        VideoTextureUpdate("Command Deck");
        yield return new WaitForSeconds(1f);
        uiController.BlackScreen.DOColor(new Color(Color.black.a, Color.black.g, Color.black.b, 0), 2f);
        yield return new WaitForSeconds(2f);
        // ShowNoCharacterText();

        ShowNextDialogue(1);
    }

    public void ShowNoCharacterText()
    {
        uiController.NocharacterInfo.SetActive(true);
        uiController.NocharacterInfo.GetComponentInChildren<Button>().onClick.AddListener(() =>

        {
            ShowNextDialogue(1);
            uiController.NocharacterInfo.SetActive(false);
        } );
    }

    void OnLevelWasLoaded(int level)
    { 
        if (level == 1)
          VideoTextureUpdate("Courtyard");
    }
}
