using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour {
	public PlayerController playerController;
	[Range(0, 100)][SerializeField] float speed = 30f;
	float horizontalMove = 0f;
	bool isJumping = false;

	void Update() {
		horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

		if (Input.GetButtonDown("Jump")) {
			isJumping = true;
		}
	}

	void FixedUpdate () {
		playerController.Move(horizontalMove * Time.fixedDeltaTime, isJumping);
		isJumping = false;
	}
}
