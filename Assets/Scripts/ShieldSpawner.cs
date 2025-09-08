using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    public GameObject shieldPrefab;   // ShieldPickup prefab
    public Transform player;          // Player
    public float spawnInterval = 15f; // seconds between spawns
    public float spawnAhead = 20f;    // distance between shield and player
    public float minY = 0f;           // lowest spawn height
    public float maxY = 3f;           // highest spawn height
    public float pickupDuration = 10f;// the pickup's duration

    float timer;                      // countdown
    GameObject active;                // last spawned pickup ( therefore only one at a time)

    void Start()
    {
        timer = spawnInterval;        // start the countdown
    }

    void Update()
    {
        if (GameManager.instance != null && !GameManager.instance.gameIsRunning) return;
        if (!shieldPrefab || !player) return;

        // if a pickup still exists, it does nothing
        if (active != null) return;

        // tick timer
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        // pick a position ahead of the player at a random height
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(player.position.x + spawnAhead, y, 0f);

        // spawn
        active = Instantiate(shieldPrefab, pos, Quaternion.identity);

        // force its duration to 10
        var sp = active.GetComponent<ShieldPickup>();
        if (sp) sp.duration = pickupDuration;

        // reset timer for next time
        timer = spawnInterval;
    }

    // call this from the pickup when it gets collected/destroyed
    public void ClearActive()
    {
        active = null;
    }
}