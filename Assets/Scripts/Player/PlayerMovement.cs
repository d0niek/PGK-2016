using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController cc;
	private float cameraRayLength = 100;
	private int floorMask;
    Animator anim;

	void Awake()
	{
        anim = GetComponentInChildren<Animator>();
		floorMask = LayerMask.GetMask ("Floor");
		cc = GetComponent<CharacterController> ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        Move (moveHorizontal, moveVertical);
		Turning ();
	}

	void Move(float horizontal, float vertical)
	{
		if (cc.isGrounded) {
			moveDirection = new Vector3(horizontal, 0, vertical);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
		}

		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);
	}

	void Turning()
	{
		Ray cameraRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (cameraRay, out floorHit, cameraRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			transform.rotation = newRotation;
		}
	}
}
