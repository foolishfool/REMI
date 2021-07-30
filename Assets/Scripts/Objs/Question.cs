using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Question : MonoBehaviour
{
    // Start is called before the first frame update

    public int ID;

    public List<ChoiceOption> Answers = new List<ChoiceOption>();
    public List<TMP_InputField> CharacterInput;
    public GameObject SubmitButton;
    public int MaxChoiceNum;
    public Scrollbar scrollBar;
    public bool IsLogIn;
    public bool LoadNewScene;
    public bool FinishScreenShow;
    public bool BlackScreen;
    public int BlackScreenID;
    public bool IsSaveData;
    void Start()
    {
        if (scrollBar)
        scrollBar.onValueChanged.Invoke(0.999f);

        for (int i = 0; i < CharacterInput.Count; i++)
        {
            CharacterInput[i].onValidateInput  += delegate (string input, int charIndex, char addedChar) { return SetToUpper(addedChar); };


        }
    }


    public char SetToUpper(char c)
    {
        string str = c.ToString().ToUpper();
        char[] chars = str.ToCharArray();
        return chars[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (IsLogIn)
        {
            for (int i = 0; i < Answers.Count; i++)
            {
                if (Answers[i].Input.text == "")
                {
                    SubmitButton.SetActive(false);
                    return;
                }
            }

            SubmitButton.SetActive(true);
        }

        else
        {
            if (Answers.Count == MaxChoiceNum)
            {
                for (int i = 0; i < Answers.Count; i++)
                {
                 
                    if (Answers[i].HasInput && Answers[i].Input.text.Length == 0)
                    {
                        SubmitButton.SetActive(false);
                        return;
                    }

                }
                SubmitButton.SetActive(true);
            }

        }
    }

    //*** change isOn first then call the function
    public void  AnswerSelected(ChoiceOption choice)
    {
        //remove *** must isOn
        if (Answers.Contains(choice) && !choice.GetComponent<Toggle>().isOn)
        {
            Answers.Remove(choice);
            DataHandler.Instance.Score -= choice.Score;


            if (Answers.Count < MaxChoiceNum)
            {
                SubmitButton.SetActive(false);
            }
        }
        else
        {
            if (Answers.Count < MaxChoiceNum )
            {
                Answers.Add(choice);
                DataHandler.Instance.Score += choice.Score;
                if (Answers.Count == MaxChoiceNum)
                {
                    SubmitButton.SetActive(true);
                }
            }
            else if (Answers.Count >= MaxChoiceNum )
            {    
                //***  cannot use Answers[MaxChoiceNum - 1].GetComponent<Toggle>().isOn = false, as it wll call AnswerSelected as the value is changed 
                Answers.Add(choice);
                DataHandler.Instance.Score += choice.Score;
                if (Answers[MaxChoiceNum - 1].GetComponent<Toggle>())
                Answers[MaxChoiceNum - 1].GetComponent<Toggle>().isOn = false; //add first then remove
                //will trigger AnswerSelected();

            }
        }

    }

    public void SubmitQuestion()
    {
        if (Answers.Count < MaxChoiceNum)
        {
            NoticePanelController.Instance.ShowNotice("You need to choose " + MaxChoiceNum + "items !",Color.red);
            return;
        }
        if (!DataHandler.Instance.AllQuestionData.ContainsKey(ID))
        {
            DataHandler.Instance.AllQuestionData.Add(ID, this);
        }


        if (!LoadNewScene)
        {

            Destroy(GameController.Instance.CurrentDialogue.gameObject);
            if (!FinishScreenShow)
            {

                if (BlackScreen)
                {
                    GameObject.Find("UIController").GetComponent<UIController>().BlackScreenScreenShow(BlackScreenID);
                }

                else
                {
                    if (DataHandler.Instance.AllDialogueDatas[GameController.Instance.CurrentDialogueID].NextMultipleDialogue != " ")
                        //show next dialogue based on previous choosen 
                        GameController.Instance.CurrentDialogue.ChooseNextDialogueBasedOnPreChoosen();
                    else
                        GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
                }

               
            }

            else
            {
                GameObject.Find("UIController").GetComponent<UIController>().FinishGameScreenShow();
            }
          
      
        }
        else
        {
            GameObject.Find("UIController").GetComponent<UIController>().LoadNewScene(true);
        }


        if (IsSaveData)
        {
            DataHandler.Instance.SaveData();
        }
       
        GameController.Instance.CurrentQuestionID++;
        Destroy(gameObject);
    }




    public void SubmitLogInDetail()
    {
        string currentuserID = "";
        for (int i = 0; i < Answers.Count; i++)
        {
            currentuserID += Answers[i].Input.text;
        }

        GameObject.Find("UIController").GetComponent<UIController>().UserCode.text = currentuserID;
        GameObject.Find("UIController").GetComponent<UIController>().UserCode.gameObject.SetActive(true);

        currentuserID += DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        currentuserID = currentuserID.Replace("/", "");
        currentuserID = currentuserID.Replace(":", "");
        currentuserID = currentuserID.Replace(" ", "");
        DataHandler.Instance.CurrentUserID = currentuserID;
        Destroy(GameController.Instance.CurrentDialogue.gameObject);
        GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
        GameController.Instance.CurrentQuestionID++;
        Destroy(gameObject);
    }




}
