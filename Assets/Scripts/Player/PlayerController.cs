using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
	[Header("The layer used to identify the ground.")]
	[SerializeField] LayerMask groundLayer;

	[Header("The position of the ground check.")]
	[SerializeField] Transform groundCheck;

	[Header("The radius for the ground check. Gizmo mode could help.")]
	[Range(0f, 0.5f)][SerializeField] float groundCheckRadius = .2f;

	[Range(0, 1000f)][SerializeField] float jumpForce = 400f;

	[Header("Allow the player to control the character in the air.")]
	[SerializeField] bool airControl = true;

	[Header("How smooth is the character's movement.")]
	[Range(0, .3f)] [SerializeField] float movementSmoothTime = .05f;

	[Header("Is the player facing right by default?")]
	[SerializeField] bool isFacingRight = true;

	[Header("A graphical help for developers.")]
	[SerializeField] bool gizmoMode = false;

	bool isGrounded = true;
	Rigidbody2D rb;
	Vector3 velocity = Vector3.zero;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
	}

	public void Move (float move, bool jump) {
		// the player can move if he's grounded or
		// if the air control is enabled.
		if (isGrounded || airControl) {
			float force = 10f;
			Vector3 currentVelocity = rb.velocity;
			Vector3 targetVelocity = new Vector2(move * force, rb.velocity.y);

			rb.velocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref velocity, movementSmoothTime);
		}

		// the player can only jump if he's grounded.
		if (jump && isGrounded) {
			rb.AddForce(new Vector2(0f, jumpForce));
		}

		// Flip the  player directions
		if (move < 0.0f && isFacingRight) {
			Flip();
		} else if (move > 0.0f && !isFacingRight) {
			Flip();
		}
	}

	// Flip the sprite by using the inverse of the local scale on x
	void Flip() {
		isFacingRight = !isFacingRight;
		Vector2 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	// Debug with a graphical view for the developer.
	void OnDrawGizmosSelected() {
		if (gizmoMode) {
			DrawGroundCheck(groundCheck.position, groundCheckRadius);
		}
    }

	void DrawGroundCheck(Vector3 center, float radius) {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(center, radius);
	}
}
