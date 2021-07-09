using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int CurrentDialogueID;
    public GameObject DialoguePrefab;
    public bool LeftSizeShow = true;
    public bool ShowDialogueFrameAnimation;
    public bool ShowOptions;
    public bool ShowEffect;
    public bool OptionClicked;
    [HideInInspector]
    public int CurrentQuestionID;
    public string CurrentEffectName;
    [HideInInspector]
    public GameObject CurrentEffect;
    public int NewDialogueID;
    static GameController instance;
    private UIController uiController;
    public List<GameObject> Questions;
    public List<GameObject> Effects;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("UIController"))
        {
            uiController = GameObject.Find("UIController").GetComponent<UIController>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNextDialogue(int ID)
    {
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        //reset
        ShowDialogueFrameAnimation = false;
        ShowOptions = false;
        OptionClicked = false;
        ShowEffect = false;
        CurrentDialogueID = ID;

        GameObject newDialogue = Instantiate(DialoguePrefab, uiController.DialoguePos.position, Quaternion.identity);
        newDialogue.transform.parent = uiController.transform;
        if (DataHandler.Instance.CurrentPersonID != DataHandler.Instance.AllDialogueDatas[ID].PersonID && DataHandler.Instance.CurrentPersonID != 0)
        {
            uiController.HidePersonUI();
            uiController.DialogueFrame.SetActive(true);
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFrameInitialPos.position.y,0f);
            LeftSizeShow = !LeftSizeShow;
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;
            ShowDialogueFrameAnimation = true;
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdate(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == 0)
        {
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;
            ShowDialogueFrameAnimation = true;
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdate(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == DataHandler.Instance.AllDialogueDatas[ID].PersonID)
        {
            //only update expression
            uiController.NewDialoguePersonUIUpdate(ID);
        }

        newDialogue.GetComponent<Dialogue>().ReadDialogueData(ID);
        newDialogue.GetComponent<Dialogue>().InitializeUI();
    }


    public void ShowNextQuestion()
    {
        GameObject newQuestion = Instantiate(Questions[CurrentQuestionID], GameObject.Find(Questions[CurrentQuestionID].name + "Pos").transform.position,Quaternion.identity);
        newQuestion.transform.SetParent(uiController.transform);
        uiController.HidePersonUI();

    }

    public void QuestionFinished()
    {
        //save QuestionResult
        Questions[CurrentQuestionID].SetActive(false);
        CurrentQuestionID++;
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
            ShowNextDialogue(1);

    }

}
