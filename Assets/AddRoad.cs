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

            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject obj = Instantiate(prefab, new Vector2(position.x, position.y), Quaternion.identity) as GameObject;






        }

    }


}
