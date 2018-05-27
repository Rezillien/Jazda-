using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autopilot : MonoBehaviour {

    float speedForce = 15f;
    float torqueForce = -200f;
    float speedlimit = 10f;
    Rigidbody2D rb;


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

        Vector2 direction = new Vector2(Mathf.Cos((rb.rotation+90) * Mathf.Deg2Rad),
                                        Mathf.Sin((rb.rotation+90) * Mathf.Deg2Rad));

        // check whats ahead of you
        RaycastHit2D hit = Physics2D.Raycast(rb.position + direction, direction);

        //If something was hit.
        if (hit.collider != null && hit.distance <= 4.0f)
        {
            //If the object hit is less than or equal to 6 units away from this object.
            
                // brake
                if (Vector2.Dot(direction, rb.velocity) > 0)
                    rb.AddForce(transform.up * -speedForce / 2f);
                else
                    rb.velocity = new Vector2(0, 0);
            

           
                
            
        } else {
            if (rb.velocity.magnitude < speedlimit)
                rb.AddForce(transform.up * speedForce);
        }


        if (Input.GetButton("Accelerate"))
        {
            rb.AddForce(transform.up * speedForce);

            // Consider using rb.AddForceAtPosition to apply force twice, at the position
            // of the rear tires/tyres
        }
        if (Input.GetButton("Brakes"))
        {
            rb.AddForce(transform.up * -speedForce / 2f);

            // Consider using rb.AddForceAtPosition to apply force twice, at the position
            // of the rear tires/tyres
        }

        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
        rb.angularVelocity = Input.GetAxis("Horizontal") * tf;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //this.speedForce = 0;
        //this.torqueForce = 0;
    }

}
