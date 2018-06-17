using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CarDestroyer : MonoBehaviour {
    System.IO.FileStream oFileStream = null;

    // Use this for initialization
    void Start () {
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "car")
        {
//            var car = coll.gameObject.GetComponent<Autopilot>();
//            var time = System.DateTime.Now - car.creation_time;
//            string creator_name = car.creator_name;
            //StreamWriter writer = new StreamWriter("Assets/Statistics/" + creator_name, true);
            //writer.WriteLine(time.TotalSeconds);
            //writer.Close();
            Destroy(coll.gameObject);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
