using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public int ID;

    [HideInInspector] public int PersonID;
    [HideInInspector] public string DialogueText;
    [HideInInspector] public string Expression;
    [HideInInspector] public int NextDialogueID;
    [HideInInspector] public int NextOperation;
    [HideInInspector] public int NextEffectID;


    [HideInInspector] public string PreviousChosen;
    [HideInInspector] public string NextMultipleDialogue;

    [HideInInspector] public string Option1;
    [HideInInspector] public int Option1NextDialogue;
    [HideInInspector] public string Option2;
    [HideInInspector] public int Option2NextDialogue;
    [HideInInspector] public string Option3;
    [HideInInspector] public int Option3NextDialogue;


    [HideInInspector] public int IsBigUI;
    [HideInInspector] public int IsBigOption;

    public GameObject NextButton;

    public TextMeshProUGUI DialogueDetails;
    public TextMeshProUGUI DialogueDetailsBig;

    public Option OptionObj1;
    public Option OptionObj2;
    public Option OptionObj3;

    public TextMeshProUGUI Option1Text;
    public TextMeshProUGUI Option2Text;
    public TextMeshProUGUI Option3Text;



    public Option OptionObj1Big;
    public Option OptionObj2Big;
    public Option OptionObj3Big;

    public TextMeshProUGUI Option1TextBig;
    public TextMeshProUGUI Option2TextBig;
    public TextMeshProUGUI Option3TextBig;


    private UIController uicontroller;
    private List<string> priviousChosenStr = new List<string>();
    private List<int> nextMultipleDialogueID = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        uicontroller = GameObject.Find("UIController").GetComponent<UIController>();
        GameController.Instance.UiController.SkipButton.transform.localScale = Vector3.one;
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
        IsBigOption = DataHandler.Instance.AllDialogueDatas[id].IsBigOption;
        NextDialogueID = DataHandler.Instance.AllDialogueDatas[id].NextDialogueID;
        NextOperation = DataHandler.Instance.AllDialogueDatas[id].NextOperation;
        NextEffectID = DataHandler.Instance.AllDialogueDatas[id].NextEffectID;

        PreviousChosen  = DataHandler.Instance.AllDialogueDatas[id].PreviousChosen;
        NextMultipleDialogue = DataHandler.Instance.AllDialogueDatas[id].NextMultipleDialogue;

        Option1 = DataHandler.Instance.AllDialogueDatas[id].Option1;
        Option1NextDialogue = DataHandler.Instance.AllDialogueDatas[id].Option1NextDialogue;
        Option2 = DataHandler.Instance.AllDialogueDatas[id].Option2;
        Option2NextDialogue = DataHandler.Instance.AllDialogueDatas[id].Option2NextDialogue;
        Option3 = DataHandler.Instance.AllDialogueDatas[id].Option3;
        Option3NextDialogue = DataHandler.Instance.AllDialogueDatas[id].Option3NextDialogue;

        GetCurrentPreviousChosen();
        GetNextMultipleDialogueID();
    }



    public void GetCurrentPreviousChosen()
    {
        if (PreviousChosen == "")
        {
            return;
        }
        string[] preiviouschooseIDs = PreviousChosen.Split('|');

        for (int i = 0; i < preiviouschooseIDs.Length; i++)
        {
            if (preiviouschooseIDs[i] != "")
            {
                if (!priviousChosenStr.Contains(preiviouschooseIDs[i]))
                priviousChosenStr.Add(preiviouschooseIDs[i]);
            }


        }
    }

    public void GetNextMultipleDialogueID()
    {
        if (NextMultipleDialogue == " ")
        {
            return;
        }
        string[] nextMultipleID = NextMultipleDialogue.Split('|');

        for (int i = 0; i < nextMultipleID.Length; i++)
        {
            int number;
            int.TryParse(nextMultipleID[i], out number);
            if (number != 0)
            {
                //could be the same number
               // if (!nextMultipleDialogueID.Contains(number))
                    nextMultipleDialogueID.Add(number);
            }

        }
    }

    public void NextButtonClick()
    {


   
         switch (NextOperation)
                {
                    case 1:

                        if (GameController.Instance.ShowOptions == false)
                        {
                            GameController.Instance.ShowOptions = true;
                            DialogueDetails.gameObject.SetActive(false);
                             DialogueDetailsBig.gameObject.SetActive(false);
                            NextButton.SetActive(false);
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
                //do not destroy as question will use its data
                        uicontroller.DialogueFrame.transform.DOMoveY(uicontroller.DialogueFrameInitialPos.position.y, 0f);
                        uicontroller.DialogueFrameBig.SetActive(false);
                        uicontroller.DialogueFrame.SetActive(false);
                        gameObject.transform.localScale = Vector3.zero;
                       

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
                            StopEffectWhiteScreenBehavior(NextEffectID - 1);
                            uicontroller.HidePersonUI();
                            GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
                            Destroy(gameObject);

                        }
                        break;

                    case 4:
                        GameController.Instance.CurrentSceneID++;
                        uicontroller.LoadNewScene(true);
                        uicontroller.DialogueFrame.transform.DOMoveY(uicontroller.DialogueFrameInitialPos.position.y, 0f);
                        uicontroller.HidePersonUI();
                        gameObject.transform.localScale = Vector3.zero;
                      break;
                  case 5:

                     uicontroller.BlackScreenScreenShow(0);
                     uicontroller.DialogueFrame.transform.DOMoveY(uicontroller.DialogueFrameInitialPos.position.y, 0f);
                     uicontroller.HidePersonUI();
                     Destroy(gameObject);
                     break;
                 case 7:

                if (uicontroller.CurrentHallWayButton)
                {
                    uicontroller.CurrentHallWayButton.GetComponent<Image>().sprite = uicontroller.HallwaybuttonDoneSprite;
                    uicontroller.CurrentHallWayButton.enabled = false;
                    if (uicontroller.CurrentHallWayButton.gameObject.name == "Zero G")
                    {
                        uicontroller.WavyTripBegin();
                    }
                    else
                    {
                        StartCoroutine(uicontroller.ShowScrollableHallway());
                    }
                }
                else StartCoroutine(uicontroller.ShowScrollableHallway());


                    uicontroller.DialogueFrame.transform.DOMoveY(uicontroller.DialogueFrameInitialPos.position.y, 0f);
                    uicontroller.HidePersonUI();
                    gameObject.transform.localScale = Vector3.zero;
                    Invoke("DestroySelf", 4f);
                    
                break;
            // case 6:
            //
            //     //finish game
            //     uicontroller.HidePersonUI();
            //     uicontroller.FinishGameUI.SetActive(true);
            //     uicontroller.Score.text = "Your Score: " + DataHandler.Instance.Score.ToString();
            //     DataHandler.Instance.SaveData();
            //     //save data
            //     Destroy(gameObject);
            //     break;


            default:
                    if (NextMultipleDialogue != " ")
                    {
                        ChooseNextDialogueBasedOnPreChoosen();
                    }

                      else if ( NextDialogueID !=0)
                      {
                          GameController.Instance.ShowNextDialogue(NextDialogueID);
                          Destroy(gameObject);
                      }
                          break;
                      }




    }



    public void ChooseNextDialogueBasedOnPreChoosen()
    {

        for (int i = 0; i < priviousChosenStr.Count; i++)
        {
            if (DataHandler.Instance.AllQuestionData.Values.Last().Answers[0].OptionNum == priviousChosenStr[i])
            {
                Debug.Log(nextMultipleDialogueID[i]);
                GameController.Instance.ShowNextDialogue(nextMultipleDialogueID[i]);
                Destroy(gameObject);
                return;
            }
        }


        //if there is no privousChosenStr means need to go from score
        //Strong score
        if (DataHandler.Instance.Score>=14)
        {
            GameController.Instance.ShowNextDialogue(nextMultipleDialogueID[0]);
        }  //Medium score
        else if (DataHandler.Instance.Score >= 9 && DataHandler.Instance.Score < 14)
        {
            GameController.Instance.ShowNextDialogue(nextMultipleDialogueID[1]);
        }//low score
        else if (DataHandler.Instance.Score <=8)
        {
            GameController.Instance.ShowNextDialogue(nextMultipleDialogueID[2]);
        }

        Destroy(gameObject);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void InitializeUI()
    {

        DialogueDetails.text = DialogueText;
        DialogueDetailsBig.text = DialogueText;

        Option1Text.text = Option1;
        Option2Text.text = Option2;
        Option3Text.text = Option3;

        Option1TextBig.text = Option1;
        Option2TextBig.text = Option2;
        Option3TextBig.text = Option3;

        OptionObj1.NextDialogueID = Option1NextDialogue;
        OptionObj2.NextDialogueID = Option2NextDialogue;
        OptionObj3.NextDialogueID = Option3NextDialogue;

        OptionObj1Big.NextDialogueID = Option1NextDialogue;
        OptionObj2Big.NextDialogueID = Option2NextDialogue;
        OptionObj3Big.NextDialogueID = Option3NextDialogue;


        StartCoroutine(UIShowBehavior());
    }

    public IEnumerator UIShowBehavior()
    {

        yield return new WaitUntil(() => GameController.Instance.ShowDialogueText);
        GameController.Instance.IsSkipClicked = false;
        if (GameController.Instance.IsBigUI)
        {
            DialogueDetails.gameObject.SetActive(false);
            DialogueDetailsBig.gameObject.SetActive(true);
        }
        else
        {
            DialogueDetailsBig.gameObject.SetActive(false);
            DialogueDetails.gameObject.SetActive(true);
        }

        if (NextOperation == 1)
        {
            NextButton.gameObject.SetActive(false);
            ShowOptions();
        }
        else 
        NextButton.gameObject.SetActive(true);

        yield break;
    }

    public void ShowEffect()
    {
        NextButton.SetActive(false);
        //as
        WhiteScreenShow(NextEffectID-1);

        DialogueDetails.gameObject.SetActive(false);
        DialogueDetailsBig.gameObject.SetActive(false);
        GameController.Instance.ShowEffect = true;
    }


    public void WhiteScreenShow(int whitescreenid)
    {
        StartCoroutine(WhiteScreenBehavior(whitescreenid));
    }

    public void StopEffectWhiteScreenBehavior(int whitescreenID)
    {
        if (uicontroller.WhiteScreens[whitescreenID].GetComponent<Image>())
        {
            uicontroller.WhiteScreens[whitescreenID].GetComponent<Image>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f);
        }
        if (uicontroller.WhiteScreens[whitescreenID].GetComponent<RawImage>())
        {
            uicontroller.WhiteScreens[whitescreenID].GetComponent<RawImage>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f);
        }
        uicontroller.WhiteScreens[whitescreenID].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 1f);
        GameController.Instance.StartVideoPlayer.Play();
    }

    public IEnumerator WhiteScreenBehavior(int whitescreenid)
    {

        if (whitescreenid == 1 || whitescreenid == 4)
        {
            uicontroller.HidePersonUI();
            if (uicontroller.WhiteScreens[whitescreenid].GetComponent<RawImage>())
                uicontroller.WhiteScreens[whitescreenid].GetComponent<RawImage>().DOColor(Color.white, 2f);

            gameObject.transform.localScale = Vector3.zero;
            if (whitescreenid == 1)
            {
                GameController.Instance.EffectVideoTextureUpdate("Biobot Creation");
            }
            if (whitescreenid == 4)
            {
                if (DataHandler.Instance.Score >= 14)
                {
                    GameController.Instance.EffectVideoTextureUpdate("High Tier");
                }  //Medium score
                else if (DataHandler.Instance.Score >= 9 && DataHandler.Instance.Score < 14)
                {
                    GameController.Instance.EffectVideoTextureUpdate("Moderate Tier");
                }//low score
                else if (DataHandler.Instance.Score <= 8)
                {
                    GameController.Instance.EffectVideoTextureUpdate("Low Tier");
                }

            }

            yield return new WaitForSeconds(13f);

            if (whitescreenid == 4)
            {
                if (uicontroller.WhiteScreens[whitescreenid].GetComponent<RawImage>())
                {
                    uicontroller.WhiteScreens[whitescreenid].GetComponent<RawImage>().DOColor(new Color(Color.white.a, Color.white.g, Color.white.b, 0), 0f);
                }

                //lava scene transfer directly
                uicontroller.LoadNewScene(false);
            }
            else
            {
                StopEffectWhiteScreenBehavior(whitescreenid);
                yield return new WaitForSeconds(2f);
                GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
                Destroy(gameObject);
            }

            //NextButton.SetActive(true);
            yield break;
        }

        else
        {
            if (uicontroller.WhiteScreens[whitescreenid].GetComponent<Image>())
                uicontroller.WhiteScreens[whitescreenid].GetComponent<Image>().DOColor(Color.white, 2f);
            if (uicontroller.WhiteScreens[whitescreenid].GetComponent<RawImage>())
                uicontroller.WhiteScreens[whitescreenid].GetComponent<RawImage>().DOColor(Color.white, 2f);
            yield return new WaitForSeconds(1f);
            uicontroller.HidePersonUI();
            uicontroller.WhiteScreens[whitescreenid].transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(Color.black, 1f);
            yield return new WaitForSeconds(1f);
            NextButton.SetActive(true);
        }
     

        yield break;
    }



    public void ShowOptions()
    {

        NextButton.SetActive(false);

        if ( GameController.Instance. CurrentDialogueID == 16)
        {
            uicontroller.ShowCrystals();
        }
        uicontroller.NewDialoguePersonUIUpdateBasedOnOption(ID);

    

        if (IsBigOption == 0)
        {
            uicontroller.DialogueFrameBig.SetActive(false);
            uicontroller.DialogueFrame.SetActive(true);
            uicontroller.DialogueFrame.transform.DOMoveY(uicontroller.DialogueFramePos.position.y, 0f);

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

        else
        {
            uicontroller.DialogueFrame.SetActive(false);
            uicontroller.DialogueFrameBig.SetActive(true);
            uicontroller.DialogueFrameBig.transform.DOMoveY(uicontroller.DialogueFramePosBig.position.y, 0f);

            if (Option1Text.text != "")
            {
                OptionObj1Big.gameObject.SetActive(true);
            }

            if (Option2Text.text != "")
            {
                OptionObj2Big.gameObject.SetActive(true);
            }
            if (Option3Text.text != "")
            {
                OptionObj3Big.gameObject.SetActive(true);
            }
        }

    }

}
