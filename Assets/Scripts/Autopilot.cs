using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autopilot : MonoBehaviour
{
    public Car2DController controller = new SimpleCarController();
    public System.DateTime creation_time;
    public string creator_name;
    
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
        controller.raycast(rb, transform);

        if (Input.GetButton("Accelerate"))
			controller.accelerate(rb, transform);

        if (Input.GetButton("Brakes"))
			controller.brake(rb, transform);
			
		controller.turn(rb, transform, Input.GetAxis("Horizontal"));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //this.speedForce = 0;
        //this.torqueForce = 0;
    }
}
