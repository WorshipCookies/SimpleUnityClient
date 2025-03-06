using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string Data_Type;
    public int FrameCount;
    public string msg;


    public PlayerData(string Data_Type, int FrameCount, string msg) 
    {
        this.Data_Type = Data_Type;
        this.FrameCount = FrameCount;
        this.msg = msg;
    }

    public string toJSON()
    {
        return JsonUtility.ToJson(this);
    }
 
}
