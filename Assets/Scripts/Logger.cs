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


    private string logDirectory;
    private string logFilePath;
    public void setup()
    {
        logDirectory = Path.Combine(Application.dataPath, "Logs");

        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
            Debug.Log($"Created directory: {logDirectory}");
        }

        logFilePath = Path.Combine(logDirectory, "Log" + DateTime.Now.ToString("dd-MM-yyyy-HHmmss") + ".txt");

        Debug.Log("Log file: " + logFilePath);

        ExerciseSingleton.Instance.LoggerEvent += logger;

    }

    public void logger(string message)
    {
        string logMessage = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} - {message}";

        File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
    }



}
#endif