using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score_Manager : MonoBehaviour {

    public static int score;           // Create variable to hold the score
    Text dispscore;                    // Create text variable
   
    // Use this for initialization
    void Start () {

        dispscore = GameObject.Find("dispscore").GetComponent<Text>(); // Find display score text component
        score = 0;                                                     // Initialize score to zero
              
    }
	
	// Update is called once per frame
	void Update () {
        dispscore.text = "Score: " + score;              // Display the score onscreen 
     }
}
