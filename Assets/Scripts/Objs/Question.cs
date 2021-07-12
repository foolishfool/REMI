using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Question : MonoBehaviour
{
    // Start is called before the first frame update

    public int ID;

    public List<ChoiceOption> Answers = new List<ChoiceOption>();
    public GameObject SubmitButton;
    public int MaxChoiceNum;
    public Scrollbar scrollBar;
    public bool IsLogIn;


    void Start()
    {
        if (scrollBar)
        scrollBar.onValueChanged.Invoke(0.999f);
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
    }

    //*** change isOn first then call the function
    public void  AnswerSelected(ChoiceOption choice)
    {
        //remove *** must isOn
        if (Answers.Contains(choice) && !choice.GetComponent<Toggle>().isOn)
        {
            Answers.Remove(choice);
            
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
                if (Answers.Count == MaxChoiceNum)
                {
                    SubmitButton.SetActive(true);
                }
            }
            else if (Answers.Count >= MaxChoiceNum )
            {    
                //***  cannot use Answers[MaxChoiceNum - 1].GetComponent<Toggle>().isOn = false, as it wll call AnswerSelected as the value is changed 
                Answers.Add(choice);
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
            Debug.Log(Answers.Count);
            NoticePanelController.Instance.ShowNotice("You need to choose " + MaxChoiceNum + "items !",Color.red);
            return;
        }
        DataHandler.Instance.AllQuestionData.Add(ID,this);
        GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID+1);
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

        currentuserID += DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        currentuserID = currentuserID.Replace("/", "");
        currentuserID = currentuserID.Replace(":", "");
        currentuserID = currentuserID.Replace(" ", "");
        DataHandler.Instance.CurrentUserID = currentuserID;
        GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID + 1);
        GameController.Instance.CurrentQuestionID++;
        Destroy(gameObject);
    }


    public void ShowNextScene()
    {
        DataHandler.Instance.AllQuestionData.Add(ID, this);
        GameController.Instance.CurrentQuestionID++;
        GameObject.Find("UIController").GetComponent<UIController>().LoadNewScene();
    }
}
