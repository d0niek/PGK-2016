using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Text goldCountText;
    public Text keysCountText;

	private int goldCount;
    private int keyCount;


	void Awake ()
	{
		goldCount = 0;
        keyCount = 0;
		SetGoldCountText ();
        SetKeysCountText ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Money"))
        {
			other.gameObject.SetActive (false);
			goldCount++;
			SetGoldCountText ();
		}

        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            keyCount++;
            SetKeysCountText();
        }
    }


	void SetGoldCountText ()
	{
		goldCountText.text = "Gold: " + goldCount.ToString ();
	}
    void SetKeysCountText()
    {
        keysCountText.text = "Keys: " + keyCount.ToString();
    }
}
