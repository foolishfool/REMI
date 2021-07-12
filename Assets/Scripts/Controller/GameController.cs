using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int CurrentDialogueID;
    [HideInInspector]
    public Dialogue CurrentDialogue;
    public GameObject DialoguePrefab;
    public GameObject DialoguePrefabBig;
    public bool LeftSizeShow = true;
    public bool ShowDialogueFrameAnimation;
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
        if (!DataHandler.Instance.AllDialogueDatas.ContainsKey(ID))
        {
            return;
        }

        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        //reset
        ShowDialogueFrameAnimation = false;
        ShowOptions = false;
        OptionClicked = false;
        ShowEffect = false;
        CurrentDialogueID = ID;
        IsBigUI = false;

        GameObject newDialogue;

        if (DataHandler.Instance.AllDialogueDatas[ID].IsBigUI == 1)
        {
             IsBigUI = true;
             newDialogue = Instantiate(DialoguePrefabBig, uiController.DialoguePos.position, Quaternion.identity);
        }
        else 
         newDialogue = Instantiate(DialoguePrefab, uiController.DialoguePos.position, Quaternion.identity);
        newDialogue.transform.parent = uiController.transform;
        newDialogue.transform.localScale = Vector3.one;
        CurrentDialogue = newDialogue.GetComponent<Dialogue>();
        if (DataHandler.Instance.CurrentPersonID != DataHandler.Instance.AllDialogueDatas[ID].PersonID && DataHandler.Instance.CurrentPersonID != 0)
        {
            uiController.HidePersonUI();
            if (IsBigUI)
            {
                uiController.DialogueFrame.SetActive(false);
                uiController.DialogueFrameBig.SetActive(true);
                uiController.DialogueFrameBig.transform.DOMoveY(uiController.DialogueFrameInitialPos.position.y, 0f);
            }
            else {
                uiController.DialogueFrame.SetActive(false);
                uiController.DialogueFrame.SetActive(true);
                uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFrameInitialPos.position.y, 0f);
            }
          
        
            LeftSizeShow = !LeftSizeShow;
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;
            ShowDialogueFrameAnimation = true;
            if (IsBigUI)
            {
                uiController.DialogueFrameBig.transform.DOMoveY(uiController.DialogueFramePosBig.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdate(ID));
            }
            else
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdate(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == 0)
        {
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;
            ShowDialogueFrameAnimation = true;
            if (IsBigUI)
            {
                uiController.DialogueFrameBig.transform.DOMoveY(uiController.DialogueFramePosBig.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdate(ID));
            }
            else
            uiController.DialogueFrame.transform.DOMoveY(uiController.DialogueFramePos.position.y, 0.5f).OnComplete(() => uiController.NewDialoguePersonUIUpdate(ID));
        }
        else if (DataHandler.Instance.CurrentPersonID == DataHandler.Instance.AllDialogueDatas[ID].PersonID)
        {
            //only update expression
            if (IsBigUI)
            {
                uiController.DialogueFrameBig.SetActive(true);
            }
            else
            uiController.DialogueFrame.SetActive(true);
            uiController.NewDialoguePersonUIUpdate(ID);
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
