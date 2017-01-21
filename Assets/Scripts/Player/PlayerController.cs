using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Text goldCountText;
    public Text keysCountText;
    public AudioClip[] audioClip;

    private int goldCount;
    public int keyCount { get; private set; }
//    Text T = GameObject.Find("GatheredKeys").GetComponent<Text>();


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
            PlayerSounds(0);
			SetGoldCountText ();
		}

        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            keyCount++;
            PlayerSounds(1);
            SetKeysCountText();
            
            
            if (keyCount == 4)
            {
  //              T.enabled = true;
                PlayerSounds(2);
            }
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

    public void PlayerSounds(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }
}
