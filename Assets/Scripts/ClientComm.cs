using System.Collections;
using UnityEngine;
using NativeWebSocket;
using System.Text;
using System.Threading.Tasks;

public class ClientComm : MonoBehaviour
{
    private WebSocket websocket;
    [SerializeField] string serverUrl = "wss://mongodb-gamelog.onrender.com"; // Replace with your WebSocket server URL

    async void Start() 
    {
        websocket = new WebSocket(serverUrl);

        websocket.OnOpen += () => Debug.Log("Connected to WebSocket Server!");
        websocket.OnMessage += (bytes) =>
        {
            string message = Encoding.UTF8.GetString(bytes);
            Debug.Log("Received from server: " + message);
        };
        websocket.OnClose += (e) => Debug.Log("Disconnected from server");

        await websocket.Connect();
    }

    // Function that sends msgs to websocket
    public async void SendMessageToServer(string message)
    {
        Debug.Log("This is a test!");
        if (websocket.State == WebSocketState.Open)
        {
            string jsonMessage = PlayerDataCreator.SendMsg(message);
            Debug.Log($"{jsonMessage}");
            await websocket.SendText(jsonMessage);
            Debug.Log("Sent message: " + jsonMessage);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
