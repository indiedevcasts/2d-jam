using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour {
	[Header("The target that the camera follows.")]
	[SerializeField] private Transform target;

	[Header("The time the camera takes to reach the target's position.")]
	[Range(0f, 50f)][SerializeField] private float smoothTime = .15f;

	[SerializeField] private bool clampOnXMin = false;
	[SerializeField] private float xMin;

	[SerializeField] private bool clampOnXMax = false;
	[SerializeField] private float xMax;

	[SerializeField] private bool clampOnYMin = false;
	[SerializeField] private float yMin;

	[SerializeField] private bool clampOnYMax = false;
	[SerializeField] private float yMax;

	private Vector3 velocity = Vector3.zero;

	// Ensure that all actions in Update of FixedUpdate are done
	// before the camera tracks the target's position.
	void LateUpdate() {
		followTarget(target.transform.position, clampOnXMin, clampOnXMax, clampOnYMin, clampOnYMax);
	}

	void followTarget(Vector3 targetPos, bool clampOnXMin, bool clampOnXMax, bool clampOnYMin, bool clampOnYMax) {
		// Horizontal
		if (clampOnXMin && clampOnXMax) {
			targetPos.x = Mathf.Clamp(targetPos.x, xMin, xMax);
		} else if (clampOnXMin) {
			targetPos.x = Mathf.Clamp(targetPos.x, xMin, targetPos.x);
		} else if (clampOnXMax) {
			targetPos.x = Mathf.Clamp(targetPos.x, targetPos.x, xMax);
		}

		// Vertical
		if (clampOnYMin && clampOnYMax) {
			targetPos.y = Mathf.Clamp(targetPos.y, yMin, yMax);
		} else if (clampOnYMin) {
			targetPos.y = Mathf.Clamp(targetPos.y, yMin, targetPos.y);
		} else if (clampOnYMax) {
			targetPos.y = Mathf.Clamp(targetPos.y, targetPos.y, yMax);
		}

		// Use the default z position of the camera
		targetPos.z = transform.position.z;

		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
	}
}
