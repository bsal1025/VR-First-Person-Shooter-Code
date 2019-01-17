using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager2 : MonoBehaviour {


    public static int score2;            // Create variable for score to carry over between waves
    Text dispscore;                      // Create text variable 

    // Use this for initialization
    void Start()
    {

        dispscore = GameObject.Find("dispscore").GetComponent<Text>(); // Find score display text component
        score2 = Score_Manager.score;   // Initialize variable to previous score, not zero
       
    }

    // Update is called once per frame
    void Update()
    {

        dispscore.text = "Score: " + score2;  // Display the new score

    }
}
