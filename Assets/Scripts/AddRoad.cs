using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddRoad : MonoBehaviour
{

    public GameObject roadPrefab;
    Vector3 startPosition;


    // Use this for initialization
    void Start()
    {
        startPosition = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = StartPositionOfTheRoad();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 endPosition = EndPositionOfTheRoad();
            CreateRoad(startPosition, endPosition);
        }
    }

    private void CreateRoad(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject roadObject = Instantiate(roadPrefab) as GameObject;

        Transform roadTransform = roadObject.GetComponent<Transform>();

        Vector3 middlePointOfRoad = (startPosition + endPosition) / 2;
        float angle = Vector3.SignedAngle(
            Vector3.right,
            endPosition - startPosition,
            Vector3.forward
            );
        float lengthOfRoad = Vector3.Distance(startPosition, endPosition);

        TransformRoad(roadTransform, middlePointOfRoad, angle, lengthOfRoad);
    }

    private void TransformRoad(Transform roadTransform, Vector3 middlePointOfRoad, float angle, float lengthOfRoad)
    {
        roadTransform.Translate(middlePointOfRoad);
        roadTransform.RotateAround(middlePointOfRoad, Vector3.forward, angle);
        roadTransform.localScale += new Vector3(lengthOfRoad, 0, 0);
    }

    private Vector3 EndPositionOfTheRoad()
    {
        Vector3 endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPosition.z = 0;
        return endPosition;
    }

    private Vector3 StartPositionOfTheRoad()
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0;
        return startPosition;
    }
}
