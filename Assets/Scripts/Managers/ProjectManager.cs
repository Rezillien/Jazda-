using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ProjectManager : MonoBehaviour
{

    private static ProjectManager _instance = null;
    public static LogManager LogManager = new LogManager();

    private ProjectManager()
    {
        
    }

    public static ProjectManager GetManager()
    {
        if (ProjectManager._instance == null)
        {
            ProjectManager._instance = new ProjectManager();
        }

        return ProjectManager._instance;
    }

    public SignalLightsManager GetSignalLightsManager()
    {
        return null;
    }

	// Use this for initialization
    void Start()
    {

    }

    private bool addStreetFlag = true;
	// Update is called once per frame
    void Update()
    {
//        if (addStreetFlag && Input.GetButtonDown("AddStreetLane"))
//        {
//            addStreetFlag = false;

//        }
    }

    
}
