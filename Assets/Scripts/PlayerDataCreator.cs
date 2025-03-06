using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataCreator : MonoBehaviour
{
    

    // CONFIG THE DATA TYPE ACCORDING TO THE SPECIFIC MSG TYPES FOR PARSING!
    public static string SendMsg(string msg)
    {
        // This is just an example!
        return new PlayerData("UPDATE", Time.frameCount, msg).toJSON();
    }



}
