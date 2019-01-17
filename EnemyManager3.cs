using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager3 : MonoBehaviour {

    [SerializeField] GameObject prefabEnemy1;       // Grab Enemy 1 Prefab
    [SerializeField] GameObject prefabEnemy2;       // Grab Enemy 2 Prefab
    [SerializeField] GameObject prefabEnemy3;       // Grab Enemy 3 Prefab
    public GameObject newEnemy1;                    // Create variable for new instances of Enemy1
    public GameObject newEnemy2;                    // Create variable for new instances of Enemy2
    public GameObject newEnemy3;                    // Create variable for new instances of Enemy3
    int j;                                          // Loop variable used later in the script
    Text wave3end;                                  // Create text variable for end of wave text
    Text wave3start;                                // Create text variable for beginning of wave text 
    // Use this for initialization
    void Start()
    {

        StartCoroutine(GenNewSet());                                          // Start a coroutine that spawns a new set of enemies
        wave3end = GameObject.Find("wave3end").GetComponent<Text>();          // Get Wave 3 Over text
        wave3start = GameObject.Find("wave3start").GetComponent<Text>();      // Get Begin Wave 3 text
        wave3start.enabled = true;                                            // Display Begin Wave 3 at start of level
        StartCoroutine(removetext());                                         // Start a coroutine to clear level start text 


    }
    IEnumerator removetext()
    {
        yield return new WaitForSeconds(1.5f);                                // Wait 1.5 seconds
        Destroy(wave3start);                                                  // Remove Begin Wave 3 text
    }

    IEnumerator GenNewSet()
    {
        while (true)
        {

            System.Random random = new System.Random();                      // Create a new random variable called random
            for (j = 0; j < 10; j++)
            {
                // Create 10 instances of Enemy1 and place them in random locations
                newEnemy1 = Instantiate(prefabEnemy1, new Vector3(random.Next(-43, 115), 0, random.Next(-251, -40)), Quaternion.identity);
                newEnemy1.GetComponent<NavMeshAgent>().speed = 5.5f; // Increase Enemy1 Speed to 5.5
            }

            for (j = 0; j < 5; j++)
            {
                // Create 5 instances of Enemy2 and place them in random locations
                newEnemy2 = Instantiate(prefabEnemy2, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy2.GetComponent<NavMeshAgent>().speed = 3.5f;  // Increase Enemy2 Speed to 3.5
            }

            for (j = 0; j < 5; j++)
            {
                // Create 5 instances of Enemy3 and place them in random locations
                newEnemy3 = Instantiate(prefabEnemy3, new Vector3(random.Next(-43, 115), 1, random.Next(-251, -40)), Quaternion.identity);
                newEnemy3.GetComponent<NavMeshAgent>().speed = 2f; // Set Enemy3 Speed to 2
            }

            if (Time.timeSinceLevelLoad >= 120f)  // If 2 minutes or more has elapsed
            {
                StopAllCoroutines();              // Stop generating enemies

            }

            yield return new WaitForSeconds(30f);  // Wait 30 seconds, then create a new set of enemies

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Score_Manager.score >= 395)      // If the player score reaches 395 or higher
        {
            if (wave3end != null)            // If Wave 3 Over is already displayed onscreen 
            {
                wave3end.enabled = true;     // Display Wave 3 Over onscreen
                StartCoroutine(desttext());  // Start a coroutine to destroy the Wave 3 Over text before next round
            }
            else
            {
                StartCoroutine(desttext());  // Start a coroutine to destroy the Wave 3 Over text before next round 
            }


        }
        else
        {
            wave3end.enabled = false;        // Turn off Wave 3 Over text 
        }
    }
    IEnumerator desttext()
    {
        yield return new WaitForSeconds(2.0f);  // Wait 2 seconds
        Destroy(wave3end);                      // Destroy Wave 3 Over text 
        SceneManager.LoadScene("Wave_4", LoadSceneMode.Single);  // Load Wave 4
    }
}