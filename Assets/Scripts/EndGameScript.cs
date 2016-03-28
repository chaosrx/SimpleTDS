using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    //ends game when backspace pressed.  Add prompt later, or bring back to main menu. 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
