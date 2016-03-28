using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public int damage = 1;
    public bool isEnemyShot = false;

    // Use this for initialization
    void Start () {
        //destroy the shot after 20 seconds.  
        //This needs to be refined to die after hitting edges of screen. 
        Destroy(gameObject, 20);
    }
	
}
