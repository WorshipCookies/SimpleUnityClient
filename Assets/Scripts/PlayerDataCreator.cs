using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataCreator : MonoBehaviour
{
    
    public static string SendMsg(string msg)
    {
        return new PlayerData("UPDATE", Time.frameCount, msg).toJSON();
    }



}
