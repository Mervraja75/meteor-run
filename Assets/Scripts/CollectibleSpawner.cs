using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab; //the prefab to Spawn coins
    public Transform player;             //reference to the player
    public float spawnInterval = 3f;     //time between each spawn (in seconds)
    public float xOffset = 15f;          //how far ahead of the player to spawn collectible

    public float yMin = -1f;            //the lowest Y position
    public float yMax = 3f;             //the highest Y position

    private float timer = 0f;           //timer to track when to spawn next


    void Update() 
    {
        if (GameManager.instance != null && !GameManager.instance.gameIsRunning) return;
        timer += Time.deltaTime;        //increased timer by the time passed from previous frame 
        if (timer >= spawnInterval)     //if enough time has passed, spawn a new coin 
        {
            SpawnCollectible();         //call method to spawn 
            timer = 0f;                 //reset timer 
        }
    }

    void SpawnCollectible()                                                             //spawns one collectible at a any Y position ahead of the player
    {
        float spawnY = Random.Range(yMin, yMax);                                        // chooses a random height between yMin and yMax
        Vector3 spawnPosition = new Vector3(player.position.x + xOffset, spawnY, 0f);   //set position ahead of the player with random height
        Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);             // create a new coin in the scene at that position 

    }



}
