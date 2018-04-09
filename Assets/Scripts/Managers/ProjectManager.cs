using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{

    private ProjectManager _instance = null;
    private readonly SignalLightsManager _signalLightsManager;

    private ProjectManager()
    {
        _signalLightsManager = new SignalLightsManager();
    }

    public ProjectManager GetManager()
    {
        if (this._instance == null)
        {
            this._instance = new ProjectManager();
        }

        return this._instance;
    }

    public SignalLightsManager GetSignalLightsManager()
    {
        return this._signalLightsManager;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
