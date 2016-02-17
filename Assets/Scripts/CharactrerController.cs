using UnityEngine;
using System.Collections;

public class CharactrerController : MonoBehaviour {
    private float moveForce = 4;
    public float maxMoveForce = 3;

    public float jumpForce = 2;
    private bool isJumping = false;
    private bool isGrounded = false;

    private GameObject parent;
    private Rigidbody2D body;
    private BoxCollider2D groundCheck;

	void Start () {
        parent = this.gameObject;
        body = parent.GetComponent<Rigidbody2D>();
        groundCheck = parent.transform.FindChild("groundCheck").GetComponent<BoxCollider2D>();

        isGrounded = groundCheck.IsTouchingLayers(LayerMask.NameToLayer("Obstacle")); //Layer 8 is obstacle layer
	}

	void Update () {
        isGrounded = groundCheck.IsTouchingLayers(1<<LayerMask.NameToLayer("Obstacle"));
        float hAxis = Input.GetAxis("Horizontal");

        if (isGrounded && hAxis != 0)
        {
            body.AddForce(new Vector2(0, 10));
        }
        if (body.velocity.x >= -maxMoveForce && body.velocity.x <= maxMoveForce)
        {
            body.AddForce(new Vector2(hAxis, isJumping ? jumpForce : 0));
        }
        else
        {
            if(body.velocity.x >= maxMoveForce && hAxis < 0)
            {
                body.AddForce(new Vector2(hAxis, isJumping ? jumpForce : 0));
            }
            else if(body.velocity.x <= -maxMoveForce && hAxis > 0)
            {
                body.AddForce(new Vector2(hAxis, isJumping ? jumpForce : 0));
            }
            body.AddForce(new Vector2(0, isJumping ? jumpForce : 0));
        }
	}
}
