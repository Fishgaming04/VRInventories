using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System;

#if UNITY_EDITOR
public class Logger
{
    private string logFilePath;


    public void stetup()
    {
        logFilePath = new DirectoryInfo(Application.dataPath).ToString() + "/Logs";

        if (!Directory.Exists(logFilePath))
        {
            Directory.CreateDirectory(logFilePath);
            Debug.Log($"Created directory: {logFilePath}");
        }
        Debug.Log("Data Path: " + logFilePath);
        
        ExperimentSingleton.Instance.LoggerEvent += logger;

    }

    public void logger(string message)
    {
        string logMessage = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} - {message}";

        File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
    }



}
#endif