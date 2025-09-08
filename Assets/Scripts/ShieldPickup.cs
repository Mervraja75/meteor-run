using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float duration = 10f;      // how long the shield stays active
    public AudioClip shieldSfx;       // assign in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // turn on shield
        var shield = other.GetComponent<PlayerShield>();
        if (shield != null) shield.Activate(duration);

        // play sound on Player's AudioSource
        var src = other.GetComponent<AudioSource>();
        if (shieldSfx && src) src.PlayOneShot(shieldSfx);

        // notify the spawner that this pickup part is gone
        var spawner = Object.FindFirstObjectByType<ShieldSpawner>();
        if (spawner != null) spawner.ClearActive();

        // remove the pickup
        Destroy(gameObject);
    }
}