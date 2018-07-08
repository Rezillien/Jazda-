using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    private StreamWriter writer;
    public string LogFileName="log";
	// Use this for initialization

    public LogManager()
    {
        lock (this)
        {


            writer = File.CreateText("Assets/Statistics/" + LogFileName);
            writer.WriteLine("log type;creator name;statistics");
            writer.Close();
        }
    }
	void Start () {
	    
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void logTime(string creatorName, DateTime creationTime)
    {
        lock (this)
        {
            writer = File.AppendText("Assets/Statistics/" + LogFileName);

            var time = DateTime.Now - creationTime;

            writer.WriteLine("time;" + creatorName + ";" + time.TotalSeconds);
            writer.Close();
        }
    }

    public void logCrash(string creatorName, int crashCount)
    {
        lock (this)
        {
            writer = File.AppendText("Assets/Statistics/" + LogFileName);
            print(creatorName);
            print(crashCount);
            writer.WriteLine("crash;" + creatorName + ";" + crashCount);
            writer.Close();
        }

    }

    public void OnDestroy()
    {
        
    }
}
