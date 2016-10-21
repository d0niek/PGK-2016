using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Rigidbody rb;
	float cameraRayLength = 100;
	int floorMask;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Move (moveHorizontal, moveVertical);
		Turning ();
	}

	void Move(float horizontal, float vertical)
	{
		movement.Set (horizontal, 0f, vertical);
		movement = movement * speed * Time.deltaTime;

		rb.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray cameraRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (cameraRay, out floorHit, cameraRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			rb.MoveRotation (newRotation);
		}
	}
}
