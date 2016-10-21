using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Text goldCountText;

	private int goldCount;

	void Awake()
	{
		goldCount = 0;
		SetGoldCountText ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Money")) {
			other.gameObject.SetActive (false);
			goldCount++;
			SetGoldCountText ();
		}
	}

	void SetGoldCountText()
	{
		goldCountText.text = "Gold: " + goldCount.ToString ();
	}
}
