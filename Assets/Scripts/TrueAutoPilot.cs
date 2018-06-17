using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TrueAutoPilot : MonoBehaviour
{
    public float Speed = 3f;
    public List<Vector2> DirectionList;
    public bool IsReckless;

    private Vector2 startingPoint;
    private Vector2 directionNow;
    private Vector3 angle;

    private float RotateSpeed = 5f;
    private float Radius = 0.1f;

    private Vector2 _centre;
    private float _angle;
    private int phase = 1;
    private bool phase2flag = false;



    // Use this for initialization
    void Start ()
	{
	    startingPoint = GetComponent<Transform>().position;
	    angle = GetComponent<Transform>().eulerAngles;
	    directionNow = DirectionList[0];
	    DirectionList.RemoveAt(0);
    }
	
	// Update is called once per frame
    void Update()
    {
        var transform = GetComponent<Transform>();
        var pointNow = transform.position;
        if (phase == 1)
        {
            goToDirection();
        }

        if (phase == 2 && IsReckless && !phase2flag)
        {
            goToDirection();
        }

        if (phase == 2 && !IsReckless && !phase2flag)
        {
            if (checkRightSide() == true)
            {
                phase2flag = true;
                Invoke("startAgain",0.25f);
            }
            else
            {
                phase++;
            }

        }

        if (phase == 3)
        {
            goToDirection();
        }

        if (Mathf.Abs(Vector2.Distance(pointNow, directionNow)) < 0.02f)
        {
            updateDirectionNow();
        }
    }

    private void startAgain()
    {
        phase2flag = false;
    }

    private bool checkRightSide()
    {
        var rb = GetComponent<Rigidbody2D>();
        
        for (var i = 10; i < 100; i++)
        {
            Vector2 direction = new Vector2(Mathf.Cos((rb.rotation + 90 - i) * Mathf.Deg2Rad),
                Mathf.Sin((rb.rotation + 90 - i) * Mathf.Deg2Rad));

            // check whats ahead of you
            RaycastHit2D hit = Physics2D.Raycast(rb.position + direction, direction);
            if (hit.collider != null && hit.distance <= 9f - i * 0.05f && hit.transform.tag=="car") {
                //If the object hit is less than or equal to n units away from this object.
                // brake
                return true;
            }
            
        }

        return false;

    }

    private void goToDirection()
    {
        var rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos((rb.rotation + 90) * Mathf.Deg2Rad),
            Mathf.Sin((rb.rotation + 90) * Mathf.Deg2Rad));

        // check whats ahead of you
        RaycastHit2D hit = Physics2D.Raycast(rb.position + direction, direction);
        if (hit.collider != null && hit.distance <= rb.velocity.magnitude + 0.5f) {
            //If the object hit is less than or equal to n units away from this object.
            // brake
            return;
        }
        var transform = GetComponent<Transform>();
        var pointNow = transform.position;
        // LookAt 2D
        // get the angle
        Vector3 norTar = (new Vector3(directionNow.x, directionNow.y, 0f) - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        // rotate to angle
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        transform.rotation = rotation;


        transform.position = Vector2.MoveTowards(pointNow, directionNow, Speed * Time.deltaTime);
    }

    private void updateDirectionNow() {
        if (DirectionList.Count==0)
        {
            Destroy(this);
        }
        else
        {
            directionNow = DirectionList[0];
            DirectionList.RemoveAt(0);
            phase=(phase%3) +1;
        }
    }


    private void turn()
    {
        _centre = transform.position;
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }

    private void turnLeft()
    {

    }

    private void turnRight()
    {

    }
}
