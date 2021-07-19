
using LitJson;
using FirebaseWebGL.Examples.Database;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DataHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]
    public Dictionary<int, DialogueData> AllDialogueDatas = new Dictionary<int, DialogueData>();
    [HideInInspector]
    public Dictionary<int, Question> AllQuestionData = new Dictionary<int, Question>();
 
    public string CurrentUserID;
    [HideInInspector]
    public int CurrentPersonID;

    [HideInInspector]
    public int Score;

    [HideInInspector]
    public DatabaseExampleHandler DatabaseHandler;

    static DataHandler instance;

    private bool isReadingData;



    public int NUM1A;
    public int NUM1B;
    public int NUM1C;
    public int NUM2A;
    public int NUM2B;
    public int NUM2C;
    public int NUM3A;
    public int NUM3B;
    public int NUM3C;
    public int NUM4A;
    public int NUM4B;
    public int NUM4C;
    public int NUM5A;
    public int NUM5B;
    public int NUM5C;
    public int NUM6A;
    public int NUM6B;
    public int NUM6C;

    string multipleChoiceAnswer = "";
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(DataHandler)) as DataHandler;
                if (instance == null)
                {
                    GameObject obj = new GameObject("DataHandler");
                    instance = obj.AddComponent<DataHandler>();
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

    void Start()
    {
        ReadAllDialogueData();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReadAllDialogueData()
    {
        TextAsset asset = Resources.Load("Json/DialogueData") as TextAsset;
        if (!asset)
        {
            Debug.LogError("DialogueData JSON doesn't exist");
            return;
        }
        string strDialogueData = asset.text;

        JsonMapper.RegisterExporter<float>((obj, writer) => writer.Write(Convert.ToDouble(obj)));
        JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));

        DialogueData[] dialogueDataArray = JsonMapper.ToObject<DialogueData[]>(strDialogueData);


        //update coral data
        foreach (var item in dialogueDataArray)
        {
            //  item.LoadData(coralDataArray, item.CoralID);
            if (!AllDialogueDatas.ContainsKey(item.ID))
            {
                AllDialogueDatas.Add(item.ID, item);

            }

        }


    }


    public void SaveRecord()
    {
        foreach (var item in AllQuestionData)
        {
          //  if (item.Key!=0)
          //  FirebaseDatabase.DefaultInstance.RootReference.Child("Users").Child(CurrentUserID).Child(item.Key.ToString()).SetValueAsync(item.Value.Answers[0].OptionNum);
        }

    }

    public void ResetData()
    {
        DatabaseHandler.PostJSON("Statistics",null);
        DatabaseHandler.PostJSON("UserData", null);
    }

    public void ReadCurrentStatistics()
    {
        StartCoroutine(ReadRecordBehavior());
    }


    public IEnumerator ReadRecordBehavior()
    {
        DatabaseHandler = GameObject.Find("DataBaseExample").GetComponent<DatabaseExampleHandler>();
        StartCoroutine(ReadDataBehavior(NUM1A, "1A"));
        yield return new WaitUntil(() =>!isReadingData);
        StartCoroutine(ReadDataBehavior(NUM1B, "1B"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM1C, "1C"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM2A, "2A"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM2B, "2B"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM2C, "2C"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM3A, "3A"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM3B, "3B"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM3C, "3C"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM4A, "4A"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM4B, "4B"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM4C, "4C"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM5A, "5A"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM5B, "5B"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM5C, "5C"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM6A, "6A"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM6B, "6B"));
        yield return new WaitUntil(() => !isReadingData);
        StartCoroutine(ReadDataBehavior(NUM6C, "6C"));

  
    }


    public  IEnumerator ReadDataBehavior(int target ,string key)
    {
        isReadingData = true;
        DatabaseHandler.FinishReading = false;
        DatabaseHandler.CheckJSON("Statistics/"+ key);
        yield return new WaitUntil(() => DatabaseHandler.FinishReading);
        if (DatabaseHandler.HasChild == "true")
        {
            DatabaseHandler.FinishReading = false;
            DatabaseHandler.GetJSON("Statistics/" + key);
            yield return new WaitUntil(() => DatabaseHandler.FinishReading);
            target = int.Parse(DatabaseHandler.OutResult);

        }
        else
        {
            target = 0;
        }
      
        isReadingData = false;
  
    }

    public void SaveData()
    {
        StartCoroutine(SaveDataBehavior());
    }

    public IEnumerator SaveDataBehavior()
    {

        DatabaseHandler = GameObject.Find("DataBaseExample").GetComponent<DatabaseExampleHandler>();

        foreach (var item in AllQuestionData)
        {
            yield return new WaitUntil(() => !isReadingData);
            isReadingData = true;
            if (item.Key!=0 && item.Key != 1)
            {
                Debug.Log("UserData / " + CurrentUserID + " / " + item.Key.ToString());
                DatabaseHandler.PostJSON("UserData/"+CurrentUserID + "/" + item.Key.ToString(), item.Value.Answers[0].OptionNum);
                StartCoroutine(UpdateStatisticValue(item.Key, item.Value.Answers[0].OptionNum));
            }
            else if(item.Key == 1)
            {
                
                for (int i = 0; i < item.Value.Answers.Count; i++)
                {
                    multipleChoiceAnswer += item.Value.Answers[i].OptionNum;
                }

                for (int i = 0; i < item.Value.Answers.Count; i++)
                {
                    if (item.Value.Answers[i].HasInput && item.Value.Answers[i].Input.text != "")
                    {
                        multipleChoiceAnswer += item.Value.Answers[i].Input.text;
                    }
                }

                DatabaseHandler.PostJSON("UserData/" + CurrentUserID + "/" + item.Key.ToString(), multipleChoiceAnswer);
                isReadingData = false;
            }
           
        }

        yield return new WaitUntil(() => !isReadingData);
        DatabaseHandler.PostJSON("UserData/" + CurrentUserID + "/Score", Score.ToString());
    }


    public IEnumerator UpdateStatisticValue(int Key, string value)
    {
        switch (value)
        {
            case "A":
                int currentNum;
                DatabaseHandler.CheckJSON("Statistics/" + Key.ToString() + "A");
                if (DatabaseHandler.HasChild == "true")
                {
                    Debug.Log(111111111111);
                    DatabaseHandler.GetJSON("Statistics/" + Key.ToString() + "A");
                    yield return new WaitForSeconds(0.5f);
                    currentNum = int.Parse(DatabaseHandler.OutResult);
                }
                else
                {                 
                    currentNum = 0;
                }
                currentNum++;
                DatabaseHandler.PostJSON("Statistics/" + Key.ToString() + "A", currentNum.ToString());
                isReadingData = false;
                break;
            case "B":
                int currentNum2;
                DatabaseHandler.CheckJSON("Statistics/"+ Key.ToString() + "B");
                if (DatabaseHandler.HasChild == "true")
                {
                    DatabaseHandler.GetJSON("Statistics/" + Key.ToString() + "B");
                    yield return new WaitForSeconds(0.5f);
                    currentNum2 = int.Parse(DatabaseHandler.OutResult);
 
                }
                else
                {
                    currentNum2 = 0;
                }
                currentNum2++;
                DatabaseHandler.PostJSON("Statistics/" + Key.ToString() + "B", currentNum2.ToString());
                isReadingData = false;
                break;

            case "C":
                int currentNum3;
                DatabaseHandler.CheckJSON("Statistics/"+ Key.ToString() + "C");
                if (DatabaseHandler.HasChild == "true")
                {
                    DatabaseHandler.GetJSON("Statistics/" + Key.ToString() + "C");
                    yield return new WaitForSeconds(0.5f);
                    currentNum3 = int.Parse(DatabaseHandler.OutResult);
         
                }
                else
                {
                    currentNum3 = 0;
                }
                currentNum3++;
                DatabaseHandler.PostJSON("Statistics/" + Key.ToString() + "C", currentNum3.ToString());
                isReadingData = false;
                break;
            default:
                break;
        }


        yield break;
    }
}
