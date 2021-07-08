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
    [HideInInspector]
    public int CurrentEffectID;

    [HideInInspector]
    public GameObject CurrentEffect;
    public int NewDialogueID;
    static GameController instance;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNextDialogue(int ID)
    {
        //reset
        ShowDialogueFrameAnimation = false;
        ShowOptions = false;
        OptionClicked = false;
        ShowEffect = false;
        CurrentDialogueID = ID;

        GameObject newDialogue = Instantiate(DialoguePrefab, GameObject.Find("UIController").GetComponent<UIController>().DialoguePos.position, Quaternion.identity);
        newDialogue.transform.parent = GameObject.Find("UIController").transform;
        if (DataHandler.Instance.CurrentPersonID != DataHandler.Instance.AllDialogueDatas[ID].PersonID && DataHandler.Instance.CurrentPersonID != 0)
        {
            GameObject.Find("UIController").GetComponent<UIController>().DialogueFrame.SetActive(true);
            GameObject.Find("UIController").GetComponent<UIController>().DialogueFrame.transform.DOMoveY(GameObject.Find("UIController").GetComponent<UIController>().DialogueFrameInitialPos.position.y,0f);
            LeftSizeShow = !LeftSizeShow;
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;
            ShowDialogueFrameAnimation = true;
            GameObject.Find("UIController").GetComponent<UIController>().DialogueFrame.transform.DOMoveY(GameObject.Find("UIController").GetComponent<UIController>().DialogueFramePos.position.y, 1f);
        }
        else if (DataHandler.Instance.CurrentPersonID == 0)
        {
            DataHandler.Instance.CurrentPersonID = DataHandler.Instance.AllDialogueDatas[ID].PersonID;
            ShowDialogueFrameAnimation = true;
            GameObject.Find("UIController").GetComponent<UIController>().DialogueFrame.transform.DOMoveY(GameObject.Find("UIController").GetComponent<UIController>().DialogueFramePos.position.y, 1f);
        }
        GameObject.Find("UIController").GetComponent<UIController>().NewDialoguePersonUIUpdate(ID);
        newDialogue.GetComponent<Dialogue>().ReadDialogueData(ID);
        newDialogue.GetComponent<Dialogue>().InitializeUI();
    }


    public void ShowNextQuestion()
    {
        Questions[CurrentQuestionID].SetActive(true);

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
