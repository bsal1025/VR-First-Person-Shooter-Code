using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy_Manager : MonoBehaviour {
    [SerializeField] GameObject prefabEnemy1;         // Grab Enemy 1 Prefab
    [SerializeField] GameObject prefabEnemy2;         // Grab Enemy 2 Prefab
    [SerializeField] GameObject prefabEnemy3;         // Grab Enemy 3 Prefab
    public GameObject newEnemy1;                      // Create variable for new instances of Enemy1
    public GameObject newEnemy2;                      // Create variable for new instances of Enemy2
    public GameObject newEnemy3;                      // Create variable for new instances of Enemy2
    int j;                                            // Loop variable used later in the script
    Text wave1end;                                    // Create text variable for end of wave text
    Text wave1start;                                  // Create text variable for beginning of wave text 

    // Use this for initialization
    void Start () {

        StartCoroutine(GenNewSet());                                        // Start a coroutine that spawns a new set of enemies
        wave1end = GameObject.Find("wave1end").GetComponent<Text>();        // Get Wave 1 Over text
        wave1start = GameObject.Find("wave1start").GetComponent<Text>();    // Get Begin Wave 1 text
        wave1start.enabled = true;                                          // Display Begin Wave 1 at start of level
        StartCoroutine(removetext());                                       // Start a coroutine to clear level start text 


    }
    IEnumerator removetext()
    {
        yield return new WaitForSeconds(1.5f);                              // Wait 1.5 seconds
        Destroy(wave1start);                                                // Remove Begin Wave 1 text
    }

    IEnumerator GenNewSet()
    {
        while (true)
        {
                       
            System.Random random = new System.Random();                    // Create a new random variable called random
            for (j = 0; j < 8; j++)
            {
                // Create 8 instances of Enemy1 and place them in random locations
                newEnemy1 = Instantiate(prefabEnemy1, new Vector3(random.Next(-43, 115), 0, random.Next(-251, -40)), Quaternion.identity);
                newEnemy1.GetComponent<NavMeshAgent>().speed = 4.5f;  // Increase Enemy1 Speed to 4.5
            }

            for (j = 0; j < 3; j++)
            {
                // Create 3 instances of Enemy2 and place them in random locations
                newEnemy2 = Instantiate(prefabEnemy2, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy2.GetComponent<NavMeshAgent>().speed = 2.5f; // Increase Enemy2 Speed to 2.5
            }

            for (j = 0; j < 1; j++)
            {
                // Create 1 instance of Enemy3 and place them in random locations
                newEnemy3 = Instantiate(prefabEnemy3, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy3.GetComponent<NavMeshAgent>().speed = 1f; // Set Enemy3 Speed to 1
            }

            if (Time.timeSinceLevelLoad >= 120f)  // If 2 minutes or more has elapsed
            {
                  StopAllCoroutines();            // Stop generating enemies

            }
            
            yield return new WaitForSeconds(30f);  // Wait 30 seconds, then create a new set of enemies

        }
    }

        
	// Update is called once per frame
	void Update ()
    {
        if (Score_Manager.score >= 85)       // If the player score reaches 85 or higher
        {
            if (wave1end != null)            // If Wave 1 Over is already displayed onscreen 
            {
                wave1end.enabled = true;     // Display Wave 1 Over onscreen
                StartCoroutine(desttext());  // Start a coroutine to destroy the Wave 1 Over text before next round
              
            }
            else
            {
                StartCoroutine(desttext());  // Start a coroutine to destroy the Wave 1 Over text before next round 
            }


        }
        else
        {
            wave1end.enabled = false;     // Turn off Wave 1 Over text 
        }

             
    }
        
     
    IEnumerator desttext()
    {
        yield return new WaitForSeconds(2.0f);  // Wait 2 seconds
        Destroy(wave1end);                      // Destroy Wave 1 Over text 
        SceneManager.LoadScene("Wave_2", LoadSceneMode.Single);  // Load Wave 2
    }

    void OnGUI()
    {
        GUI.contentColor = Color.blue;

        GUI.Label(new Rect(0, 50, 200, 25), "Forward: W");
        GUI.Label(new Rect(0, 75, 200, 25), "Backward: S");
        GUI.Label(new Rect(0, 100, 200, 25), "Strafe Left: A");
        GUI.Label(new Rect(0, 125, 200, 25), "Strafe Right: D");
        GUI.Label(new Rect(0, 150, 200, 25), "Jump: Spacebar");
        GUI.Label(new Rect(0, 175, 200, 25), "Sniper Rifle: 1");
        GUI.Label(new Rect(0, 200, 200, 25), "Shotgun: 2");
        GUI.Label(new Rect(0, 225, 200, 25), "Machine Gun: 3");
        GUI.Label(new Rect(0, 250, 200, 25), "Reload: R");
        GUI.Label(new Rect(0, 275, 200, 25), "Fire Weapon: Left Mouse");
        GUI.Label(new Rect(0, 300, 200, 25), "Aim Weapon: Right Mouse");
    }
}

