
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


public class JsonWrapper
{
    [Serializable]
    private class JsonData<T>
    {
        public T playerDatas;
    }

    public static void SaveDatas<T>(T datas, string path)
    {
        JsonData<T> jsonData = new JsonData<T>();
        jsonData.playerDatas = datas;
        string dataJson = JsonUtility.ToJson(jsonData);
        if(!path.StartsWith('/'))
            path = "/" + path;
        File.WriteAllText(Application.dataPath + path, dataJson);
    }
    public static T LoadDatas<T>(string path)
    {
        if(!path.Equals("/")) 
            path = "/" + path;
        string dataJson = File.ReadAllText(Application.dataPath + path);
        JsonData<T> jsonData = JsonUtility.FromJson<JsonData<T>>(dataJson);
        return jsonData.playerDatas;
    }
}
