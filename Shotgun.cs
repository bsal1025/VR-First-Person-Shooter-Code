using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {
    public Renderer shotgun;                  // Create variable for shotgun renderer
    // Use this for initialization
    void Start () {
        shotgun = GetComponent<Renderer>();   // Grab shotgun
        shotgun.enabled = false;              // Disable shotgun on startup
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("2"))           // If player presses 2
        {
            shotgun.enabled = true;          // Enable shotgun
        }
        else if (Input.GetKeyDown("1"))      // If player presses 1
        {
            shotgun.enabled = false;         // Disable shotgun
        }
        else if (Input.GetKeyDown("3"))      // If player presses 3
        {
            shotgun.enabled = false;         // Disable shotgun
        } 
    }
}
