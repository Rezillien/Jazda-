using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DController : MonoBehaviour
{

    public bool isControllable = true;

    float speedForce = 300f;
    float torqueForce = -200f;
    float driftFactorSticky = 0.7f;
    float driftFactorSlippy = 0.1f;
    float maxStickyVelocity = 2.5f;
    float minSlippyVelocity = 1.5f;

    private float breakes = 300f;




    // Use this for initialization
    void Start()
    {
        if (isControllable == false)
        {
            this.speedForce = 0;
            this.torqueForce = 0;
        }
    }

    void Update()
    {
        // check for button up/down here, then set a bool that you will use in FixedUpdate
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Rigidbody2D rb = GetComponent<Rigidbody2D>();



        float driftFactor = driftFactorSticky;
        if (RightVelocity().magnitude > maxStickyVelocity)
        {
            driftFactor = driftFactorSlippy;
        }

        rb.velocity = ForwardVelocity() + RightVelocity() * driftFactor;

        if (Input.GetButton("Accelerate"))
        {
            rb.AddForce(transform.up * speedForce);

            // Consider using rb.AddForceAtPosition to apply force twice, at the position
            // of the rear tires/tyres
        }
        if (Input.GetButton("Brakes"))
        {
            rb.AddForce(transform.up * -speedForce * 3);

            // Consider using rb.AddForceAtPosition to apply force twice, at the position
            // of the rear tires/tyres
        }

        // If you are using positional wheels in your physics, then you probably
        // instead of adding angular momentum or torque, you'll instead want
        // to add left/right Force at the position of the two front tire/types
        // proportional to your current forward speed (you are converting some
        // forward speed into sideway force)
        float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 2);
        rb.angularVelocity = Input.GetAxis("Horizontal") * tf;



    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //this.speedForce = 0;
        //this.torqueForce = 0;
    }

    private float getDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, new Vector2(0f,0f));
        
        //If something was hit.
        if (hit.collider != null)
        {
            //If the object hit is less than or equal to 6 units away from this object.
            if (hit.distance <= 6.0f)
            {
                Debug.Log("Enemy In Range!");
            }
        }

        return 0f;
    }

    public void SetIsControllable(bool isControllable)
    {
        this.isControllable = isControllable;
    }

}
