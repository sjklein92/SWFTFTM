using UnityEngine;
using System.Collections;

public class PogoManCharacterController : MonoBehaviour {

	bool grounded = false;
	float maxSpeed = 7;
	public Transform groundCheck1, groundCheck2;
	float groundRadius = 0.002f;
	public LayerMask ground;
	float jumpForce = 200;
	
	void FixedUpdate () {
		if(Physics2D.OverlapCircle(groundCheck1.position, groundRadius, ground) ||
		   Physics2D.OverlapCircle(groundCheck2.position, groundRadius, ground))
			grounded = true;
		else
			grounded = false;

		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2(maxSpeed*move, rigidbody2D.velocity.y);

		if(grounded){
			rigidbody2D.AddForce (new Vector2(0, jumpForce));
		}
	}
}
