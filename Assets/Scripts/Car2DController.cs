using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car2DController
{
    public float speedForce = 15f;
    public float brakeForce = 30f;
    public float torqueForce = -300f;
    public float speedlimit = 10f;
    public float carefullness = 1.0f;
    public int auto_drive = 0;
    
    public virtual void raycast(Rigidbody2D rb, Transform transform) {
		float phi = (rb.rotation + 90) * Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(phi), Mathf.Sin(phi));
            
		RaycastHit2D hit = Physics2D.Raycast(rb.position + direction, direction);

        //If something was hit.
        if (hit.collider != null && hit.distance <= rb.velocity.magnitude * carefullness + 0.5f)
        {
            //If the object hit is less than or equal to n units away from this object.
            // brake
            if (Vector2.Dot(direction, rb.velocity) > 0)
                rb.AddForce(transform.up * -brakeForce);
            else
                rb.velocity = new Vector2(0, 0);
        }
        else
        {
            if (rb.velocity.magnitude < speedlimit)
                rb.AddForce(transform.up * speedForce * auto_drive);
        }
	}
	
	public virtual void accelerate(Rigidbody2D rb, Transform transform) {
		if (rb.velocity.magnitude < speedlimit)
        {
            rb.AddForce(transform.up * speedForce);
            
			// Consider using rb.AddForceAtPosition to apply force twice, at the position
			// of the rear tires/tyres
		}
    }
    
    public virtual void brake(Rigidbody2D rb, Transform transform) {
		if (rb.velocity.magnitude < speedlimit)
        {
            rb.AddForce(transform.up * -brakeForce);
            
			// Consider using rb.AddForceAtPosition to apply force twice, at the position
			// of the rear tires/tyres
		}
    }
    
    public virtual void turn(Rigidbody2D rb, Transform transform, float turnAxis) {
		float turn = Mathf.Clamp(turnAxis, -.2f, .2f);
		float direction = Mathf.Sign(rb.rotation * Vector2.SignedAngle(Vector2.up, rb.velocity));
		rb.AddForce(transform.up * -direction * brakeForce * Mathf.Abs(turn));
		
		float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / speedlimit);
        rb.angularVelocity = turnAxis * tf;
	}
}
