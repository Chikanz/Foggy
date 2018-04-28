using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class postProcessEnabler : MonoBehaviour {



	// Use this for initialization
	void Start ()
    {
        GetComponent<PostProcessingBehaviour>().enabled = true;
        GetComponent<SSMS.SSMSGlobalFog>().enabled = true;
        GetComponent<SSMS.SSMS>().enabled = true;
        var g = GameObject.Find("No place trees");
        if(g) g.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
