using UnityEngine;
using System.Collections;

public class CharactrerController : MonoBehaviour {
    public float moveForceMultiplier = 4;
    public float maxMoveForce = 3;

    public float jumpForce = 200;
    private bool isJumping = false;
    private bool isGrounded = false;

    private GameObject parent;
    private Rigidbody2D body;
    private BoxCollider2D groundCheck;

	void Start () {
        parent = this.gameObject;
        body = parent.GetComponent<Rigidbody2D>();
        groundCheck = parent.transform.FindChild("groundCheck").GetComponent<BoxCollider2D>();

        isGrounded = groundCheck.IsTouchingLayers(1<<LayerMask.NameToLayer("Obstacle")); //Checks if collider is touching an obstacle
	}

	void Update () {
        isGrounded = groundCheck.IsTouchingLayers(1<<LayerMask.NameToLayer("Obstacle"));
		float hAxis = Input.GetAxis("Horizontal")*moveForceMultiplier;
		
		if(Input.GetKey("space") && isGrounded){
			body.AddForce(new Vector2(0,jumpForce));
		}
		
        if (isGrounded && hAxis != 0)
        {
            body.AddForce(new Vector2(0, 10)); //Adds a force to avoid friction when on obstacle
        }
		
        if (body.velocity.x >= -maxMoveForce && body.velocity.x <= maxMoveForce) //If it has not reached the limit
        {
            body.AddForce(new Vector2(hAxis, 0)); //Add the force of the horizontal input axis
        }
        else if(body.velocity.x >= maxMoveForce && hAxis < 0 || body.velocity.x <= -maxMoveForce && hAxis > 0) 
		{
			body.AddForce(new Vector2(hAxis, 0));
        }
	}
}
