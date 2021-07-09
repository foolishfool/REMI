using Firebase.Database;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class DataHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]
    public Dictionary<int, DialogueData> AllDialogueDatas = new Dictionary<int, DialogueData>();
    [HideInInspector]
    public Dictionary<int, Question> AllQuestionData = new Dictionary<int, Question>();


    public int CurrentDialogueID;
    public int CurrentPersonID;

    public string UserID;


    static DataHandler instance;
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



}
