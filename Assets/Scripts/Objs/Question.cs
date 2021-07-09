using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    // Start is called before the first frame update

    public int ID;

    public List<ChoiceOption> Answers = new List<ChoiceOption>();
    public GameObject SubmitButton;
    public int MaxChoiceNum;
    public Scrollbar scrollBar;
    void Start()
    {
        scrollBar.onValueChanged.Invoke(0.999f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        DataHandler.Instance.AllQuestionData.Add(ID,this);
        GameController.Instance.ShowNextDialogue(GameController.Instance.CurrentDialogueID++);
        Destroy(this.gameObject);
    }

    
}
