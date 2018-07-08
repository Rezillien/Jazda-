using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autopilot : MonoBehaviour
{

    public float speedForce = 15f;
    public float brakeForce = 30f;
    float torqueForce = -200f;
    public List<Vector2> DirectionList;
    public float speedlimit = 6.0f;
    public float carefullness = 1.0f;
    public int auto_drive = 1;
    Rigidbody2D rb;
    public System.DateTime creation_time;
    public string creator_name;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // check for button up/down here, then set a bool that you will use in FixedUpdate
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = new Vector2(Mathf.Cos((rb.rotation + 90) * Mathf.Deg2Rad),
            Mathf.Sin((rb.rotation + 90) * Mathf.Deg2Rad));

        // check whats ahead of you
        RaycastHit2D hit = Physics2D.Raycast(rb.position + direction, direction);

        Vector2 direction1 = new Vector2(Mathf.Cos((rb.rotation + 115) * Mathf.Deg2Rad),
            Mathf.Sin((rb.rotation + 115) * Mathf.Deg2Rad));

        Vector2 direction2 = new Vector2(Mathf.Cos((rb.rotation + 65) * Mathf.Deg2Rad),
            Mathf.Sin((rb.rotation + 65) * Mathf.Deg2Rad));

        RaycastHit2D hit1 = Physics2D.Raycast(rb.position + direction1, direction1);
        RaycastHit2D hit2 = Physics2D.Raycast(rb.position + direction2, direction2);


        //If something was hit.
        if (hit.collider != null && hit.distance <= rb.velocity.magnitude * carefullness + 0.5f)
        {
            //If the object hit is less than or equal to n units away from this object.
            // brake
            if (Vector2.Dot(direction, rb.velocity) > 0)
                rb.AddForce(transform.up * -brakeForce);
            else
                rb.velocity = new Vector2(0, 0);
        } else if ((hit.collider != null && hit.distance <= rb.velocity.magnitude * carefullness + 0.5f)
                   || (hit.collider != null && hit.distance <= rb.velocity.magnitude * carefullness + 0.5f))
        {
            if (Vector2.Dot(direction, rb.velocity) > 0)
                rb.AddForce(transform.up * -brakeForce*0.4f);
            else
                rb.velocity = new Vector2(0, 0);
        }
        else
        {
            if (rb.velocity.magnitude < speedlimit)
                rb.AddForce(transform.up * speedForce * auto_drive);
        }


        if (Input.GetButton("Accelerate"))
        {
            if (rb.velocity.magnitude < speedlimit)
            {
                rb.AddForce(transform.up * speedForce);
            }
            // Consider using rb.AddForceAtPosition to apply force twice, at the position
            // of the rear tires/tyres
        }

        if (Input.GetButton("Brakes"))
        {
            rb.AddForce(transform.up * -brakeForce);

            // Consider using rb.AddForceAtPosition to apply force twice, at the position
            // of the rear tires/tyres
        }

        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
        rb.angularVelocity = Input.GetAxis("Horizontal") * tf;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        this.speedForce = 0;
        //this.torqueForce = 0;
    }
}