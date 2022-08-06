using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData 
{
    public int ID { get; set; }

    public int SceneID { get; set; }
    public int PersonID { get; set; }
    public string DialogueText { get; set; }
    public string Expression { get; set; }
    public int IsBigUI { get; set; }
    public int IsBigOption { get; set; }
    public int NextDialogueID { get; set; }
    public int NextOperation { get; set; }
    public int NextEffectID { get; set; }

    public string PreviousChosen { get; set; }

    public string NextMultipleDialogue { get; set; }

    public string Option1 { get; set; }
    public int Option1NextDialogue { get; set; }
    public string Option2 { get; set; }
    public int Option2NextDialogue { get; set; }

    public string Option3 { get; set; }
    public int Option3NextDialogue { get; set; }

}


