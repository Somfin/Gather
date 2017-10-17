using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableIfFileToLoad : MonoBehaviour {
    [SerializeField]
    private int fileIndex;

	// Use this for initialization
	void Start () {
        if (GetComponentInParent<ButtonUtilities>().HasFileToLoad(fileIndex))
        {
            GetComponent<Button>().interactable = true;
        } else
        {
            GetComponent<Button>().interactable = false;
        }
	}
}
