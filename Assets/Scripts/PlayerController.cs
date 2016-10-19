using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public Text goldCountText;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController cc;
	private int goldCount;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
		goldCount = 0;
		SetGoldCountText ();
	}

	void FixedUpdate ()
	{
		if (cc.isGrounded) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
		}

		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Money")) {
			other.gameObject.SetActive (false);
			goldCount++;
			SetGoldCountText ();
		}
	}

	void SetGoldCountText ()
	{
		goldCountText.text = "Gold: " + goldCount.ToString ();
	}
}
