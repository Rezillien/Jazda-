using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts.FileObjects;

public class CarCreator : MonoBehaviour {
    public string name;

	// Use this for initialization
	void Start ()
	{
        //StreamWriter sr = File.CreateText("Assets/Statistics/" + name);
        //sr.Close();
        StartCoroutine(CreateCars());
	}

    public float x;
    public float y;
    public float angle;
    public GameObject car;
    public float interval;

	// Update is called once per frame
	void Update () {
		
	}

    public List<Vector2> list;

    private IEnumerator CreateCars()
    {
        while (true)
        {

            

            GameObject prefab = this.car;

            GameObject obj = Instantiate(prefab) as GameObject;
            var car = obj.GetComponent<TrueAutoPilot>();
//            car.creation_time = System.DateTime.Now;
//            car.creator_name = name;
            car.DirectionList = new List<Vector2>(list);


            Transform transform = obj.GetComponent<Transform>();

            Vector3 position = new Vector3 { x = this.x, y = this.y };
            Vector3 angle = new Vector3 { x = 0, y = 0, z = this.angle };
            //                Vector3 scale = new Vector3 { x = lane.scalex, y = lane.sc
            transform.SetPositionAndRotation(position, new Quaternion(0, 0, 0, 0));
            //            transform.RotateAround(middle, Vector3.forward, angle);
            transform.eulerAngles = angle;
            yield return new WaitForSeconds(interval);
        }
    }
}
