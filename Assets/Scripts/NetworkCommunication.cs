using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkCommunication{
    IPAddress serverAddress;
    int serverPort;
    IPEndPoint serverEndPoint;
    public NetworkCommunication(String _ip, int _port) {
        serverPort = _port;
        serverAddress = IPAddress.Parse(_ip);
        serverEndPoint = new IPEndPoint(serverAddress, serverPort);
    }

    public String RequestService(String _r) {
        try {
            Socket clientRequestSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientRequestSocket.Connect(serverEndPoint);
            clientRequestSocket.Send(Encoding.UTF8.GetBytes(_r));
            byte[] buffer = new byte[1024 * 1024];
            int msgLen = clientRequestSocket.Receive(buffer);
            String recMsg = Encoding.UTF8.GetString(buffer, 0, msgLen);
            clientRequestSocket.Close();
            return recMsg;
        } catch (Exception e) {
            Console.WriteLine("Exception : " + e);
            return e.ToString();
        }

    }

    public String [] QueryResults() {
        String msg = "QueryResults";
        String[] msg_para = RequestService(msg).Split(':');
        String[] results = new String[2];
        if (msg_para.Length > 1) {
            results = msg_para[1].Split('|');
            for (int i = 0; i < results.Length; ++i) {
                results[i] = results[i].Replace("(", "").Replace(")", "").Replace("'", "");
            }
        }
        return results;
    }

    public String UploadResult(String _n,int _s) {
        Debug.Log("Upload Result");
        String msg = "UploadResult:" + _n + ":" + _s;
        return RequestService(msg);
    }
}
