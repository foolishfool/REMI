using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{

    public int ID;
    [HideInInspector] public int PersonID;
    [HideInInspector] public string DialogueText;
    [HideInInspector] public string Expression;
    [HideInInspector] public int NextDialogueID;
    [HideInInspector] public int NextOperation;
    [HideInInspector] public int NextEffectID;

    [HideInInspector] public string Option1;
    [HideInInspector] public int Option1NextDialogue;
    [HideInInspector] public string Option2;
    [HideInInspector] public int Option2NextDialogue;
    [HideInInspector] public string Option3;
    [HideInInspector] public int Option3NextDialogue;
    [HideInInspector]
    public int IsBigUI;

    public GameObject NextButton;

    public TextMeshProUGUI DialogueDetails;


    public Option OptionObj1;
    public Option OptionObj2;
    public Option OptionObj3;

    public TextMeshProUGUI Option1Text;
    public TextMeshProUGUI Option2Text;
    public TextMeshProUGUI Option3Text;

    private UIController uicontroller;

    // Start is called before the first frame update
    void Start()
    {
        uicontroller = GameObject.Find("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ReadDialogueData(int id)
    {
        ID = DataHandler.Instance.AllDialogueDatas[id].ID;
        PersonID = DataHandler.Instance.AllDialogueDatas[id].PersonID;
        DialogueText = DataHandler.Instance.AllDialogueDatas[id].DialogueText;
        Expression = DataHandler.Instance.AllDialogueDatas[id].Expression;
        IsBigUI = DataHandler.Instance.AllDialogueDatas[id].IsBigUI;
        NextDialogueID = DataHandler.Instance.AllDialogueDatas[id].NextDialogueID;
        NextOperation = DataHandler.Instance.AllDialogueDatas[id].NextOperation;
        NextEffectID = DataHandler.Instance.AllDialogueDatas[id].NextEffectID;
        Option1 = DataHandler.Instance.AllDialogueDatas[id].Option1;
        Option1NextDialogue = DataHandler.Instance.AllDialogueDatas[id].Option1NextDialogue;
        Option2 = DataHandler.Instance.AllDialogueDatas[id].Option2;
        Option2NextDialogue = DataHandler.Instance.AllDialogueDatas[id].Option2NextDialogue;
        Option3 = DataHandler.Instance.AllDialogueDatas[id].Option3;
        Option3NextDialogue = DataHandler.Instance.AllDialogueDatas[id].Option3NextDialogue;
    }


    public void NextButtonClick()
    {
        if (NextDialogueID == 0)
        {
            switch (NextOperation)
            {
                case 1:

                    if (GameController.Instance.ShowOptions == false)
                    {
                        GameController.Instance.ShowOptions = true;
                        DialogueDetails.gameObject.SetActive(false);
                        ShowOptions();
                    }


                    if (GameController.Instance.OptionClicked == true)
                    {
                        GameController.Instance.OptionClicked = false;
                        GameController.Instance.ShowNextDialogue(GameController.Instance.NewDialogueID);
                        uicontroller.HideCrystals();
                        Destroy(gameObject);
                    }

                    break;
                case 2:

                        GameController.Instance.ShowNextQuestion();
                        Destroy(gameObject);
   
                    break;
                case 3:
                
                    if (GameController.Instance.ShowEffect == false)
                    {
                        ShowEffect();

                    }

                    else
                    {
                        GameController.Instance.ShowEffect = false;
                        //NextEffectID start from 1
                        StopEffectWhiteScreenBehavior(NextEffectID-1);
                        uicontroller.HidePersonUI();
                        if (NextEffectID!=3)
                        {
                            GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
                        }
                     
                        Destroy(gameObject);

                    }
                    break;

                default:
                    break;
            }
        }

        else
        {
          GameController.Instance.ShowNextDialogue(NextDialogueID);
            Destroy(gameObject);
        }
    }



    public void InitializeUI()
    {

        DialogueDetails.text = DialogueText;
      // if (DialogueDetails.text == " ")
      // {
      //     GameObject.Find("UIController").GetComponent<UIController>().DialogueFrame.SetActive(false);
      // }

        Option1Text.text = Option1;
        Option2Text.text = Option2;
        Option3Text.text = Option3;
        OptionObj1.NextDialogueID = Option1NextDialogue;
        OptionObj2.NextDialogueID = Option2NextDialogue;
        OptionObj3.NextDialogueID = Option3NextDialogue;


        StartCoroutine(UIShowBehavior(GameController.Instance.ShowDialogueFrameAnimation));
    }

    public IEnumerator UIShowBehavior(bool showAnmation)
    {
        if (showAnmation)
        {
            yield return new WaitForSeconds(0.5f);
        }
        DialogueDetails.gameObject.SetActive(true);
        NextButton.gameObject.SetActive(true);

        yield break;
    }

    public void ShowEffect()
    {
        NextButton.SetActive(false);
        //as
        WhiteScreenShow(NextEffectID-1);

        DialogueDetails.gameObject.SetActive(false);
        GameController.Instance.ShowEffect = true;
    }


    public void WhiteScreenShow(int whitescreenid)
    {
        StartCoroutine(WhiteScreenBehavior(whitescreenid));
    }

    public void StopEffectWhiteScreenBehavior(int whitescreenID)
    {
        uicontroller.WhiteScreens[whitescreenID].DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f).OnComplete(()=> {
            //effectID = 3 but whitescreenID =2
            if (whitescreenID == 2)
            {
                uicontroller.LoadNewScene();
            }
        });
        uicontroller.WhiteScreens[whitescreenID].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f);

    }

    public IEnumerator WhiteScreenBehavior(int whitescreenid)
    {
        uicontroller.WhiteScreens[whitescreenid].DOColor(Color.white,2f);
        yield return new WaitForSeconds(1f);
        uicontroller.WhiteScreens[whitescreenid].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(Color.black,1f);
        NextButton.SetActive(true);
        yield break;
    }



    public void ShowOptions()
    {

        NextButton.SetActive(false);

        if ( GameController.Instance. CurrentDialogueID == 13)
        {
            uicontroller.ShowCrystals();
        }


        if (Option1Text.text != "")
        {
            OptionObj1.gameObject.SetActive(true);
        }

        if (Option2Text.text != "")
        {
            OptionObj2.gameObject.SetActive(true);
        }
        if (Option3Text.text != "")
        {
            OptionObj3.gameObject.SetActive(true);
        }
    }

}
