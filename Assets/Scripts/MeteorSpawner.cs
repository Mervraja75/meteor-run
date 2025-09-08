using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;     //the meteor object to be spawned
    public float spawnInterval = 2f;    // how often meteor fall (seconds between each one)
    public float spawnHeight = 6f;
    public float xSpawnRange = 8f;      //how far (left or right) the meteor appears
    public Transform player;

    public float horizontalPush = 1.5f; // small sideways speed so meteors drift
    private Rigidbody2D rb;             // cache the rigidbody to set velocity

    private float timer = 0f;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();                                // gets the rigidbody on this meteor
        float dir = Random.value < 0.5f ? -1f : 1f;                      // randomly choose left (-1) or right (+1)
        if (rb != null)                                                  // if rigidbody is found
        {
            rb.linearVelocity = new Vector2(dir * horizontalPush, rb.linearVelocity.y); // add a small sideways velocity
        }
    }

    void Update()
    {
       if (GameManager.instance != null && !GameManager.instance.gameIsRunning) return; 
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMeteor(); // creates a meteor at a random X axis above the player
            timer = 0f;
        }
        
    }

    void SpawnMeteor()
    {
        float spawnX = player.position.x + Random.Range(-xSpawnRange, xSpawnRange);
        Vector2 spawnPosition = new Vector2(spawnX, spawnHeight);
        Instantiate(meteorPrefab, spawnPosition, Quaternion.Euler(0, 0, 45)); //rotates object 45 degrees in 2D
    }
}