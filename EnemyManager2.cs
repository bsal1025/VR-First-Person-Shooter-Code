using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager2 : MonoBehaviour {
    [SerializeField] GameObject prefabEnemy1;        // Grab Enemy 1 Prefab
    [SerializeField] GameObject prefabEnemy2;        // Grab Enemy 2 Prefab
    [SerializeField] GameObject prefabEnemy3;        // Grab Enemy 2 Prefab
    public GameObject newEnemy1;                     // Create variable for new instances of Enemy1
    public GameObject newEnemy2;                     // Create variable for new instances of Enemy2
    public GameObject newEnemy3;                     // Create variable for new instances of Enemy3
    int j;                                           // Loop variable used later in the script
    Text wave2end;                                   // Create text variable for end of wave text
    Text wave2start;                                 // Create text variable for beginning of wave text 
    // Use this for initialization
    void Start()
    {

        StartCoroutine(GenNewSet());                                          // Start a coroutine that spawns a new set of enemies
        wave2end = GameObject.Find("wave2end").GetComponent<Text>();          // Get Wave 2 Over text
        wave2start = GameObject.Find("wave2start").GetComponent<Text>();      // Get Begin Wave 2 text
        wave2start.enabled = true;                                            // Display Begin Wave 2 at start of level
        StartCoroutine(removetext());                                         // Start a coroutine to clear level start text 


    }
    IEnumerator removetext()
    {
        yield return new WaitForSeconds(1.5f);                                // Wait 1.5 seconds
        Destroy(wave2start);                                                  // Remove Begin Wave 2 text
    }

    IEnumerator GenNewSet()
    {
        while (true)
        {

            System.Random random = new System.Random();                       // Create a new random variable called random
            for (j = 0; j < 8; j++)
            {
                // Create 8 instances of Enemy1 and place them in random locations
                newEnemy1 = Instantiate(prefabEnemy1, new Vector3(random.Next(-43, 115), 0, random.Next(-251, -40)), Quaternion.identity);
                newEnemy1.GetComponent<NavMeshAgent>().speed = 4.5f;  // Increase Enemy1 Speed to 4.5
            }

            for (j = 0; j < 5; j++)
            {
                // Create 5 instances of Enemy2 and place them in random locations
                newEnemy2 = Instantiate(prefabEnemy2, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy2.GetComponent<NavMeshAgent>().speed = 2.5f; // Increase Enemy2 Speed to 2.5
            }

            for (j = 0; j < 3; j++)
            {
                // Create 3 instances of Enemy3 and place them in random locations
                newEnemy3 = Instantiate(prefabEnemy3, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy3.GetComponent<NavMeshAgent>().speed = 1f; // Set Enemy3 Speed to 1
            }

            if(Time.timeSinceLevelLoad >= 120f)  // If 2 minutes or more has elapsed
            {
                StopAllCoroutines();             // Stop generating enemies

            }

            yield return new WaitForSeconds(30f);  // Wait 30 seconds, then create a new set of enemies

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Score_Manager.score >= 220)      // If the player score reaches 220 or higher
        {
            if (wave2end != null)            // If Wave 2 Over is already displayed onscreen 
            {
                wave2end.enabled = true;     // Display Wave 2 Over onscreen
                StartCoroutine(desttext());  // Start a coroutine to destroy the Wave 2 Over text before next round
            }
            else
            {
                StartCoroutine(desttext());  // Start a coroutine to destroy the Wave 2 Over text before next round 
            }


        }
        else
        {
            wave2end.enabled = false;     // Turn off Wave 2 Over text 
        }
    }
    IEnumerator desttext()
    {
        yield return new WaitForSeconds(2.0f);  // Wait 2 seconds
        Destroy(wave2end);                      // Destroy Wave 2 Over text 
        SceneManager.LoadScene("Wave_3", LoadSceneMode.Single);  // Load Wave 3
    }
}
