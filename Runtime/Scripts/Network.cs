using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;
using TouchSocket.Core;
using TouchSocket.Sockets;
using UnityEngine;

public class Network : MonoBehaviour
{

    public int Port = 5555;

    private UdpSession _udpClient;

    private ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _deviceReadMessages;

    private StreamWriter writer;
    private string _txtPath;
    void Start()
    {
        _txtPath = Application.dataPath + "stream.data";
        _deviceReadMessages = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
        _udpClient = new UdpSession();

        _udpClient.Received += ReceiveMsg;

        _udpClient.Setup(new TouchSocketConfig()
                .SetBindIPHost(new IPHost(Port))
                .SetUdpDataHandlingAdapter(() => new NormalUdpDataHandlingAdapter()))
            .Start();

        Debug.Log("UDP Client Start!!");

    }


    private void WriteIntoTxt(string message)
    {
        FileInfo file = new FileInfo(_txtPath);
        if (!file.Exists)
        {
            writer = file.CreateText();
        }
        else
        {
            writer = file.AppendText();
        }

        writer.WriteLine(message);
        writer.Flush();
        writer.Dispose();
        writer.Close();
    }

    private void ReceiveMsg(EndPoint endpoint, ByteBlock byteblock, IRequestInfo requestinfo)
    {
        string msg = Encoding.UTF8.GetString(byteblock.Buffer, 0, byteblock.Len);
        Debug.Log(msg);
        //WriteIntoTxt(msg);
        JObject obj = JObject.Parse(msg);
        var jps = obj.Properties();
        foreach (var jp in jps)
        {
            string role_name = jp.Name;

            JToken token = obj.GetValue(role_name);
            JArray array = token["Parameter"] as JArray;
            ConcurrentDictionary<string, string> _deviceMsg = new ConcurrentDictionary<string, string>();
            for (int i = 0; i < array.Count; i++)
            {
                JObject obj1 = array[i] as JObject;
                string key = obj1.GetValue("Name").ToString();
                string value = obj1.GetValue("Value").ToString();
                _deviceMsg.TryAdd(key, value);
            }
            if (_deviceReadMessages.ContainsKey(role_name))
            {
                _deviceReadMessages[role_name] = _deviceMsg;
            }
            else
            {
                _deviceReadMessages.TryAdd(role_name, _deviceMsg);
            }
            
        }
        //JToken token = obj.GetValue("Device1_"+Port);
        //JArray array = token["Parameter"] as JArray;
        //for (int i = 0; i < array.Count; i++)
        //{
        //    JObject obj1 = array[i] as JObject;
        //    string key = obj1.GetValue("Name").ToString();
        //    string value = obj1.GetValue("Value").ToString();
        //    _device1ReadMessages.AddOrUpdate(key, value);
        //}

        //JToken token2 = obj.GetValue("Device2_"+Port);
        //JArray array2 = token2["Parameter"] as JArray;
        //for (int i = 0; i < array.Count; i++)
        //{
        //    JObject obj1 = array2[i] as JObject;
        //    string key = obj1.GetValue("Name").ToString();
        //    string value = obj1.GetValue("Value").ToString();
        //    _device2ReadMessages.AddOrUpdate(key, value);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float Convert2Angle(string role_name, string key)
    {
        float angle = 0;
        if (!string.IsNullOrEmpty(role_name))
        {
            if (!_deviceReadMessages.ContainsKey(role_name))
            {
                return 0;
            }
            string str = _deviceReadMessages[role_name][key];

            if (!string.IsNullOrEmpty(str))
            {
                angle = Single.Parse(str);
            }
        }
        return angle;
    }

    public bool Convert2Bool(string role_name, string key)
    {
        bool flag = false;
        if (!string.IsNullOrEmpty(role_name))
        {
            if (!_deviceReadMessages.ContainsKey(role_name))
            {
                return false;
            }
            string str = _deviceReadMessages[role_name][key];

            if (!string.IsNullOrEmpty(str))
            {
                flag = bool.Parse(str);
            }
        }
        return flag;
    }

    public void SendVibrationMsg(string RoleName, string IP, VibrationData data)
    {
        var json_role = new JObject();
        var json_one = new JObject();
        var parameterArrayLeft = new JArray();
        var parameterArrayRight = new JArray();

        var _Lpara_active = new JObject();
        _Lpara_active.Add("Name", "Vibrators");
        _Lpara_active.Add("Value", data.Virbators[0].ActiveCommand);

        var _Lpara_duration = new JObject();
        _Lpara_duration.Add("Name", "Duration");
        _Lpara_duration.Add("Value", data.Virbators[0].Duration);

        var _Lpara_amplitude = new JObject();
        _Lpara_amplitude.Add("Name", "Amplitude");
        _Lpara_amplitude.Add("Value", data.Virbators[0].Amplitude);

        parameterArrayLeft.Add(_Lpara_active);
        parameterArrayLeft.Add(_Lpara_duration);
        parameterArrayLeft.Add(_Lpara_amplitude);

        var _Rpara_active = new JObject();
        _Rpara_active.Add("Name", "Vibrators");
        _Rpara_active.Add("Value", data.Virbators[1].ActiveCommand);

        var _Rpara_duration = new JObject();
        _Rpara_duration.Add("Name", "Duration");
        _Rpara_duration.Add("Value", data.Virbators[1].Duration);

        var _Rpara_amplitude = new JObject();
        _Rpara_amplitude.Add("Name", "Amplitude");
        _Rpara_amplitude.Add("Value", data.Virbators[1].Amplitude);

        parameterArrayRight.Add(_Rpara_active);
        parameterArrayRight.Add(_Rpara_duration);
        parameterArrayRight.Add(_Rpara_amplitude);

        json_one.Add("LeftHand", parameterArrayLeft);
        json_one.Add("RightHand", parameterArrayRight);
        json_role.Add(RoleName, json_one);

        Debug.Log(json_role.ToJson());
        _udpClient.Send(new IPEndPoint(IPAddress.Parse(IP), 8920), Encoding.UTF8.GetBytes(json_role.ToJson()));
    }

    private void OnDestroy()
    {
        _udpClient.Received -= ReceiveMsg;
        _udpClient.Stop();
        _udpClient.Dispose();
    }
}
