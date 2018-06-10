using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "car")
            Destroy(coll.gameObject);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
