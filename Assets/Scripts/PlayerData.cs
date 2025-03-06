using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string ID;
    public int TimeStamp;
    public string msg;


    public PlayerData(string ID, int TimeStamp, string msg) 
    {
        this.ID = ID;
        this.TimeStamp = TimeStamp;
        this.msg = msg;
    }

    public string toJSON()
    {
        return JsonUtility.ToJson(this);
    }
 
}
