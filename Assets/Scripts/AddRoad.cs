using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddRoad : MonoBehaviour
{

    public GameObject prefab;
    Vector3 start_position;


    // Use this for initialization
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            start_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            start_position.z = 0;
        }


        if (Input.GetMouseButtonUp(0))
        {
            Vector3 end_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            end_position.z = 0;


            GameObject obj = Instantiate(prefab) as GameObject;

            Transform transform = obj.GetComponent<Transform>();

            Vector3 middle = (start_position + end_position) / 2;
            float angle = Vector3.SignedAngle(
                Vector3.right,
                end_position - start_position,
                Vector3.forward
                );
            float length = Vector3.Distance(start_position, end_position);

            transform.Translate(middle);
            transform.RotateAround(middle, Vector3.forward, angle);
            transform.localScale += new Vector3(length, 0, 0);
        }
    }


}
