using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor.PackageManager;

public class ClientComm : MonoBehaviour
{
    [SerializeField] int SocketPort;
    [SerializeField] string IPAddress;
    [SerializeField] string messageToSend = "Hello Bob!";

    private TcpClient _tcpClient;
    private NetworkStream _stream;
    private Thread clientReceiveThread;


    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    // Update is called once per frame
    void Update()
    {
        //disable this if you are sending from another script or a button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageToServer(messageToSend);
        }
    }


    private void ConnectToServer()
    {
        try
        {
            _tcpClient = new TcpClient(IPAddress, SocketPort);
            _stream = _tcpClient.GetStream();
            Debug.Log("Connected to Server Successfully!");

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e.ToString());
        }
    }

    // Data Receiving Thread - Constantly Listening for Messages
    private void ListenForData()
    {
        try
        {
            byte[] bytes = new byte[1024];
            while (true)
            {
                // Check if there's any data available on the network stream
                if (_stream.DataAvailable)
                {
                    int length;
                    // Read incoming stream into byte array.
                    while ((length = _stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message.
                        string serverMessage = Encoding.UTF8.GetString(incomingData);
                        Debug.Log("Server message received: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    // Send Messages to the Server!
    public void SendMessageToServer(string message)
    {
        if (_tcpClient == null || !_tcpClient.Connected)
        {
            Debug.LogError("Client not connected to server.");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message);
        _stream.Write(data, 0, data.Length);
        Debug.Log("Sent message to server: " + message);
    }

    private void OnApplicationQuit()
    {
        DisconnectFromServer();
    }

    public void DisconnectFromServer()
    {
        if (_stream != null)
            _stream.Close();
        if (_tcpClient != null)
            _tcpClient.Close();
        if (clientReceiveThread != null)
            clientReceiveThread.Abort();
    }


}
