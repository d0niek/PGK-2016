using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeysMsgController : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.Find("Player");
        PlayerController accessor = player.GetComponent<PlayerController>();

        if (accessor.keyCount == accessor.keyAmount)
        {
            StartCoroutine(ShowMessage("You gathered all keys!", 3));
        }
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        Text text;
        text = GetComponent<Text>();
        text.text = message;
        text.enabled = true;
        yield return new WaitForSeconds(delay);
        text.transform.Translate(new Vector3(200, 200));
    }
}
