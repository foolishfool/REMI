using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UserRecord : ScriptableObject
{
    [HideInInspector]
    public string associatedSheet = "2PACX-1vTEZxUDj984K811xz3XEqgkQBfR84YD67WstJhw2-akclUVhgv22shMvufvGAp7SyJQdHt1Oit7hYio";
    [HideInInspector]
    public string associatedWorksheet = "Stats";

    public string Q1;
    public string Q2;
    public string Q3;
    public string Q4;
    public string Q5;
    public string Q6;
    public int Score;

    internal void UpdateStats(List<GSTU_Cell> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "Q1":
                    {
                        Q1 = list[i].value;
                        break;
                    }
                case "Q2":
                    {
                        Q2 = list[i].value;
                        break;
                    }
                case "Q3":
                    {
                        Q3 = list[i].value;
                        break;
                    }
                case "Q4":
                    {
                        Q4 = list[i].value;
                        break;
                    }
                case "Q5":
                    {
                        Q4 = list[i].value;
                        break;
                    }
                case "Q6":
                    {
                        Q4 = list[i].value;
                        break;
                    }
                case "Score":
                    {
                        Score = int.Parse(list[i].value);
                        break;
                    }
            }
        }
    }

    internal void UpdateStats(GstuSpreadSheet ss)
    {

        Q1 = ss[name, "Q1"].value;
        Q2 = ss[name, "Q2"].value;
        Q3 = ss[name, "Q3"].value;
        Q4 = ss[name, "Q4"].value;
        Q5 = ss[name, "Q5"].value;
        Q6 = ss[name, "Q6"].value;
        Score = int.Parse(ss[name, "Score"].value);

    }
}

