using UnityEngine;
using System.Collections;

public class GatheredKeysController : MonoBehaviour {

    public GameObject EnableDisable;
	// Use this for initialization
	public void Enable()
    {
        EnableDisable.SetActive(true);
    }

    public void Disable()
    {
        EnableDisable.SetActive(false);
    }
}
